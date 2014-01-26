using UnityEngine;
using System.Collections;

public class CreditScript : MonoBehaviour {

	private float endTime;

	void Start () {
		endTime = Time.time + 10;
		UnityEngine.Resolution res = Screen.currentResolution;
		int height = res.height;
		int width = res.width;
		GameObject credits = GameObject.Find("Credits");
		TextMesh[] names = credits.GetComponentsInChildren<TextMesh>();
		float c_width = 0;
		float c_height = 0;

		foreach(TextMesh name in names){
			float w = name.renderer.bounds.size.x;
			float h = name.renderer.bounds.size.z;
			if(w > c_width){
				c_width = w;
			}

			c_height += h;
			c_height += 10;
		}
		c_height -= 10;


		credits.transform.position = new Vector3(0 - c_width/2, 
		                                         credits.transform.position.y, 
		                                         0);
	}
	
	void Update () {
		float timeLeft = endTime - Time.time;

		if (timeLeft <= 0) {
			Application.LoadLevel ("TitleScene");
		}
	}
}
