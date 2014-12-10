using UnityEngine;
using System.Collections;

public class S_Medkit : MonoBehaviour {

	public S_PlayerController playerscript;
	public AudioClip medkitSFX;
	
	// Use this for initialization
	void Start () {
		playerscript = GameObject.Find ("P_Player_PlaceHolder").GetComponent<S_PlayerController> ();
	}

	void OnTriggerEnter (Collider col)
	{
		if(col.transform.tag == "Player" && playerscript.playerHP < 100)
		{
			AudioSource.PlayClipAtPoint(medkitSFX, transform.position);
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
