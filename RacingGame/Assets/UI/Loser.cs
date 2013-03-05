using UnityEngine;
using System.Collections;


public class Loser : MonoBehaviour{ 
	public Texture2D lose;
	void OnGUI(){
	GUI.Box(new Rect(0,0,950,475),(lose));
	}
}
