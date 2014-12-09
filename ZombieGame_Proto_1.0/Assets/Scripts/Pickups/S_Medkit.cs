using UnityEngine;
using System.Collections;

public class S_Medkit : MonoBehaviour {

	public S_PlayerController playerscript;
	
	// Use this for initialization
	void Start () {
		playerscript = GameObject.Find ("P_Player_PlaceHolder").GetComponent<S_PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
	}	
	void OnTriggerEnter (Collider col)
	{
		if(col.transform.tag == "Player" && playerscript.playerHP < 100)
		{
			playerscript.playerHP = 100;
			Destroy(gameObject);
//			playerscript.playerHP += 20;
//			if (playerscript.playerHP >= 100 );
//			{
//				playerscript.playerHP = playerscript.playerHP;
//			}
		}
	}
}
