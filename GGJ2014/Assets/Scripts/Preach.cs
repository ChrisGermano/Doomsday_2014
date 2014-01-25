using UnityEngine;
using System.Collections;

public class Preach : MonoBehaviour {

	private GameObject targetPerson;
	private bool preaching;

	// Use this for initialization
	void Start () {
		preaching = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!preaching) {
			Debug.Log ("Not preaching");
			Transform cam = Camera.main.transform;
			RaycastHit target;
			if (Physics.Raycast (cam.position, cam.forward, out target, 20.0f)) {
				//Debug.Log (target.transform.name);
				Vector3 oldCamPos = cam.position;
				cam.Translate(cam.forward * 20.0f);
				Debug.DrawLine (oldCamPos, cam.position);
			}
		}

	}
}
