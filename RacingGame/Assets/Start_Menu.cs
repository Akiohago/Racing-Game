using UnityEngine;
using System.Collections;


public class Start_Menu : MonoBehaviour{ 
	public Texture2D menu;
	void OnGUI(){
	GUI.Box(new Rect(0,0,950,475),(menu));
	}
}