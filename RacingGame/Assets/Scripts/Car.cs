using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {
    public WheelCollider[] wheels;
    public JointSpring m_springJoint;
    public float m_Mass;
    public float m_SuspensionDistance;
    public WheelData m_ForwardFrictionData;
    public WheelData m_SidwaysFrictionData;
    public WheelCollider[] drivers;
    public float mt;


	// Update is called once per frame
    void Start()
    {
        foreach(WheelCollider wc in wheels){
            wc.suspensionSpring = m_springJoint;
            wc.mass = m_Mass;
            wc.suspensionDistance = m_SuspensionDistance;
            wc.forwardFriction = m_ForwardFrictionData.setupWheelFriction();
            wc.sidewaysFriction = m_SidwaysFrictionData.setupWheelFriction();
        }
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            foreach (WheelCollider wc in wheels)
            {
                wc.motorTorque = mt;
            }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            foreach (WheelCollider wc in wheels)
            {
                wc.motorTorque = mt*(-1);
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            foreach (WheelCollider wc in wheels)
            {
                wc.steerAngle = wc.steerAngle+(-90 / rigidbody.velocity.magnitude);
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            foreach (WheelCollider wc in wheels)
            {
                wc.steerAngle = wc.steerAngle+( 90 / rigidbody.velocity.magnitude);
            }
        }
        else
        {
            foreach (WheelCollider wc in wheels)
            {
                wc.steerAngle = 0;
            }
        }
    }
}