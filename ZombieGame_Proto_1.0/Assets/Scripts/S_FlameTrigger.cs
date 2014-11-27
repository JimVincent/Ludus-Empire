using UnityEngine;
using System.Collections;

public class S_FlameTrigger : MonoBehaviour {

	public float flamedamage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider col) {

		print (col.transform.name);

	}
}
