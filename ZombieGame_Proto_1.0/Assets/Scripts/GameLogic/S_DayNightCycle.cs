using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class S_DayNightCycle : MonoBehaviour 
{
	public enum DayState{day, night};

	// Global Vars
	public static DayState dayState;
	public static int dayCount = 1;
	public static float tUntilNewDay;

	// public cycle vars
	public float minutesInDay = 3.20f;		// *note* 2.2 minutes = 2 mins and 20 seconds
	public float minutesInNight = 2.40f;

	// audio clips
	public AudioClip aDawnClip;
	public AudioClip aDuskClip;

	private AudioSource aSource;
	private float currentSpeed;
	private GameObject sun;
	private bool playOnce = false;

	// Use this for initialization
	void Start () 
	{
		dayState = DayState.day;
		currentSpeed = 180 / MinutesToSeconds(minutesInDay);
		transform.eulerAngles = new Vector3 (0.0f, transform.eulerAngles.y, transform.eulerAngles.z);
		tUntilNewDay = MinutesToSeconds(minutesInDay) + MinutesToSeconds(minutesInNight);

		// set up audio
		aSource = GetComponent<AudioSource>();
		aSource.loop = false;
		aSource.playOnAwake = false;
	}

	// Update is called once per frame
	void Update() 
	{
		// rotate at currentSpeed
		transform.Rotate(new Vector3(currentSpeed * Time.deltaTime, 0.0f, 0.0f));

		// track day count
		tUntilNewDay -= Time.deltaTime;
		if(tUntilNewDay <= 0.0f)
		{
			tUntilNewDay = MinutesToSeconds(minutesInDay) + MinutesToSeconds(minutesInNight);
			dayCount++;

			// play morning audio
			aSource.clip = aDawnClip;
			aSource.Play();
			playOnce = false;
		}

		// transition through states
		if(transform.eulerAngles.x > 270.0f)
		{
			dayState = DayState.night;
			currentSpeed = 180 / MinutesToSeconds(minutesInNight);

			// play night audio
			if(!playOnce)
			{
				aSource.clip = aDuskClip;
				aSource.Play();
				playOnce = true;
			}
		}
		else if(transform.eulerAngles.x < 270)
		{
			dayState = DayState.day;
			currentSpeed = 180 / MinutesToSeconds(minutesInDay);
		}
	}

	// converts minutes to seconds
	public float MinutesToSeconds(float time)
	{
		if(time % 1.0f == 0.0f)
			return time = time * 60.0f;
		else
		{
			return time = 100.0f * (time % 1.0f) + (Mathf.Floor(time)) * 60.0f;
		}
	}
}
