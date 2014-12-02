using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class S_PlayerController : MonoBehaviour
{
	//character
	public float playerHP = 100;

	//control
	public float rotationSpeed = 500;
	public float walkSpeed = 6;
	public float runSpeed = 8;

	//camera
	private Quaternion targetRotation;
	private CharacterController controller;
	private Camera cam;
	
	public GameObject primaryWeaponBullet;

	void Start ()
	{
		controller = GetComponent<CharacterController>();
		cam = Camera.main;
	}

	void Update ()
	{
		ControlMouse();
	
	//player death (place holder)
		if (playerHP <= 0) 
		{
			//Destroy (gameObject);
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
	//faces the character towards the mouse position
	void ControlMouse()
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

	void OnTriggerEnter(Collider otherObj)
	{
		if(otherObj.tag == "ZombieAttack")
			print ("Hit Received");
	}

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
}
