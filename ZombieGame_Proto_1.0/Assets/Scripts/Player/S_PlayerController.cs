using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class S_PlayerController : MonoBehaviour
{
	public static S_PlayerController inst;

	//game objects
	public GameObject player;
	public GameObject car;

	//character vars
	public float playerHP = 100;
	public bool inCar = false;
	public float zombieDamage;

	//control
	public float rotationSpeed = 500;
	public float walkSpeed = 6;
	public float runSpeed = 8;

	//camera
	private Quaternion targetRotation;
	private CharacterController controller;
	private Camera cam;

	//car
	public float acceleration = 60f;
	private float steering;
	
	public GameObject primaryWeaponBullet;

	void Awake()
	{
		inst = this;
	}

	void Start ()
	{
		controller = GetComponent<CharacterController>();
		cam = Camera.main;
	}

	void FixedUpdate ()
	{
		// update HUD inst
		S_HUD_Manager.inst.playerHealth = playerHP;

		if (inCar == false)
		{
			ControlPlayer();
		}

		if (inCar == true)
		{
			player.SetActive (false);
			car.SetActive (true);
		}
	
	//player death (place holder)
		if (playerHP <= 0) 
		{
			PlayerPrefs.SetInt("victory",0);
			Application.LoadLevel("End_Game");
		}

	//fire primary weapon (place holder)
//		if (Input.GetMouseButtonDown(0) && playerAmmo > 0)
//		{
//			Vector3 position = new Vector3(transform.position.x,transform.position.y,transform.position.z);
//			//TODO create a bullet to be fired
//			//Instantiate(primaryWeaponBullet,position,transform.rotation);
//			playerAmmo -= 1;
//		}

	}

	#region Player
	//faces the character towards the mouse position
	void ControlPlayer()
	{
		Vector3 mousePos = Input.mousePosition;
		mousePos = cam.ScreenToWorldPoint(new Vector3(mousePos.x,mousePos.y,cam.transform.position.y - transform.position.y));
		targetRotation = Quaternion.LookRotation(mousePos - new Vector3(transform.position.x,0,transform.position.z));
		transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y,targetRotation.eulerAngles.y,rotationSpeed * Time.deltaTime);

	//smooths the character movement and scales the strafe speed down to normal
		Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
		Vector3 motion = input;
		motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1)?.7f:1;
		motion *= (Input.GetButton("Run"))?runSpeed:walkSpeed;
		motion += Vector3.up * -8;
		
		controller.Move(motion * Time.deltaTime);
	}
	#endregion

	#region car control
	void ControlCar()
	{
		//make steering relative to velocity
		steering = Input.GetAxis("Vertical");
		
		float rot = transform.localEulerAngles.y + Input.GetAxis("Horizontal") * steering;
		
		//turn the gameobject
		transform.localEulerAngles = new Vector3(0.0f, rot, 0.0f);
		
		// apply car movement
		rigidbody.AddForce(transform.forward * Input.GetAxis("Vertical") * acceleration * 50.0f * Time.deltaTime);
	}
	#endregion

	void OnTriggerEnter(Collider temp)
	{
		//destroy any objects hit by the car
		temp.SendMessage ("AddDamage", 100f, SendMessageOptions.DontRequireReceiver);

		if(temp.tag == "Attack"){
			playerHP -= zombieDamage;
		}
	}


	#region send messages
	//send message place holders
		void OnBumpIntoEnemy(float dmg)
		{
			//playerHP -= dmg;
		}

		void OnKillEnemy(float score)
		{
			//update minimap / HUD
		}
		
		void OnPickup(float supply)
		{
			//add ammo / hp etc
		}
	#endregion
}
