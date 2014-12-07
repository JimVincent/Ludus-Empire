using UnityEngine;
using System.Collections;

public class S_Menu_Buttons : MonoBehaviour {
	public Color defaultColor, hoverColor;

	public enum ButtonKinds{
		start,
		controls,
		credits,
		menu
	}

	public ButtonKinds buttonType;

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
		switch(buttonType){
			case ButtonKinds.start:
			Application.LoadLevel("Level_NonRandom");
			break;

			case ButtonKinds.controls:
			Camera.main.transform.position = new Vector3(20,0,0);
			break;

			case ButtonKinds.credits:
			Camera.main.transform.position = new Vector3(40,0,0);
			break;

			case ButtonKinds.menu:
			Camera.main.transform.position = Vector3.zero;
			break;

			default:
			break;
		}
	}
}
