using UnityEngine;
using System.Collections;

public class S_Grenade_Explosion : MonoBehaviour {

	public GameObject explodeFX;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDestroy(){
		Instantiate(explodeFX, transform.position, Quaternion.identity);
	}
}
