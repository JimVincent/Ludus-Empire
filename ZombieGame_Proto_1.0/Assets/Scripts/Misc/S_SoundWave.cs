using UnityEngine;
using System.Collections;

public class S_SoundWave : MonoBehaviour 
{
	public float scaleSpeed = 30.0f;
	public float scaleSize = 10.0f;
	public float thickness = 0.1f;
	public bool play = false;
	public GameObject soundWave;

	void Start()
	{
		// SoundWave initial settings
		soundWave = (GameObject)Instantiate(soundWave, transform.position, Quaternion.identity);
		soundWave.transform.position = new Vector3(transform.position.x, transform.position.y - transform.localScale.y, transform.position.z);
		soundWave.collider.isTrigger = true;
		soundWave.collider.tag = "SoundWave";
		soundWave.transform.parent = transform;
		soundWave.SetActive(false);
	}

	// Update is called once per frame
	void Update () 
	{
		if(play)
		{
			soundWave.SetActive(true);

			// grow soundWave x,z scale
			float xScale = soundWave.transform.localScale.x + scaleSpeed * Time.deltaTime;
			float zScale = soundWave.transform.localScale.z + scaleSpeed * Time.deltaTime;
			soundWave.transform.localScale = new Vector3(xScale, thickness, zScale);

			// reached desired size
			if(xScale >= scaleSize)
			{
				// reset object
				play = false;
				soundWave.transform.localScale = Vector3.zero;
				soundWave.SetActive(false);
			}
		}
	}

	// runs soundWave once
	public void ActivateWave()
	{
		play = true;
	}
}
