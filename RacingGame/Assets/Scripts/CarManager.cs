using UnityEngine;
using System.Collections;

public class CarManager : MonoBehaviour {
	
	public GameObject[] m_cars;
	
	// Use this for initialization
	void Start () {
		int carIndex;
		if(PlayerPrefs.HasKey("selectedCar")){
			carIndex=PlayerPrefs.GetInt("selectedCar");
		}else{
			carIndex=0;
		}
		GameObject car= (GameObject)Instantiate(m_cars[carIndex]);
		car.tag="Player";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
