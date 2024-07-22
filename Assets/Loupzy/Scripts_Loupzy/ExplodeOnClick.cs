using TMPro;
using UnityEngine;

public class ExplodeOnClick : MonoBehaviour {
    private Camera Camera;
    [SerializeField]
    private ParticleSystem ParticleSystemPrefab;
    public int MaxHits = 25;
    public float Radius = 1000f;
    public LayerMask HitLayer;
    public LayerMask BlockExplosionLayer;
    public int MaxDamage = 50;
    public int MinDamage = 10;
    public float ExplosiveForce;

    public float timer;
    public float maxTimer = 60f;
    public float curRadius;
    public TextMeshProUGUI timerText;

    private Collider[] Hits;
    private bool exploded = false;
    private Vector3 initialScale;

    private void Awake() {
        Camera = GetComponent<Camera>();
        Hits = new Collider[MaxHits];
        curRadius = Radius;
        initialScale = new Vector3(10f, 10, 10f);
        ParticleSystemPrefab.transform.localScale = initialScale;
    }

    private void Update() {
        if (maxTimer > 0) {
            maxTimer -= Time.deltaTime;
            timerText.text = $"Tiempo pa explotar: {Mathf.FloorToInt(maxTimer)}";
        } else if (!exploded) {
            Explode();
            exploded = true;
        }

        timer += Time.deltaTime;
        if (timer >= 10f) {
            AddRadius();
            timer = 0f;
        }
    }

    private void AddRadius() {
        float force=ExplosiveForce+100;
        ExplosiveForce = force;
        curRadius += Radius;
        Radius = curRadius;
        ParticleSystemPrefab.transform.localScale *= 2f;
        Debug.Log("Más radio: " + curRadius);
    }

    private void Explode() {
        Vector3 explosionPoint = transform.position;
        ParticleSystem particleSystemInstance = Instantiate(ParticleSystemPrefab, explosionPoint, Quaternion.identity);
        Destroy(particleSystemInstance.gameObject, 2f);

        int hits = Physics.OverlapSphereNonAlloc(explosionPoint, curRadius, Hits, HitLayer);

        for (int i = 0; i < hits; i++) {
            if (Hits[i].TryGetComponent<Rigidbody>(out Rigidbody rigidbody)) {
                float distance = Vector3.Distance(explosionPoint, Hits[i].transform.position);

                if (!Physics.Raycast(explosionPoint, (Hits[i].transform.position - explosionPoint).normalized, distance, BlockExplosionLayer.value)) {
                    rigidbody.AddExplosionForce(ExplosiveForce, explosionPoint, curRadius);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("CheckPoint")) {
            maxTimer += 30f;
            Destroy(other.gameObject);
        }
    }
}
