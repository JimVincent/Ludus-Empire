using UnityEngine;
using System.Collections;

public class S_PosTracker : MonoBehaviour {

	public static Vector3 playerPos;
	public static Vector3 carPos;

	public static GameObject player;
	public static GameObject car;

	// Use this for initialization
	void Awake () 
	{
		// find objects and assign vars
		car = GameObject.FindGameObjectWithTag ("Car");
		player = GameObject.FindGameObjectWithTag ("Player");
		playerPos = player.transform.position;
		carPos = car.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// track current pos
		playerPos = player.transform.position;
		carPos = car.transform.position;
	}
}
