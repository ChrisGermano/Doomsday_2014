using UnityEngine;
using System.Collections;


public class AIPath : MonoBehaviour {

	public float Speed = 1f;

	float rotatespeed = 1;
	bool Rotating = false;
	public bool Following = false;
	bool Preached = false;
	Animation anim;
	System.Random r;
	GameObject Player;
	AudioSource footsteps;
	AudioSource talk;

	
	public int[] thoughts = new int[3];


	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag("Player");
		r = new System.Random();
		anim = transform.Find ("model").animation;
		transform.Find("board_model").gameObject.SetActive(false);
		anim.Play ("Walk Start");
		anim.PlayQueued("Walk Loop");
		AudioSource[] aSources = GetComponents<AudioSource>();
		footsteps = aSources[0];
		talk = aSources[1];

		System.Random rand = new System.Random();
		int thought = (int)Mathf.Ceil((float)rand.NextDouble()*3);
		for (int i = 0; i < thoughts.Length; i++) {
			thoughts[i] = thought;
			thought = (int)Mathf.Ceil((float)rand.NextDouble()*3);
			
		}
		
	}
	
	// Update is called once per frame
	void Update () {

		RaycastHit target;
		bool hit_obj = Physics.Raycast (transform.position, transform.forward, out target, 5.0f);

		if(Following || Preached){
			if(Following){
				bool I_Care = false;
				if(hit_obj){
					I_Care = target.collider.gameObject.tag == "Building";
				} 
				if(I_Care){
					transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.normal * 
					                                                                                 	(5/Vector3.Distance (transform.position,target.transform.position)), 
					                                                                                 transform.up), rotatespeed*Time.deltaTime);
				}
			} 

			transform.rotation = Quaternion.Slerp(transform.rotation,
			                                      Quaternion.LookRotation(Player.transform.position - transform.position), rotatespeed*3*Time.deltaTime);

			if(Vector3.Distance(Player.transform.position, transform.position) <= 5){
				if(!anim.isPlaying){
					return;
				}
				if(anim.isPlaying && !anim.IsPlaying("Walk End")){
					anim.Play("Walk End");
				}
				return;
			}
			if(!anim.IsPlaying("Walk Loop")){
				anim.PlayQueued("Walk Loop");
			}
			//move towards the player
			transform.position += transform.forward * Speed * Time.deltaTime;
			PlayAudio();
		} else {
			if(!Rotating){
				transform.position += transform.forward * Speed * Time.deltaTime;
				PlayAudio();
				if (hit_obj) {
					Quaternion xx = Quaternion.LookRotation(target.normal, transform.up);
					StartCoroutine(RotateCharacter(xx));
					transform.Rotate (new Vector3(0, (float)(r.NextDouble()*90 - 45), 0));
				}
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
		foreach(AnimationState a in anim){
			a.speed = 0.25f;
		}
		while(returnDeltaTime < totalTime) 
		{
			transform.rotation = Quaternion.Lerp(startR, resultant, returnDeltaTime/totalTime);
			transform.Rotate(new Vector3(0, (float)(r.NextDouble() * 10 - 5), 0));
			returnDeltaTime += Time.deltaTime;
			yield return 0;
		}
		
		foreach(AnimationState a in anim){
			a.speed = 1f;
		}
		Rotating = false;
	}


	void OnDrawGizmos(){
		Gizmos.DrawRay(transform.position, transform.forward * 10);
	}

	public void SetPreached(){
		Preached = true;
		anim.PlayQueued("Walk End", QueueMode.PlayNow);
		GetComponentInChildren<Light>().intensity = 1.4f;
	}
	public void SetPreachedOff(){
		Preached = false;
		anim.PlayQueued("Walk Loop", QueueMode.PlayNow);
		GetComponentInChildren<Light>().intensity = 0f;
	}
	public void SetFollow(){
		StartCoroutine(changeModel());
		Following = true;
	}

	IEnumerator changeModel() {
		//anim.Play("Convert");
		while(anim.isPlaying) 
		{
			yield return 0;
		}
		GameObject board = transform.Find ("board_model").gameObject;
		anim = board.animation;
		board.SetActive(true);
		transform.Find("model").gameObject.SetActive(false);
	}

	private void PlayAudio(){
		if(!footsteps.isPlaying){
			int multi = 1;
			if (Random.Range (0,4) % 2 == 0) {
				multi *= -1;
			}
			footsteps.pitch = 0.5f + ((float)r.NextDouble() * multi);
			footsteps.Play();
		}

	}

	public int ValidateGuess(int[] moves){
		
		int successCounter = 0;
		for (int i = 0; i < moves.Length; i++) {
			if (moves[i] == thoughts[i]) {
				successCounter++;
			}
		}

		if(successCounter > 1){
			talk.pitch = 1.4f;
			talk.Play();
		} else {
			talk.pitch = 0.8f;
			talk.Play();
		}

		talk.pitch = 1f;

		return successCounter;
	}
}
