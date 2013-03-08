using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class RigidBodyCar : MonoBehaviour {
	private static Rigidbody playerCar;
	public float m_maxSpeed;
	public float m_acceleration;
	public float m_turnSpeed;
	public float m_breakAmount;
	
	public Vector3 m_lastCheckPoint;
	public Vector3 m_goalDir;

	protected float m_gasAmount;
	protected float m_steerAmount;
	protected bool m_brakeActive;
    private float m_maxDownTime=5f;
    private float m_downTimer;
	protected float m_currentSpeed;

	
	// Use this for initialization
	void Start () {
		if(tag=="Player"){
			playerCar=rigidbody;
		}
        m_goalDir = Vector3.forward;
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
        rightCheck();

	}
    protected void rightCheck(){
        if (1 < vectorValueCompare(transform.up, Vector3.up)||rigidbody.velocity.magnitude<.01f)
        {
            if (m_downTimer > 0)
            {
                m_downTimer = m_downTimer - Time.deltaTime;
            }
            else
            {
               CarManager.sm_carManager.respawnCar(gameObject);
            }
        }
        else
        {
            m_downTimer = m_maxDownTime;
        }

    }
	public static string s_speed{
		get{
			return ""+playerCar.velocity.magnitude;
		}
	}
	protected void gas(){
		if(m_maxSpeed>rigidbody.velocity.magnitude){
			rigidbody.velocity=rigidbody.velocity+(transform.forward*m_gasAmount*(m_acceleration*Time.deltaTime));
		}
	}
	protected void stop(){
		rigidbody.velocity=rigidbody.velocity*m_breakAmount;
	}
	protected void steer(){
		transform.RotateAround(new Vector3(0,1,0),(m_steerAmount*Time.deltaTime*m_turnSpeed));
	}
    float vectorValueCompare(Vector3 vec1, Vector3 vec2)
    {
        float diff = Mathf.Abs(vec1.x - vec2.x) + Mathf.Abs(vec1.y - vec2.y) + Mathf.Abs(vec1.z - vec2.z);
        return diff;
    }
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag=="Finishline"){
			Debug.Log(gameObject.name +" completed");
			CarManager.sm_carManager.place(gameObject);
		}
        PathTriggers pt = other.gameObject.GetComponent<PathTriggers>();
        if (pt != null){
            m_goalDir = pt.m_goalDir;
        }
    }

}
