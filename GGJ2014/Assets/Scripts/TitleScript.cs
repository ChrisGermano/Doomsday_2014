using UnityEngine;
using System.Collections;

public class TitleScript : MonoBehaviour {

	Ray ray;
	RaycastHit hit;

	void Start () {
	}
	
	void FixedUpdate () {
		if (Input.GetButtonDown("360_StartButton")) {
			Application.LoadLevel("GameScene");
		} else if (Input.GetButtonDown("360_BackButton")) {
			Application.Quit();
		}
	}

	void OnMouseOver() {
		renderer.material.color = new Color (0.7f,0.45f,0.27f);
	}

	void OnMouseExit() {
		renderer.material.color = new Color(0.24f,0.28f,0.44f);
	}

	void OnMouseDown() {
		if (this.gameObject.name == "PlayBuilding") {
			Application.LoadLevel("GameScene");
		} else if (this.gameObject.name == "QuitBuilding") {
			Application.Quit();
		}
	}
}
