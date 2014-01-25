using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour 
{
	public float PlayerMovementSpeed = 30;
	public float PlayerRotationSpeed = 180;
	
	void Update ()
	{
		Movement();
	}

	void Movement()
	{
		transform.Translate(0,0,Input.GetAxis("Vertical") * Time.deltaTime * PlayerMovementSpeed);
		
		transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * PlayerMovementSpeed,0,0);
		
		transform.Rotate(0,Input.GetAxis("RightStick_H") * Time.deltaTime * PlayerRotationSpeed,0);
	}
}