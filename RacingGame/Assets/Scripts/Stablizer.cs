using UnityEngine;
using System.Collections;

public class Stablizer : MonoBehaviour
{
    public WheelCollider m_WheelL;
    public WheelCollider m_WheelR;
    public float m_AntiRoll = 5000.0f;

    // Use this for initialization
    void Start()
    {

    }

    public void FixedUpdate()
    {
        WheelHit hit;
        float travelL = 1.0f;
        float travelR = 1.0f;

        bool groundedL = m_WheelL.GetGroundHit(out hit);
        bool groundedR = m_WheelR.GetGroundHit(out hit);

        if (groundedL)
            travelL = (-m_WheelL.transform.InverseTransformPoint(hit.point).y - m_WheelL.radius)
                      / m_WheelL.suspensionDistance;

        if (groundedR)
            travelR = (-m_WheelR.transform.InverseTransformPoint(hit.point).y - m_WheelR.radius)
                      / m_WheelR.suspensionDistance;

        float antiRollForce = (travelL - travelR) * m_AntiRoll;

        if (groundedL && !groundedR)
            rigidbody.AddForceAtPosition(m_WheelL.transform.up * -antiRollForce, m_WheelL.transform.position);
        if (groundedR && !groundedL)
            rigidbody.AddForceAtPosition(m_WheelR.transform.up * antiRollForce, m_WheelR.transform.position);
    }
}
