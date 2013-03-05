using UnityEngine;
using System.Collections;


public class Car_Select : MonoBehaviour{ 
	public Texture2D car;
	void OnGUI(){
	GUI.Box(new Rect(0,0,950,475),(car));
	}
}
