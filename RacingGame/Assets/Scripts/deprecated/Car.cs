using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {
    public WheelCollider[] m_wheels;
    public JointSpring m_springJoint;
    public float m_Mass;
    public float m_SuspensionDistance;
    public Vector3 m_centerOfMassOffset;
    public WheelData m_ForwardFrictionData;
    public WheelData m_SidwaysFrictionData;
    public WheelCollider[] m_drivers;
    public float mt;


	// Update is called once per frame
    void Start(){
        rigidbody.centerOfMass = rigidbody.centerOfMass + m_centerOfMassOffset;
        foreach(WheelCollider wc in m_wheels){
            wc.suspensionSpring = m_springJoint;
            wc.mass = m_Mass;
            wc.suspensionDistance = m_SuspensionDistance;
            wc.forwardFriction = m_ForwardFrictionData.setupWheelFriction();
            wc.sidewaysFriction = m_SidwaysFrictionData.setupWheelFriction();
        }
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow)){
            foreach (WheelCollider wc in m_wheels)
            {
                wc.motorTorque = mt;
            }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            foreach (WheelCollider wc in m_drivers)
            {
                wc.motorTorque = mt*(-1);
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            foreach (WheelCollider wc in m_drivers)
            {
                wc.steerAngle = wc.steerAngle - 1;//(-90 / rigidbody.velocity.magnitude);
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            foreach (WheelCollider wc in m_wheels)
            {
                wc.steerAngle = wc.steerAngle+1;//( 90 / rigidbody.velocity.magnitude);
            }
        }
        else
        {
            foreach (WheelCollider wc in m_wheels)
            {
                wc.steerAngle = 0;
            }
        }
    }
}