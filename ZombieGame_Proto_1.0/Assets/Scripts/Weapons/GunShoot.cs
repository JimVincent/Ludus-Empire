using UnityEngine;
using System.Collections;

public class GunShoot : MonoBehaviour {
	
	//General & Timers
	public Texture Crosshair;
	public float time;
	public GameObject Nozzle;
	public AudioClip changeWeapon;
	public Color pistolCol, machineCol, grenadeCol, flamecol;
	public GunState curState;

	private Ray ray;
	private RaycastHit hit;
	
	//Pistol
	public int pistolDamage = 25;
	public float pistolFirerate = 1;

	//Rifle
	public int machgunbullets = 70;
	public int machineDamage = 15;
	private float machineFirerate = 0.5f;	

	//Flamethrower
	public float flamefuel = 5;
	public GameObject Flametrigger;

	//Grenade Launcher
	public int grenadenumber = 3;
	public int grenadeDamage = 100;
	public GameObject grenade;
	public GameObject explosion;
	public float grenadeTimer;
	public float blastTimer, blastRadius, blastForce;
	public ParticleSystem Explode;

	private GameObject shotGrenade;
	private GameObject temp;
	private bool hasExploded = false;
	private bool grenadeReleased = false;


	//Not Needed
		//public bool outofammo;
		//public int pistolbullets = 7;
		//public bool fired = false;
		//public bool Cannotfire = false;
		//public bool timedown;
		//public LineRenderer shot;

	public enum GunState
	{
		Pistol,
		MachineGun,
		Grenade,
		Flamethrower
	};

	// Use this for initialization
	void Start () {
		ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));

        blastTimer = 0.00f;
		
