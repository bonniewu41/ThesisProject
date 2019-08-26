using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Player : MonoBehaviour 
{
	//Public variables
	public Shooting shooting;
	public Slider hpSlider;
	public float mouseSensitivity = 1f;
	public int hp = 10;
	public UnityEvent onDamagedEvent;
	public UnityEvent onDiedEvent;

	//Private variables
	Movement movement;
	Magazine magazine;

	
	/*-----------------------
	Unity: Start
	-----------------------*/
	void Start () 
	{
		movement = GetComponent<Movement>();
		magazine = GetComponent<Magazine>();
		hpSlider.value = hpSlider.maxValue = hp;
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
			doFocus();
			doReload();
		}
	}


	/*-----------------------
	Do movement from keyboard
	-----------------------*/
	void doMovement()
	{
		//1. Check if grounded
		if(!movement.grounded()) return;

		//2. Only rotate around y-axis
		Quaternion rotY = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
		Vector3 v = Input.GetAxis("Vertical") * (rotY * Vector3.forward)
				  + Input.GetAxis("Horizontal") * (rotY * Vector3.right);

		//3. Move or jump
		if(Input.GetButton("Jump")) movement.jump(v);
		else movement.move(v);		
	}


	/*-----------------------
	Do rotation from mouse
	-----------------------*/
	void doRotation()
	{
		movement.rotate(
			-mouseSensitivity * Input.GetAxis("Mouse Y"),
			 mouseSensitivity * Input.GetAxis("Mouse X")
		);
	}


	/*-----------------------
	Do shooting from mouse
	-----------------------*/
	void doShooting()
	{
		if(Input.GetMouseButton(0) && magazine.bulletCount > 0)
		{
			if(shooting.fire())
				magazine.decreaseBulletCount(1);
		}		
	}


	/*-----------------------
	Do focus from mouse
	-----------------------*/
	void doFocus()
	{
		if(Input.GetMouseButtonDown(1))
			shooting.toggleFocus();
	}


	/*-----------------------
	Do reload from keyboard
	-----------------------*/
	void doReload()
	{
		if(Input.GetButton("Reload"))
			magazine.reload();
	}


	/*-----------------------
	Player is damaged
	-----------------------*/
	public void damage(int atk)
	{
		//1. Already died
		if(hp <= 0) return;

		//2. Decrease HP
		hp -= atk;
		if(hp <= 0) hp = 0;

		hpSlider.value = hp;
		onDamagedEvent.Invoke();

		//3. Check death
		if(hp <= 0)
		{
			movement.face(transform.position + 10f * Vector3.up);
			onDiedEvent.Invoke();
		}
	}
}
