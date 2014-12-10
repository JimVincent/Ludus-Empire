using UnityEngine;
using System.Collections;

public class S_Flamethrower_Ammo : MonoBehaviour {

	public GunShoot gunscript;
	public float ammoAmount;
	public bool testBool = false;
	public AudioClip ammopickup,ammoFull;
	
	// Use this for initialization
	void Start () {
		gunscript = GameObject.Find ("P_Player_Gun_Placeholder").GetComponent<GunShoot> ();
	}
	
	// Update is called once per frame
	void Update () {
	}	
	void OnTriggerEnter (Collider col)
	{
		if(col.transform.tag == "Player" && gunscript.flamefuel < gunscript.maxFlameFuel && !testBool)
		{
			AudioSource.PlayClipAtPoint(ammopickup, transform.position);
			gunscript.flamefuel += ammoAmount;
			if (gunscript.flamefuel > gunscript.maxFlameFuel)
			{
				gunscript.flamefuel = gunscript.maxFlameFuel;
			}

			Destroy(gameObject);
		}
		else if(col.transform.tag == "Player" && gunscript.flamefuel >= gunscript.maxFlameFuel)
		{
			AudioSource.PlayClipAtPoint(ammoFull,transform.position);
		}
	}
}
