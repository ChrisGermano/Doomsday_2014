using UnityEngine;
using System.Collections;

public class Preach : MonoBehaviour {

	public Stack followers;

	private GameObject targetPerson;
	private bool preaching;

	public int[] moves;
	private int numMoves;

	void Start () {
		followers = new Stack();
		preaching = false;
		numMoves = 0;
		moves = new int[3];
	}
	
	void Update () {

		if (preaching) {
			if (Input.GetKeyDown("space")) {
				preaching = false;
				numMoves = 0;
				moves = new int[3];
				targetPerson.GetComponentInChildren<Light>().intensity = 0f;
				targetPerson = null;
			} else if (Input.GetKeyDown ("1")) {
				moves[numMoves] = 1;
				numMoves++;
			} else if (Input.GetKeyDown ("2")) {
				moves[numMoves] = 2;
				numMoves++;
			} else if (Input.GetKeyDown ("3")) {
				moves[numMoves] = 3;
				numMoves++;
			}
		} else if (targetPerson == null) {
			Transform cam = Camera.main.transform;
			RaycastHit target;
			if (Physics.Raycast (cam.position, cam.forward, out target, 10.0f)) {
				if (target.transform.tag == "Person") {
					if (Input.GetKeyDown("space")) {
						preaching = true;
						targetPerson = target.collider.gameObject;
						targetPerson.GetComponentInChildren<Light>().intensity = 1.4f;
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
	}

	private void checkMoves(int[] ms) {
		PersonScript ps = targetPerson.GetComponent<PersonScript>();
		AIPath aips = targetPerson.GetComponent<AIPath>();

		int[] personThoughts = ps.thoughts;
		int successCounter = 0;
		for (int i = 0; i < moves.Length; i++) {
			if (moves[i] == personThoughts[i]) {
				successCounter++;
			}
		}

		if (successCounter > 1) {
			aips.Following = true;
			followers.Push(targetPerson);
		} else {
			if (followers.Count > 0) {
				GameObject popped = (GameObject)followers.Pop();
				Debug.Log ("Name: " + popped.name);
				if (popped != null) {
					Debug.Log ("AI!");
					AIPath aips2 = popped.GetComponent<AIPath>();
					aips2.Following = false;
				}
			}
		}
		targetPerson.GetComponentInChildren<Light>().intensity = 0f;
		targetPerson = null;
	}

}
