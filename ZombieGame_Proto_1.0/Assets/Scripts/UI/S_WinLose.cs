using UnityEngine;
using System.Collections;

public class S_WinLose : MonoBehaviour {

	public TextMesh winText;

	// Use this for initialization
	void Start () {
		winText = gameObject.GetComponent<TextMesh>();

		if(PlayerPrefs.GetInt("victory") == 0){
			winText.text = "YOU LOSE";
		}
		else{
			winText.text = "YOU WIN";
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
