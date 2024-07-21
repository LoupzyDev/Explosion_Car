using UnityEngine;
using System.Collections;

public class RearWheelDrive : MonoBehaviour
{
    private WheelCollider[] wheels;

    public float maxAngle = 30f;
    public float maxTorque = 300f;
    public GameObject wheelShape;

    private void Start()
    {
        wheels = GetComponentsInChildren<WheelCollider>();

        foreach (var wheel in wheels)
        {
            // Crear formas de ruedas solo cuando sea necesario
            if (wheelShape != null)
            {
                var ws = Instantiate(wheelShape);
                ws.transform.parent = wheel.transform;
            }
        }
    }

    private void Update()
    {
        float angle = maxAngle * Input.GetAxis("Horizontal");
        float torque = maxTorque * Input.GetAxis("Vertical");
        bool handbrake = Input.GetKey(KeyCode.Space);

        foreach (var wheel in wheels)
        {
            // Las ruedas delanteras giran y las traseras impulsan
            if (wheel.transform.localPosition.z > 0)
                wheel.steerAngle = angle;

            if (wheel.transform.localPosition.z < 0)
            {
                wheel.motorTorque = torque;
                if (handbrake)
                {
                    wheel.brakeTorque = Mathf.Infinity; // Activar freno de mano
                }
                else
                {
                    wheel.brakeTorque = 0; // Desactivar freno de mano
                }
            }

            // Actualizar las ruedas visuales si existen
            if (wheelShape != null)
            {
                wheel.GetWorldPose(out Vector3 pos, out Quaternion quat);
                Transform shapeTransform = wheel.transform.GetChild(0);
                shapeTransform.position = pos;
                shapeTransform.rotation = quat;
            }
        }
    }
}