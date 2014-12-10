using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class S_Mechanic_AI : MonoBehaviour 
{
	public enum NPCState {start, inGame, end};

	public static bool carUnderAttack;

	public float playerDetectionRange;
	public float inRangeTalkRate;
	public float absentTalkRate;		// says something when back in range

	public AudioClip[] general;
	public AudioClip[] getParts;
	public AudioClip[] getRequest;
	public AudioClip[] gettingAttacked;
	public AudioClip[] thanks;

	public AudioClip openingDialogue;
	public AudioClip closingDialogue;

	private AudioSource aSource;
	private bool activeRequest;
	private float inTimer = 0.0f;
	private float outTimer = 0.0f;
	private NPCState state = NPCState.start;
	private bool playOnce = false;
	
	// Use this for initialization
	void Start () 
	{
		// set up audio source
		aSource = GetComponent<AudioSource>();
		aSource.playOnAwake = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float pDist = Vector3.Distance (transform.position, S_PosTracker.playerPos);

		// reset timers
		if(aSource.isPlaying)
		{
			inTimer = 0.0f;
			outTimer = 0.0f;
		}

		switch (state)
		{
		case NPCState.start:

			if(!playOnce)
			{
				aSource.clip = openingDialogue;
				aSource.Play();
				playOnce = true;
			}
			// wait before state change
			if(!aSource.isPlaying)
			{
				state = NPCState.inGame;
				playOnce = false;
			}
			break;

		case NPCState.inGame:

			// track in and out time
			if(pDist < playerDetectionRange && S_DayNightCycle.dayState == S_DayNightCycle.DayState.day)
			{
				inTimer += Time.deltaTime;
				carUnderAttack = false;

				// handle request state
				CheckRequest ();
				
				// play audio
				if(inTimer > inRangeTalkRate)
				{
					LoadAudio();
					aSource.Play();
				}
			}
			else if(pDist < playerDetectionRange && S_DayNightCycle.dayState == S_DayNightCycle.DayState.night)
			{
				inTimer += Time.deltaTime;
				
				// play audio
				if(inTimer > inRangeTalkRate)
				{
					if(carUnderAttack)
					{
						aSource.clip = NewAudio(gettingAttacked);
						aSource.Play();
					}
				}
			}

			// player out of range
			if(pDist > playerDetectionRange && S_DayNightCycle.dayState == S_DayNightCycle.DayState.day)
				outTimer += Time.deltaTime;

			if(pDist < playerDetectionRange && S_Car_Maintenance.fullyRepaired){
				GameObject.Find("P_Player_PlaceHolder").GetComponent<S_PlayerController>().inCar = true;
			}

			break;

		case NPCState.end:

			if(!aSource.isPlaying)
			{
				if(!playOnce)
				{
					aSource.clip = closingDialogue;
					aSource.Play();
					playOnce = true;
				}
			}
			break;
		}
	}

	// show player detection range
	void OnDrawGizmosSelected() 
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, playerDetectionRange);
	}

	// plays random audio
	public AudioClip NewAudio(AudioClip[] clip)
	{
		int randomClip = (int)Mathf.RoundToInt(Random.Range(0, clip.Length));
		return clip[randomClip];
	}

	// switches request state
	public void CheckRequest()
	{



//		if(activeRequest)// && static bool == false)
//		{
//			activeRequest = false;
//			aSource.clip = NewAudio(thanks);
//			aSource.Play();
//		}
//		else if(!activeRequest)// && static bool == true)
//		{
//			activeRequest = true;
//			aSource.clip = NewAudio(getRequest);
//			aSource.Play();
//		}
	}

	// decides what audio to play
	public void LoadAudio()
	{
		// check state
		if(activeRequest)
			aSource.clip = NewAudio(getRequest);
		else if(!S_Car_Maintenance.fixing)
			aSource.clip = NewAudio(getParts);
		else
			aSource.clip = NewAudio(general);
	}
}
