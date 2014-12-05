using UnityEngine;
using System.Collections;

public class S_Trash_Can : MonoBehaviour {

	public float smallWaveSize;
	public float largeWaveSize;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollision (Collision col){
		gameObject.GetComponent<S_SoundWave>().scaleSize = smallWaveSize;
		gameObject.GetComponent<S_SoundWave>().ActivateWave();
	}

	public void OnHit(){
		gameObject.GetComponent<S_SoundWave>().scaleSize = largeWaveSize;
		gameObject.GetComponent<S_SoundWave>().ActivateWave();
	}
}
