﻿using UnityEngine;
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

		// handle reques state
		CheckRequest ();

		// reset timers
		if(aSource.isPlaying)
		{
			inTimer = 0.0f;
			outTimer = 0.0f;
		}

		switch (state)
		{
		case NPCState.start:

			aSource.clip = openingDialogue;
			aSource.Play();

			// wait before state change
			if(!aSource.isPlaying)
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
					LoadAudio();
					aSource.Play();
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
		int randomClip = (int)Mathf.RoundToInt(Random.Range(0, clip.Length));
		return clip[randomClip];
	}

	// switches request state
	public void CheckRequest()
	{
		// check static bool request 

		if(activeRequest)// && static bool == false)
		{
			activeRequest = false;
			aSource.clip = NewAudio(thanks);
			aSource.Play();
		}
		else if(!activeRequest)// && static bool == true)
		{
			activeRequest = true;
			aSource.clip = NewAudio(getRequest);
			aSource.Play();
		}
	}

	// decides what audio to play
	public void LoadAudio()
	{
		// check state
		if(activeRequest)
			aSource.clip = NewAudio(getRequest);
		//else if(S_CarManager.isGettingFixed == false)
		//	aSource.clip = NewAudio(getParts);
		else
			aSource.clip = NewAudio(general);
	}
}
