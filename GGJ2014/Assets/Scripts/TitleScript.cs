using UnityEngine;
using System.Collections;

public class TitleScript : MonoBehaviour {

	Ray ray;
	RaycastHit hit;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnMouseOver() {
		renderer.material.color = new Color (0.2f,0.2f,0.2f);
	}

	void OnMouseExit() {
		renderer.material.color = new Color(0.6f,0.6f,0.6f);
	}

	void OnMouseDown() {
		if (this.gameObject.name == "PlayBuilding") {
			Application.LoadLevel ("DemoScene");
		} else if (this.gameObject.name == "QuitBuilding") {
			Application.Quit();
		}
	}
}
