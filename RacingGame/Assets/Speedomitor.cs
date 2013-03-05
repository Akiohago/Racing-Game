using UnityEngine;
using System.Collections;


public class Speedomitor : MonoBehaviour{ 
	public Texture2D Speed;
	void OnGUI(){
	GUI.Box(new Rect(825,382,100,100),(Speed));
	}
}
