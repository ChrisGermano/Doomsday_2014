using UnityEngine;
using System.Collections;

public class Preach : MonoBehaviour {

	public Stack followers;

	private GameObject targetPerson;
	private bool preaching;

	public int[] moves;
	private int numMoves;

	// Use this for initialization
	void Start () {
		followers = new Stack();
		preaching = false;
		numMoves = 0;
		moves = new int[3];
	}
	
	// Update is called once per frame
	void Update () {

		if (preaching) {
			if (Input.GetKeyDown("space")) {
				preaching = false;
				numMoves = 0;
				moves = new int[3];
				targetPerson.GetComponentInChildren<Light>().intensity = 0f;
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
		} else {
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
		//PersonScript ps = targetPerson.GetComponent<PersonScript>();
		//int[] personThoughts = ps.thoughts;
		int successCounter = 0;
		for (int i = 0; i < moves.Length; i++) {
			//if (moves[i] == personThoughts[i]) {
				successCounter++;
			//}
		}

		if (successCounter > 1) {
			targetPerson.GetComponentInChildren<Light>().intensity = 0f;
			followers.Push(targetPerson);
		} else {
			if (followers.Count > 1) {
				followers.Pop();
			}
		}
	}

}
