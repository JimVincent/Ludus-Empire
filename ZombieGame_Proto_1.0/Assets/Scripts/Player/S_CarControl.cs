using UnityEngine;
using System.Collections;

public class S_CarControl : MonoBehaviour
{

	public float acceleration = 60f;
	private float steering;
    
	void Start ()
	{
		S_GameCamera.inst.target = GameObject.FindGameObjectWithTag("Player").transform;
		Debug.Log ("switched");
	}

	void FixedUpdate ()
	{
		CarControl();
	}

	void CarControl()
	{
		//make steering relative to velocity
		steering = Input.GetAxis("Vertical");

		float rot = transform.localEulerAngles.y + Input.GetAxis("Horizontal") * steering;

		//turn the gameobject
		transform.localEulerAngles = new Vector3(0.0f, rot, 0.0f);

		// apply car movement
		rigidbody.AddForce(transform.forward * Input.GetAxis("Vertical") * acceleration * 50.0f * Time.deltaTime);
	}

	void OnTriggerEnter(Collider temp)
	{
		//destroy any objects hit by the car
		temp.SendMessage ("AddDamage", 100f, SendMessageOptions.DontRequireReceiver);
	}
}
