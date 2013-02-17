using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class RigidBodyCar : MonoBehaviour {
	private static Rigidbody playerCar;
	public float m_maxSpeed;
	public float m_acceleration;
	public float m_turnSpeed;
	public float m_breakAmount;
	
	protected float m_gasAmount;
	protected float m_steerAmount;
	protected bool m_brakeActive;
	
	protected float m_currentSpeed;
	
	
	// Use this for initialization
	void Start () {
		if(tag=="Player"){
			playerCar=rigidbody;
		}
	}
	
	// Update is called once per frame
	void Update () {
		m_gasAmount = Input.GetAxis("Vertical");
		m_steerAmount = Input.GetAxis ("Horizontal");
		m_brakeActive = Input.GetButton("Jump");
		if(m_gasAmount!=0){
			gas();
		}
		if(m_steerAmount!=0){
			steer();
		}
		if(m_brakeActive){
			stop ();
		}
	}
	public static string s_speed{
		get{
			return ""+playerCar.velocity.magnitude;
		}
	}
	protected void gas(){
		if(m_maxSpeed<rigidbody.velocity.magnitude){
			rigidbody.velocity=rigidbody.velocity+(transform.forward*m_gasAmount*(m_acceleration*Time.deltaTime));
		}
	}
	protected void stop(){
		rigidbody.velocity=rigidbody.velocity*m_breakAmount;
	}
	protected void steer(){
		transform.RotateAround(new Vector3(0,1,0),(m_steerAmount*Time.deltaTime*m_turnSpeed));
	}
}
