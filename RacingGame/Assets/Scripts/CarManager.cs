using UnityEngine;
using System.Collections;

public class CarManager : MonoBehaviour {

    public static CarManager sm_carManager;
	public GameObject[] m_cars;
	public string[] m_places;
	private int m_carsFinished=0;
	
	// Use this for initialization
	void Start () {
		int carIndex;
		m_places=new string[m_cars.Length];
        sm_carManager = this;
		if(PlayerPrefs.HasKey("selectedCar")){
			carIndex=PlayerPrefs.GetInt("selectedCar");
            Debug.Log(carIndex + "car num");
		}else{
			carIndex=0;
		}
		for(int i=0;i<m_cars.Length;i++){ //each (GameObject aCar in m_cars){
			if(i==carIndex){
				GameObject car= (GameObject)Instantiate(m_cars[carIndex]);
		        car.tag="Player";
			}else{
            	spawnAIcar(m_cars[i]);
			}
        }
		
	}
    void spawnAIcar(GameObject car){
        Debug.Log("spawning"+ car.name);
        GameObject aCar= (GameObject)Instantiate(car);
        RigidBodyCar rc = (RigidBodyCar)aCar.GetComponent<RigidBodyCar>();
        float maxSpeed = rc.m_maxSpeed;
        float acc = rc.m_acceleration;
        float turnSpeed = rc.m_turnSpeed;
        float brake = rc.m_breakAmount;
		AudioClip accAudio=rc.m_accelerationAudio;
		AudioClip decelAudio=rc.m_breakAudio;
		AudioClip turnAudio=rc.m_turnAudio;
		
        Destroy(rc);
        foreach (Transform tran in rc.transform)
        {

            if (tran.gameObject.GetComponent<Camera>() != null){
                Destroy(tran.gameObject);
            }
        }
        aiCar ac = (aiCar)aCar.AddComponent<aiCar>();
        ac.m_maxSpeed = maxSpeed;
        ac.m_acceleration = acc;
        ac.m_turnSpeed = turnSpeed;
        ac.m_breakAmount = brake;
		ac.m_accelerationAudio=accAudio;
		ac.m_breakAudio=decelAudio;
		ac.m_turnAudio=turnAudio;
    }

	// Update is called once per frame
	void Update () {
	
	}
    public void respawnCar(GameObject car)
    {
        Debug.Log("respawning");
		RigidBodyCar carInfo=(RigidBodyCar)car.GetComponent<RigidBodyCar>();
        GameObject nCar=(GameObject)Instantiate(car, carInfo.m_lastCheckPoint, Quaternion.Euler(carInfo.m_goalDir));
		nCar.name=car.name;
        Destroy(car);
    }
	public void place(GameObject car){
		m_places[m_carsFinished]=car.name;
		Destroy(car);
		m_carsFinished++;
		if(m_carsFinished==m_places.Length){
			Application.LoadLevel("Start_Menu");
		}
	}
}
