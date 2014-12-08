using UnityEngine;
using System.Collections;

public class S_GameCamera : MonoBehaviour
{

	private Vector3 cameraTarget;

	public Transform target;

	public static S_GameCamera inst;

	void Awake()
	{
		inst = this;
	}
	
	void Start ()
	{
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update () {
		cameraTarget = new Vector3(target.position.x,transform.position.y,target.position.z - 5);
		transform.position = Vector3.Lerp(transform.position,cameraTarget,Time.deltaTime * 8);
	}
}
