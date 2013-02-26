using UnityEngine;
using System.Collections;

public class CarManager : MonoBehaviour {

    public static CarManager sm_carManager;
	public GameObject[] m_cars;
	
	// Use this for initialization
	void Start () {
		int carIndex;
        sm_carManager = this;
		if(PlayerPrefs.HasKey("selectedCar")){
			carIndex=PlayerPrefs.GetInt("selectedCar");
            Debug.Log(carIndex + "car num");
		}else{
			carIndex=0;
		}
		GameObject car= (GameObject)Instantiate(m_cars[carIndex]);
		car.tag="Player";
        foreach (GameObject aCar in m_cars){
            spawnAIcar(aCar);
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
    }

	// Update is called once per frame
	void Update () {
	
	}
    public void respawnCar(GameObject car, Vector3 dir)
    {
        Debug.Log("respawning");
        GameObject nCar=(GameObject)Instantiate(car, car.transform.position, Quaternion.identity);
        nCar.transform.Rotate(dir);
        Destroy(car);
    }
}
