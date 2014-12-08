using UnityEngine;
using System.Collections;

public class S_Flamethrower_Ammo : MonoBehaviour {

	public GunShoot gunscript;
	public float ammoAmount;
	public bool testBool = false;
	
	// Use this for initialization
	void Start () {
		gunscript = GameObject.Find ("P_Player_PlaceHolder").GetComponentInChildren<GunShoot> ();
	}
	
	// Update is called once per frame
	void Update () {
	}	
	void OnTriggerEnter (Collider col)
	{
		if(col.transform.tag == "Player" && gunscript.flamefuel < gunscript.maxFlameFuel && !testBool)
		{
			gunscript.flamefuel += ammoAmount;
			if (gunscript.flamefuel > gunscript.maxFlameFuel)
			{
				gunscript.flamefuel = gunscript.maxFlameFuel;
			}

			Destroy(gameObject);
		}
	}
}
