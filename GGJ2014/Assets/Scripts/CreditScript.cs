using UnityEngine;
using System.Collections;

public class CreditScript : MonoBehaviour {

	private float endTime;

	void Start () {
		endTime = Time.time + 10;
	}
	
	void Update () {
		float timeLeft = endTime - Time.time;

		if (timeLeft <= 0) {
			Application.LoadLevel ("TitleScene");
		}
	}
}
