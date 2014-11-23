using UnityEngine;
using System.Collections;

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
	public float lightIntensity = 0.5f;

	private float currentSpeed;
	private GameObject sun;

	// Use this for initialization
	void Start () 
	{
		dayState = DayState.day;
		currentSpeed = 180 / MinutesToSeconds(minutesInDay);
		transform.eulerAngles = new Vector3 (0.0f, transform.eulerAngles.y, transform.eulerAngles.z);
		tUntilNewDay = MinutesToSeconds(minutesInDay) + MinutesToSeconds(minutesInNight);

		// set up sun
		sun = GameObject.CreatePrimitive(PrimitiveType.Cube);
		sun.renderer.enabled = false;
		sun.collider.enabled = false;
		sun.AddComponent<Light>();
		sun.light.type = LightType.Directional;
		sun.light.intensity = lightIntensity;
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
		}

		// transition through states
		if(transform.eulerAngles.x > 270.0f)
		{
			dayState = DayState.night;
			currentSpeed = 180 / MinutesToSeconds(minutesInNight);
		}
		else if(transform.eulerAngles.x < 270)
		{
			dayState = DayState.day;
			currentSpeed = 180 / MinutesToSeconds(minutesInDay);
		}

		///////////////////HACK TO CHANGE DAY / NIGHT//////////
		///////////////////////////////////////////////////////
		if(Input.GetKeyDown(KeyCode.P))		///////////////////	
		{									///////////////////
			if(dayState == DayState.day)	///////////////////	
				dayState = DayState.night;	///////////////////
			else 							///////////////////
				dayState = DayState.day;	///////////////////
		}									///////////////////
		///////////////////////////////////////////////////////
		///////////////////////////////////////////////////////
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
