using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class S_Mechanic_AI : MonoBehaviour 
{
	public enum NPCState {start, inGame, end};

	public float playerDetectionRange;
	public float inRangeTalkRate;
	public float absentTalkRate;		// says something when back in range

	public AudioClip[] general;
	public AudioClip[] getParts;
	public AudioClip[] gettingAttacked;
	public AudioClip[] thanks;

	public AudioClip openingDialogue;
	public AudioClip closingDialogue;

	public AudioClip needCoffee;
	public AudioClip needTobacco;
	public AudioClip needTape;
	public AudioClip needScrewDriver;
	public AudioClip needMagazine;

	private AudioSource aSource;

	private bool activeRequest;

	private float inTimer = 0.0f;
	private float outTimer = 0.0f;
	private NPCState state = NPCState.start;
	
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

		switch (state)
		{
		case NPCState.start:

			aSource.clip = openingDialogue;
			aSource.Play();
			state = NPCState.inGame;
			break;

		case NPCState.inGame:

			// track in and out time
			if(pDist < playerDetectionRange && S_DayNightCycle.dayState == S_DayNightCycle.DayState.day)
			{
				inTimer += Time.deltaTime;
				
				// play audio
				if(inTimer > inRangeTalkRate)
				{
					if(!activeRequest)

				}
			}
			else if(pDist > playerDetectionRange && S_DayNightCycle.dayState == S_DayNightCycle.DayState.day)
				outTimer += Time.deltaTime;

			break;

		case NPCState.end:

			if(!aSource.isPlaying)
			{
				aSource.clip = closingDialogue;
				aSource.Play();
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
		inTimer = 0.0f;
		outTimer = 0.0f;

		int randomClip = (int)Mathf.RoundToInt(Random.Range(0, clip.Length));
		return clip[randomClip];
	}
}
