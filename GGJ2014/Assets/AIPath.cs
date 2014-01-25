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

		transform.Translate (transform.forward * Speed);

	}

	void OnTriggerEnter(Collider c){
		double y = r.NextDouble() * 360;
		transform.Rotate(new Vector3(0, (float)y, 0));
	}
}
