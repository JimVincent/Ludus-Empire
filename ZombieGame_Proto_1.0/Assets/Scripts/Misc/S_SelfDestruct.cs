using UnityEngine;
using System.Collections;

public class S_SelfDestruct : MonoBehaviour {

	public float destructTime;
	private float timer = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer >= destructTime){
			Destroy(gameObject);
		}
	}
}
