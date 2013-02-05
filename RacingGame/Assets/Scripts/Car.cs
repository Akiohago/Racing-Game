using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {
    public WheelCollider[] wheels;
    public JointSpring m_springJoint;
    public float m_Mass;
    public float m_SuspensionDistance;
    public WheelFrictionCurve m_ForwardFriction;
    public WheelFrictionCurve m_SidewaysFriction;
    public WheelCollider[] drivers;
    public float mt;


	// Update is called once per frame
    void Start()
    {
        foreach(WheelCollider wc in wheels){
            wc.suspensionSpring = m_springJoint;
            wc.mass = m_Mass;
            wc.suspensionDistance = m_SuspensionDistance;
            wc.forwardFriction = m_ForwardFriction;
            wc.sidewaysFriction = m_SidewaysFriction;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            foreach (WheelCollider wc in drivers)
            {
                wc.motorTorque = mt;
            }
        }

    }
}
