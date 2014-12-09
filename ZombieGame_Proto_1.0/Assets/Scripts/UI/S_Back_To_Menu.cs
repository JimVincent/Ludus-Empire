using UnityEngine;
using System.Collections;

public class S_Back_To_Menu : MonoBehaviour {

	public Color defaultColor, hoverColor;

	void Start () {
		renderer.material.color = defaultColor;
	}
	
	void OnMouseOver(){
		renderer.material.color = hoverColor;
	}
	
	void OnMouseExit(){
		renderer.material.color = defaultColor;
	}

	void OnMouseDown(){
		Application.LoadLevel("Menu");
	}
}
