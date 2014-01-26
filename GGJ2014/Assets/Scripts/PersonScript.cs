using UnityEngine;
using System.Collections;

public class PersonScript : MonoBehaviour {

	public int[] thoughts = new int[3];

	// Use this for initialization
	void Start () {
		for (int i = 0; i < thoughts.Length; i++) {
			thoughts[i] = Random.Range(1,3);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
