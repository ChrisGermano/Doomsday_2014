using UnityEngine;
using System.Collections;

public class Preach : MonoBehaviour {

	public Stack followers;

	private GameObject targetPerson;
	private bool preaching;

	public int timeL;

	public int[] moves;
	private int numMoves;

	private float endTime;

	private int badPreaching;
	

	void Start () {
		badPreaching = 0;
		endTime = Time.time + timeL;
		followers = new Stack();
		preaching = false;
		numMoves = 0;
		moves = new int[3];
	}
	
	void Update () {
		float timeLeft = endTime - Time.time;

		if (preaching) {
			if (Input.GetKeyDown("space") || Input.GetButtonDown ("360_AButton")) {
				targetPerson.GetComponentInChildren<AIPath>().SetPreachedOff();
				preaching = false;
				numMoves = 0;
				moves = new int[3];
				targetPerson = null;
			} else if (Input.GetKeyDown ("1") || Input.GetButtonDown ("360_XButton")) {
				moves[numMoves] = 1;
				numMoves++;
			} else if (Input.GetKeyDown ("2") || Input.GetButtonDown ("360_YButton")) {
				moves[numMoves] = 2;
				numMoves++;
			} else if (Input.GetKeyDown ("3") || Input.GetButtonDown ("360_BButton")) {
				moves[numMoves] = 3;
				numMoves++;
			}
		} else if (targetPerson == null) {
			Transform cam = Camera.main.transform;
			RaycastHit target;
			if (Physics.Raycast (cam.position, cam.forward, out target, 10.0f)) {
				if (target.transform.tag == "Person") {
					if (Input.GetKeyDown("space") || Input.GetButtonDown ("360_AButton")) {
						preaching = true;
						targetPerson = target.collider.gameObject;
						targetPerson.GetComponentInChildren<AIPath>().SetPreached();
					}
				}
			}
		}

		if (numMoves == 3) {
			checkMoves(moves);
			numMoves = 0;
			preaching = false;
			moves = new int[3];
		}

		if (timeLeft <= 0) {
			if (followers.Count >= (GetComponent<Spawn>().numAI / 10)) {
				GetComponent<EndGame>().doomsday = true;
				RenderSettings.fogColor = new Color(0f,0f,0f);
			} else {
				GetComponent<EndGame>().doomsday = false;
				RenderSettings.fogColor = new Color(1f,1f,1f);
			}
			if (GetComponent<EndGame>().startT == 0)
				GetComponent<EndGame>().startT = endTime;
			GetComponent<EndGame>().EndUpdate();
		}

	}

	private void checkMoves(int[] ms) {
		AIPath aips = targetPerson.GetComponent<AIPath>();
		int successCounter = aips.ValidateGuess(ms);

		if (successCounter > 1) {
			aips.SetFollow();
			followers.Push(targetPerson);
			badPreaching = 0;
		} else {
			if (followers.Count > 0) {
				badPreaching++;
				if (badPreaching == 2) {
					GameObject popped = (GameObject)followers.Pop();
					if (popped != null) {
						AIPath aips2 = popped.GetComponent<AIPath>();
						aips2.Following = false;
					}
					badPreaching = 0;
				}
			}
		}
		aips.SetPreachedOff();
		targetPerson = null;
	}
}
