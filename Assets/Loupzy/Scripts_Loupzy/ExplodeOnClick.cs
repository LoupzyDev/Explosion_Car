using TMPro;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ExplodeOnClick : MonoBehaviour {
    private Camera Camera;
    [SerializeField]
    private ParticleSystem ParticleSystemPrefab;
    public int MaxHits = 25;
    public float Radius = 10f;
    public LayerMask HitLayer;
    public LayerMask BlockExplosionLayer;
    public int MaxDamage = 50;
    public int MinDamage = 10;
    public float ExplosiveForce;

    public float timer;
    public float maxTimer = 30f;
    public float curRadius;
    public TextMeshProUGUI timerText;

    private Collider[] Hits;

    private void Awake() {
        Camera = GetComponent<Camera>();
        Hits = new Collider[MaxHits];
        curRadius = Radius;
    }

    private void Update() {
        timer += Time.deltaTime;
        timerText.text = $"Tiempo: {Mathf.FloorToInt(timer)} ";

        if (timer > maxTimer)
        {
            AddRadius();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0)) {
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit)) {
                ParticleSystem particleSystemInstance = Instantiate(ParticleSystemPrefab, hit.point, Quaternion.identity);
                ScaleParticleSystem(particleSystemInstance, curRadius);
                Destroy(particleSystemInstance.gameObject, 2f);

                int hits = Physics.OverlapSphereNonAlloc(hit.point, curRadius, Hits, HitLayer);

                for (int i = 0; i < hits; i++) {
                    if (Hits[i].TryGetComponent<Rigidbody>(out Rigidbody rigidbody)) {
                        float distance = Vector3.Distance(hit.point, Hits[i].transform.position);

                        if (!Physics.Raycast(hit.point, (Hits[i].transform.position - hit.point).normalized, distance, BlockExplosionLayer.value)) {
                            rigidbody.AddExplosionForce(ExplosiveForce, hit.point, curRadius);
                            //Debug.Log($"Would hit {rigidbody.name} for {Mathf.FloorToInt(Mathf.Lerp(MaxDamage, MinDamage, distance / Radius))}");
                        }
                    }
                }
            }
        }
    }

    private void AddRadius()
    {
        curRadius += Radius;
        Debug.Log("Más radio: " + curRadius);
        timer = 0f;
    }

    private void ScaleParticleSystem(ParticleSystem particleSystem, float radius)
    {
        var main = particleSystem.main;
        main.startSize = radius; 

        var shape = particleSystem.shape;
        shape.radius = radius; 
    }
}
