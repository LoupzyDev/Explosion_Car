using UnityEngine;

public class RearWheelDrive : MonoBehaviour {
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    public float maxAngle = 30f; // Puedes reducir este valor para mejorar la maniobrabilidad
    public float maxTorque = 100000f;
    public GameObject wheelShape;

    public float maxBrakeTorque = 500f; // Añadido para ajustar el par de frenado máximo

    private Rigidbody rb;

    private void Start() {
        // Crear formas de ruedas solo cuando sea necesario
        if (wheelShape != null) {
            CreateWheelShape(frontLeftWheel);
            CreateWheelShape(frontRightWheel);
            CreateWheelShape(rearLeftWheel);
            CreateWheelShape(rearRightWheel);
        }

        rb = GetComponent<Rigidbody>();
        //rb.centerOfMass = new Vector3(0, -0.9f, 0); // Bajando el centro de masa
    }

    private void Update() {
        float angle = maxAngle * Input.GetAxis("Horizontal");
        float torque = maxTorque * -Input.GetAxis("Vertical");
        bool handbrake = Input.GetKey(KeyCode.Space);

        // Las ruedas delanteras giran
        frontLeftWheel.steerAngle = angle;
        frontRightWheel.steerAngle = angle;

        // Las ruedas traseras impulsan
        rearLeftWheel.motorTorque = torque;
        rearRightWheel.motorTorque = torque;

        if (handbrake) {
            rearLeftWheel.brakeTorque = maxBrakeTorque;
            rearRightWheel.brakeTorque = maxBrakeTorque;
        } else {
            rearLeftWheel.brakeTorque = 0;
            rearRightWheel.brakeTorque = 0;
        }

        // Actualizar la posición y rotación de las formas de las ruedas
        UpdateWheelPose(frontLeftWheel);
        UpdateWheelPose(frontRightWheel);
        UpdateWheelPose(rearLeftWheel);
        UpdateWheelPose(rearRightWheel);
    }

    private void CreateWheelShape(WheelCollider wheel) {
        if (wheelShape != null && wheel != null) {
            var ws = Instantiate(wheelShape);
            ws.transform.parent = wheel.transform;
        }
    }

    private void UpdateWheelPose(WheelCollider collider) {
        if (wheelShape != null && collider != null) {
            collider.GetWorldPose(out Vector3 pos, out Quaternion quat);
            Transform shapeTransform = collider.transform.GetChild(0);

            shapeTransform.position = pos;
            shapeTransform.rotation = quat;
        }
    }
}
