using UnityEngine;
using System.Collections;

public class S_Grenade_Ammo : MonoBehaviour {

	public GunShoot gunscript;

	// Use this for initialization
	void Start () {
		gunscript = GameObject.Find ("vincent").GetComponent<GunShoot> ();
	}
	
	// Update is called once per frame
	void Update () {
	}	
	void OnTriggerEnter (Collider col)
	{
		if(col.transform.tag == "Player" && gunscript.grenadenumber <= 3)
		{
			gunscript.grenadenumber ++;
		}
	}
}
