using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour 
{
	public float PlayerRotationSpeed = 180;
	
	void Update ()
	{
		Movement();
	}

	void Movement()
	{
		transform.Rotate(Input.GetAxis("RightStick_V") * Time.deltaTime * PlayerRotationSpeed,0,0);
	}
}