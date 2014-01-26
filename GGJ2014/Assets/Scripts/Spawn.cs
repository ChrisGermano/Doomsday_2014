using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	public int numAI;
	public GameObject ai;

	// Use this for initialization
	void Start () {

		numAI = 50 + Random.Range (50,150);

		int coordX;
		int coordZ;

		for (int i = 0; i < numAI; i++) {
			coordX = Random.Range (25, 85);
			coordZ = Random.Range (25, 85);
			if (Random.Range(1,10) % 2 == 0)
				coordX *= -1;
			if (Random.Range(1,10) % 2 == 0)
				coordZ *= -1;
			Vector3 loc = new Vector3((float)coordX, 2f, (float)coordZ);
			GameObject tempAI = (GameObject)Instantiate(ai, loc, Quaternion.identity);
			tempAI.transform.Rotate(new Vector3(0, Random.Range (0,359), 0));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}