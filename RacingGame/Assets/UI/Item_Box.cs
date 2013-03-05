using UnityEngine;
using System.Collections;


public class Item_Box : MonoBehaviour{ 
	public Texture2D item;
	void OnGUI(){
	GUI.Box(new Rect(750,395,75,75),(item));
	}
}