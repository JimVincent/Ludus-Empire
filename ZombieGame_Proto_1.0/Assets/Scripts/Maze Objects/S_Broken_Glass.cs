using UnityEngine;
using System.Collections;

public class S_Broken_Glass : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if(col.tag == "Player"){
			this.gameObject.GetComponent<S_SoundWave>().ActivateWave();
		}
	}
}
