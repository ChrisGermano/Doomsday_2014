using UnityEngine;
using System.Collections;


public class AIPath : MonoBehaviour {

	public float Speed = 0.1f;

	float rotatespeed = 2;
	bool Rotating = false;
	System.Random r;


	// Use this for initialization
	void Start () {
		r = new System.Random();

	}
	
	// Update is called once per frame
	void Update () {
		if(!Rotating){
			transform.position += transform.forward * Speed;
			RaycastHit target;
			if (Physics.Raycast (transform.position, transform.forward, out target, 10.0f)) {
				Quaternion xx = Quaternion.LookRotation(target.normal, transform.up);
				StartCoroutine(RotateCharacter(xx));
				transform.Rotate (new Vector3(0, (float)(r.NextDouble()*90 - 45), 0));
			}
		}

	}

	/// <summary>
	/// Co-routine to return PUC to the player
	/// </summary>
	private IEnumerator RotateCharacter(Quaternion resultant) 
	{
		float returnDeltaTime = 0;
		Rotating = true;
		Quaternion startR = transform.rotation;
		float totalTime = 1;
		while(returnDeltaTime < totalTime) 
		{
			transform.rotation = Quaternion.Lerp(startR, resultant, returnDeltaTime/totalTime);
			transform.Rotate(new Vector3(0, (float)(r.NextDouble() * 10 - 5), 0));
			returnDeltaTime += Time.deltaTime;
			yield return 0;
		}
		Rotating = false;
	}


	void OnDrawGizmos(){
		Gizmos.DrawRay(transform.position, transform.forward * 10);
	}

}
