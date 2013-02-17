using UnityEngine;
using System.Collections;

public class CarSelector : MonoBehaviour {
	
	public GameObject[] m_cars;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			RaycastHit hitInfo;
			Ray ray=Camera.mainCamera.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hitInfo,100)){
				Debug.Log(hitInfo.collider.gameObject.name+ "tapped");
				for(int i=0; i<m_cars.Length; i++){
					if(hitInfo.collider.gameObject.transform){
						PlayerPrefs.SetInt("selectedCar",i);
						Application.LoadLevel("GamePlay");
					}
				}
			}
		}
	}
}
