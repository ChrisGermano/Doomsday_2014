using UnityEngine;
using System.Collections;

public class TitleScript : MonoBehaviour {

	Ray ray;
	RaycastHit hit;
	bool loading = false;
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
			if(!loading){
				this.StartCoroutine(EndLevel());
				loading = true;
			}
		} else if (this.gameObject.name == "QuitBuilding") {
			Application.Quit();
		}
	}

	
	
	private IEnumerator EndLevel()
	{
		LoadLevel lvl = this.gameObject.GetComponent<LoadLevel>();
		bool waiting = true;
		lvl.FadeOut();
		while(waiting)
		{
			if(lvl.IsFading())
			{
				yield return 0;
			}
			else 
			{
				waiting = false;
				Application.LoadLevel("GameScene");
			}
		}
	}
}
