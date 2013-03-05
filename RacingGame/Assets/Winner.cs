using UnityEngine;
using System.Collections;


public class Winner : MonoBehaviour{ 
	public Texture2D win;
	void OnGUI(){
	GUI.Box(new Rect(0,0,950,475),(win));
	}
}
