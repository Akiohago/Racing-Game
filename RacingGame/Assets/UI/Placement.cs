using UnityEngine;
using System.Collections;


public class Placement : MonoBehaviour{ 
	public Texture2D place;
	void OnGUI(){
	GUI.Box(new Rect(0,0,100,100),(place));
	}
}
