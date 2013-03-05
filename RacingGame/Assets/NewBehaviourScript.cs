using UnityEngine;
using System.Collections;


public class NewBehaviourScript : MonoBehaviour{ 
	public Texture2D test;
	void OnGUI(){
	GUI.Box(new Rect(0,415,250,100),(test));
	}
}