//		Flametrigger = gameObject.transform.GetChild (2).gameObject;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown ("1"))
		{
			//AudioSource.PlayClipAtPoint (changeWeapon, transform.position);
			curState = GunState.Pistol;
			Flametrigger.SetActive (false);
		}

		if(Input.GetKeyDown ("2"))
		{
			//AudioSource.PlayClipAtPoint (changeWeapon, transform.position);
			curState = GunState.MachineGun;
			Flametrigger.SetActive (false);
		}

		if (Input.GetKeyDown ("3")) 
		{
			//AudioSource.PlayClipAtPoint (changeWeapon, transform.position);
			curState = GunState.Flamethrower;
		} 

		if(Input.GetKeyDown ("4"))
		{
			//AudioSource.PlayClipAtPoint (changeWeapon, transform.position);
			curState = GunState.Grenade;
			Flametrigger.SetActive (false);
		}

		if (Input.GetKeyDown (KeyCode.K))
			Application.LoadLevel("Level2"); 

		switch (curState) 
		{
		case GunState.Pistol:
			DoPistolState();
			break;
		case GunState.MachineGun:
			DoMachineGunState();
			break;
		case GunState.Grenade:
			DoGrenadeState();
			break;
		case GunState.Flamethrower:
			DoFlameState();
			break;

		}

		ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));
	
	}
	void OnGUI()
	{
		//GUI.DrawTexture(new Rect(Screen.width/2 - 50,Screen.height/2 - 50,100,100),Crosshair);
	}
			//changes for each different weapon
	void DoPistolState()
	{
				//renderer.material.color = pistolCol;
				//shot.enabled = false;

				if (Input.GetMouseButtonDown(0)) {
//						timedown = true;
						if (Physics.Raycast (transform.position, this.transform.forward, out hit, Mathf.Infinity)) {
								//Debug.DrawLine (Nozzle.transform.position, hit.point);
				print (hit.collider.tag);

//								shot.enabled = true;
//								shot.SetVertexCount (2);
//								shot.SetPosition (0, Nozzle.transform.position);
//								shot.SetPosition (1, hit.point);
						}
						if (hit.transform.tag == "Enemy") {
							hit.transform.gameObject.GetComponent<S_Zombie_Health>().OnHit(pistolDamage);	
							//hit.transform.SendMessage ("OnHit", pistolDamage, SendMessageOptions.DontRequireReceiver);
						}

//						if (pistolbullets == 0) {
//								pistolbullets = 7;
//								outofammo = true;
//								timedown = false;
//						} else
//								outofammo = false;
//						if (timedown == true) {
//								time = (time - (Firerate * Time.deltaTime));
//								if (time <= 0) {
//										pistolbullets = pistolbullets - 1;
//										fired = true;
//										timedown = false;
//										time = 1f;
//										fired = false;
//								}
//						}
//						if (fired == true){
//								time = 1;
//						}
				}
		}

	void DoMachineGunState()
	{		
		if (Input.GetMouseButton(0) && machgunbullets > 0) 
		{
			machineFirerate += Time.deltaTime;

			if (machineFirerate >= 0.5f)
			{
//				timedown = true;
				if (Physics.Raycast (transform.position, this.transform.forward, out hit, Mathf.Infinity))
				{
					print (hit.collider.tag);
				}
				if (hit.transform.tag == "Enemy") 
				{
					hit.transform.gameObject.GetComponent<S_Zombie_Health>().OnHit(machineDamage);	
					//hit.transform.SendMessage ("OnHit", machineDamage, SendMessageOptions.DontRequireReceiver);
				}

				machgunbullets --;
				machineFirerate = 0;
			}
		}
		if (Input.GetMouseButton (0) && machgunbullets <= 0) 
		{
			//play sound
		}
	}
	
	//ExplodeTarget Script
	//This is the particle effect for the explosion 
	public void ExplodeEffect (GameObject go)
	{
		Explode.enableEmission = true;
		Explode.transform.position = go.transform.position;
		Explode.Play ();
	}

	void AreaDamage (Vector3 location)
	{
		Collider[] objectInBlastRadius = Physics.OverlapSphere (location, blastRadius);
		
		foreach (Collider hit in objectInBlastRadius) 
		{
			if (hit && hit.rigidbody) 
			{
				hit.rigidbody.AddExplosionForce (blastForce, location, blastRadius, -3.0f);
			}

			if(hit && hit.transform.tag == "Enemy"){
				hit.transform.gameObject.GetComponent<S_Zombie_Health>().OnHit(grenadeDamage);
			}
		}
	}

	/*	DoGrenadeState
	 * description: Setting the damage to 50 is the base of what will be done, if the player presses lmb as well as have
     * spare ammo the player will fire a grenade that will be taken from the stock. After the timer has run out grenade will detonate. 
     * role: Player Attacking Role
     * purpose: This code's purpose is to allow the player to fire grenades to use against the zombies>
	 */
	void DoGrenadeState()

	{
			if(Input.GetMouseButtonDown(0) && grenadenumber >= 0 && !grenadeReleased)
			{
				
				shotGrenade = (GameObject)Instantiate (grenade,gameObject.transform.position+transform.forward*2, gameObject.transform.rotation);
				shotGrenade.rigidbody.AddForce (shotGrenade.transform.forward*10, ForceMode.VelocityChange);
				grenadeReleased = true;
                grenadenumber --;
            }
				if (grenadeReleased == true && hasExploded == false) 
				{
					//Grenade Timer
					blastTimer += Time.deltaTime;
					if (blastTimer >= grenadeTimer)
					{
						temp = (GameObject)Instantiate (explosion,gameObject.transform.position, gameObject.transform.rotation);
						hasExploded = true;
						blastTimer = 0.0f;
					}
				}

				if(hasExploded == true)
				{
					Destroy (temp);
					ExplodeEffect (shotGrenade);
					AreaDamage (shotGrenade.transform.position);
					shotGrenade.SetActive (false);
					Destroy (shotGrenade);
					hasExploded = false;
					grenadeReleased = false;
				}
	}
	/*	Do flame state
	 * This gives the player the ability to slip into the flamethrower weapon and use it.
	 * This code creates a trigger infront of the player that will damage and set alight enemies.
	 * if player has enough fuel they can activate the trigger when the trigger is active they lose fuel.
	 * when fuel is gone they can no longer activate trigger. 
	 */

	void DoFlameState()

	{
		if(Input.GetMouseButtonDown(0) && flamefuel > 0)
		{
			Flametrigger.SetActive (true);
		}

		if(Input.GetMouseButton(0) && flamefuel > 0)
		{
			flamefuel -= Time.deltaTime;
		}

		if (Input.GetMouseButtonUp(0) || flamefuel <= 0)
		{
			Flametrigger.SetActive (false);
		}
	}
}