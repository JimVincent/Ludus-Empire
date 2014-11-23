using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(NavMeshAgent))]
public class S_Walker_AI : MonoBehaviour 
{
	public enum State {idle, moving, active};

	// movement speeds
	public float moveSpeed = 2.0f;
	public float rotateSpeed = 1.0f;

	// move, sight, attack range
	public float sightLength = 5.0f;
	public float sightAngle = 110.0f;
	[Range(1, 10)]
	public float moveRadius = 5.0f;
	public float attackRange = 1.0f;

	// wait time
	public float idleTime = 15.0f;
	public float idleBuffer = 20.0f;
	public float searchTime = 10.0f;
	
	public State walkerState;
	private NavMeshAgent navAgent;
	private Vector3 playerPos;
	private float moveTimer = 0.0f;
	private float lookTimer = 0.0f;
	private float searchTimer = 0.0f;
	private float groanTimer = 0.0f;

	// targetPos is ranged off startPos
	private Vector3 startPos;
	private Vector3 targetPos;

	private S_SoundWave soundWave;

	// Use this for initialization
	void Start () 
	{
		walkerState = State.idle;
		startPos = transform.position;
		targetPos = NewPosition();
		navAgent = GetComponent<NavMeshAgent>();
		soundWave = GetComponent<S_SoundWave>();
		navAgent.speed = moveSpeed;
		navAgent.stoppingDistance = 1.0f;
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// always know where the player is
		playerPos = S_PosTracker.playerPos ;

		// look for player
		if(Vector3.Distance(playerPos, transform.position) <= sightLength)
		{
			// player is within sight angle with no obstructions
			Vector3 dir = playerPos - transform.position;
			float angle = Vector3.Angle(dir, transform.forward);
			RaycastHit hit;

			if(angle <= sightAngle / 2.0f)
			{
				if(Physics.Linecast(transform.position, playerPos, out hit))
				{
					// player has been seen
					if(hit.collider.tag == "Player")
					{
						targetPos = playerPos;
						moveTimer = 0.0f;
						lookTimer = 0.0f;
						walkerState = State.active;
					}
				}
			}
		}

		// state actions
		switch (walkerState)
		{
		case State.idle:	// looks around
				
			// face targetPos
			LookAtTarget();

			// look in another direction
			lookTimer += Time.deltaTime;
			if(lookTimer >= RangeWindow(idleTime / 2, idleBuffer))
			{
				lookTimer = 0.0f;
				targetPos = NewPosition();
			}

			// timer before move
			moveTimer += Time.deltaTime;
			if(moveTimer >= RangeWindow(idleTime, idleBuffer))
			{
				moveTimer = 0.0f;
				lookTimer = 0.0f;
				walkerState = State.moving;
			}

			break;

		case State.moving:	// moves towards targetPos

			if(navAgent.enabled == false)
				navAgent.enabled = true;

			// move towards targetPos
			navAgent.SetDestination(targetPos);

			// check if target reached
			if (Vector3.Distance(transform.position, targetPos) <= 1.0f)
			{
				navAgent.enabled = false;
				walkerState = State.idle;
			}

			break;

		case State.active:	// engages the player and makes noise

			if(navAgent.enabled == false)
				navAgent.enabled = true;

			// move towards player pos

			//LookAtTarget();
			//transform.localPosition += transform.forward * Time.deltaTime * moveSpeed;
			navAgent.SetDestination(targetPos);

			// cause soundWave
			groanTimer -= Time.deltaTime;
			if(groanTimer <= 0.0f)
			{
				////////////////// play audio here ///////////////////////////////////////////////
				soundWave.ActivateWave();
				groanTimer = Random.Range(1.0f, 7.0f);
			}

			// is zombie out of pursuit range
			if(Vector3.Distance(transform.position, targetPos) >= sightLength)
			{
				// go to last seen pos and set up new patrol area
				searchTimer += Time.deltaTime;
				if(searchTimer >= searchTime)
				{
					startPos = targetPos;
					searchTimer = 0.0f;
					walkerState = State.moving;
				}
			}
			// attack player
			else if(Vector3.Distance(transform.position, playerPos) <= attackRange)
			{
				/////////////////// run attack code method here ////////////////////////////////////////////////////
			}
			// player is within awareness range
			else
			{
				targetPos = playerPos;
				searchTimer = 0.0f;
			}

			break;
		}
	}

	// handle collosions
	void OnTriggerEnter(Collider otherObj)
	{
		if(otherObj.tag == "SoundWave" && walkerState != State.active)
		{
			moveTimer = 0.0f;
			lookTimer = 0.0f;
			targetPos = otherObj.transform.position;
			walkerState = State.moving;
		}
	}

	void OnDrawGizmosSelected() 
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere(targetPos, 0.5f);
	}

	// returns a random float within a higher and lower percentage of the original value
	public float RangeWindow(float value, float percentage)
	{
		return Random.Range(value - value * (percentage / 100.0f), value + value * (percentage / 100.0f));
	}

	// returns a random vector3 within the desired radius of the startPos
	public Vector3 NewPosition()
	{
		bool clearPath = false;
		Vector3 tempV;

		// only return a clear path position
		while(!clearPath)
		{
			tempV = startPos + Random.insideUnitSphere * moveRadius;

			// check if point A and B have an obstacle in the way and less than desired angle
			RaycastHit hit;
			float angle = Vector3.Angle(transform.forward, tempV - transform.position);
			if(!Physics.Linecast(transform.position, tempV, out hit) && angle < 90.0f)
			{
				clearPath = true;
				return new Vector3(tempV.x, transform.position.y, tempV.z);
			}
		}

		// Error default pos
		return transform.position;
	}

	// slowly looks faces target pos
	public void LookAtTarget()
	{
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPos - transform.position), rotateSpeed * Time.deltaTime);
	}
}