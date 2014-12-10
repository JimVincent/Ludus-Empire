using UnityEngine;
using System.Collections;

public class S_Car_Maintenance : MonoBehaviour 
{
	// static variables
	public static float carHealth = 10.0f;
	public static bool fixing = false;
	public static bool fullyRepaired = false;

	// inspector variables
	public float fixRate = 2.0f;
	public float fixValue = 0;

	private AudioSource aSource;

	public void Start(){
		aSource = gameObject.GetComponent<AudioSource>();
	}
	
	public void Update()
	{

		// update HUD inst
		S_HUD_Manager.inst.carHealth = carHealth;
		
		// is car fully repaired
		if(carHealth >= 100)
		{
			fullyRepaired = true;
			aSource.Stop();
		}
		else
		{
			// is car being fixed
			if(fixValue > 0 && !S_Mechanic_AI.activeRequest)
			{
				fixing = true;
				aSource.Play();
				
				// add value to carHealth over time
				carHealth += fixRate * Time.deltaTime;
				fixValue -= fixRate * Time.deltaTime;
			}
			else
			{
				aSource.Stop();
			}

			if(fixValue < 0)
			{
				fixValue = 0;
				fixing = false;
			}
		}
	}
	
	// adds part value to mechanics fix cue
	public void FixCar(float partValue)
	{
		fixValue += partValue;
	}
}
