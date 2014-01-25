using UnityEngine;
using System.Collections;


public class AIPath : MonoBehaviour {

	public float Speed = 0.1f;
	
	System.Random r;

	// Use this for initialization
	void Start () {
		r = new System.Random();

	}
	
	// Update is called once per frame
	void Update () {

		transform.Translate (Vector3.forward * Speed);
		

	}


}
