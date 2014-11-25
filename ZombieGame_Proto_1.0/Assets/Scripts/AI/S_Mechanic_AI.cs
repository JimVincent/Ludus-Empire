using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class S_Mechanic_AI : MonoBehaviour 
{
	public enum NPCState {start, playerIn, playerOut, end};

	public float playerDetectionRange;
	public float inRangeTalkRate;
	public float absentTalkRate;

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

	public AudioSource aSource;

	private bool madeRequest;
	private bool inRange;
	private bool talking;

	private float inTimer = 0.0f;
	private float outTimer = 0.0f;
	private NPCState state = NPCState.start;
	
	// Use this for initialization
	void Start () 
	{
		aSource = GetComponent<AudioSource>();
		aSource.playOnAwake = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// update state
		float pDist = Vector3.Distance (transform.position, S_PosTracker.playerPos);
		if(pDist < playerDetectionRange)
		{
			if(state != NPCState.start || state != NPCState.end)
				state = NPCState.playerIn;
		}
		else
		{
			if(state != NPCState.start || state != NPCState.end)
				state = NPCState.playerOut;
		}

		switch(state)
		{
		case NPCState.start:

			inTimer += Time.deltaTime;

			// wait for x seconds before intro
			if(inTimer > 3.0f)
			{
				talking = true;
				aSource.clip = openingDialogue;
				state = NPCState.playerIn;
			}
			break;

		case NPCState.playerIn:

			inTimer += Time.deltaTime;

			if(madeRequest)
			{

			}
			else
			{
				if(outTimer < absentTalkRate && inTimer > inRangeTalkRate)
				{

				}
			}

			break;

		case NPCState.playerOut:

			break;

		case NPCState.end:

			break;

		default:
			Debug.Log("Something Gone Done Broke!");
			break;
		}

		//plays loaded audio clip: reset both timers
		if(talking && !aSource.isPlaying)
		{
			talking = false;
			aSource.Play();
			inTimer = 0.0f;
			outTimer = 0.0f;
		}
	}

	// show player detection range
	void OnDrawGizmosSelected() 
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, playerDetectionRange);
	}

	// checks request and apply clip at rate
	public void CheckRequest()
	{
		// assign matching audio
		//switch(S_Replenishment.RequestedItem)


	}

	public int DiceRoll(int max)
	{
		return Random.Range(0, max);
	}


}
