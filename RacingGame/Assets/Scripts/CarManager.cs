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
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void respawnCar(GameObject car)
    {
        Debug.Log("respawning");
        Instantiate(car, car.transform.position, Quaternion.identity);
        Destroy(car);
    }
}
