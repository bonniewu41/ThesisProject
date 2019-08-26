using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour 
{
	//Public variables
	public float moveSpeed = 0.2f;
	public float jumping = 10f;

	//Private variables
	bool isGrounded = false;
	float distToGround = 0f;


	/*-----------------------
	Unity: Start
	-----------------------*/
	void Start()
	{
		//Compute distance to ground
		distToGround = GetComponent<Collider>().bounds.extents.y + 0.5f;
	}


	/*-----------------------
	Unity: Update
	-----------------------*/
	void Update()
	{
		//Check grounded
		isGrounded = Physics.Raycast(transform.position, -Vector3.up, distToGround);
	}


	/*-----------------------
	Return isGrounded
	-----------------------*/
	public bool grounded()
	{
		return isGrounded;
	}


	/*-----------------------
	Move the game object
	-----------------------*/
	public void move(Vector3 v)
	{
		transform.localPosition += moveSpeed * v;
	}


	/*-----------------------
	Rotate the game object
	-----------------------*/
	public void rotate(float deltaX, float deltaY)
	{
		//1. Rotate around x-axis & y-axis
		Vector3 angles = transform.localRotation.eulerAngles;
		angles.x += deltaX;
		angles.y += deltaY;

		//2. Constrain the angle around x-axis in [0 ~ 90] or [270 ~ 360] degrees
		if(angles.x > 90f && angles.x < 270f)
		{
			if(deltaX > 0) angles.x = 90f;
			else angles.x = 270f;
		}

		//3. Assign rotation
		transform.localRotation = Quaternion.Euler(angles);
	}


	/*-----------------------
	Let the game object face 
	to a point
	-----------------------*/
	public void face(Vector3 pos)
	{
		//1. Compute the rotation to the vector facing the point
		Vector3 v = Quaternion.LookRotation(
			pos - transform.position,
			transform.up
		).eulerAngles;

		//2. Assign rotation
		transform.localRotation = Quaternion.Euler(v.x, v.y, 0);
	}


	/*-----------------------
	Let the game object jump
	-----------------------*/
	public void jump(Vector3 v)
	{
		GetComponent<Rigidbody>().velocity = jumping * (Vector3.up + v);
	}
}