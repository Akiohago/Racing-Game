using UnityEngine;
using System.Collections;

public class CarSelector : MonoBehaviour {
	public Texture2D choosecar2;
	public GameObject[] m_cars;
	/*void OnGUI(){
		GUI.Box(new Rect(0,0,950,475),((choosecar2)));
		Update ();
	}*/
	
	// Use this for initialization
	//void Start() {

	//}
	
	// Update is called once per frame
	void OnGUI(){
		GUI.Box(new Rect(0,0,950,475),((choosecar2)));
		if(Input.GetMouseButtonDown(0)){
			RaycastHit hitInfo;
			Ray ray=Camera.mainCamera.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hitInfo,100)){
				Debug.Log(hitInfo.collider.gameObject.name+ "tapped");
				for(int i=0; i<m_cars.Length; i++){
                    Debug.Log(hitInfo.collider.gameObject.name);
					if(hitInfo.collider.gameObject.transform==m_cars[i].transform){
                        Debug.Log(i + " clicked");
						PlayerPrefs.SetInt("selectedCar",i);
						Application.LoadLevel("GamePlay");
					}
				}
			}
		}
	}
}
