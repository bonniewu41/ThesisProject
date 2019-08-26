using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour 
{
	//Public variables
	public Shooting shooting;
	public Transform targetTransform;
	public int hp = 3;
	public UnityEvent onDiedEvent;

	//Private variables
	Movement movement;
	SelfDestroy selfDestroy;


	/*-----------------------
	Unity: Start
	-----------------------*/
	void Start () 
	{
		movement = GetComponent<Movement>();
		selfDestroy = GetComponent<SelfDestroy>();
	}
	

	/*-----------------------
	Unity: Update
	-----------------------*/
	void Update () 
	{
		if(hp > 0)
		{
			doMovement();
			doRotation();
			doShooting();
		}	
	}


	/*-----------------------
	Do movement
	-----------------------*/
	void doMovement()
	{
		//1. Check if grounded
		if(!movement.grounded()) return;

		//2. Compute movement vector
		Vector3 v = targetTransform.position - transform.position;
		v.y = 0;
		v.Normalize();

		//3. Move or jump
		if(Random.Range(0, 100) > 1)
		{
			movement.move(
				Random.Range(-1f, 1f) * Vector3.forward
			  + Random.Range(-1f, 1f) * Vector3.right
			  + v
			);
		}
		else
			movement.jump(v);
	}


	/*-----------------------
	Do Rotation
	-----------------------*/
	void doRotation()
	{
		//Face to the target point
		movement.face(targetTransform.position);
	}


	/*-----------------------
	Do shooting
	-----------------------*/
	void doShooting()
	{
		if(Random.Range(0, 500) < 10)
			shooting.fire();
	}


	/*-----------------------
	Enemy is damaged
	-----------------------*/
	public void damage(int atk)
	{
		//1. Already died
		if(hp <= 0) return;

		//2. Decrease HP
		hp -= atk;
		if(hp <= 0) hp = 0;

		//3. Check death
		if(hp <= 0)
		{
			movement.face(transform.position + 10f * Vector3.up);
			onDiedEvent.Invoke();
			selfDestroy.startCountDown();
		}
	}
}
