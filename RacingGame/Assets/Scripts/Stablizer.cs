using UnityEngine;
using System.Collections;

public class Stablizer : MonoBehaviour
{
    public WheelCollider WheelL;
    public WheelCollider WheelR;
    public float AntiRoll = 5000.0f;

    // Use this for initialization
    void Start()
    {

    }

    public void FixedUpdate()
    {
        WheelHit hit;
        float travelL = 1.0f;
        float travelR = 1.0f;

        bool groundedL = WheelL.GetGroundHit(out hit);
        bool groundedR = WheelR.GetGroundHit(out hit);

        if (groundedL)
            travelL = (-WheelL.transform.InverseTransformPoint(hit.point).y - WheelL.radius)
                      / WheelL.suspensionDistance;

        if (groundedR)
            travelR = (-WheelR.transform.InverseTransformPoint(hit.point).y - WheelR.radius)
                      / WheelR.suspensionDistance;

        float antiRollForce = (travelL - travelR) * AntiRoll;

        if (groundedL && !groundedR)
            rigidbody.AddForceAtPosition(WheelL.transform.up * -antiRollForce, WheelL.transform.position);
        if (groundedR && !groundedL)
            rigidbody.AddForceAtPosition(WheelR.transform.up * antiRollForce, WheelR.transform.position);
    }
}
