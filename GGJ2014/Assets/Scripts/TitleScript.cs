using UnityEngine;
using System.Collections;

public class TitleScript : MonoBehaviour {

	Ray ray;
	RaycastHit hit;

	void Start () {
	}
	
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
			Application.LoadLevel ("GameScene");
		} else if (this.gameObject.name == "QuitBuilding") {
			Application.Quit();
		}
	}
}
