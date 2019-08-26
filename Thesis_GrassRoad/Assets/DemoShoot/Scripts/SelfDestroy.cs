using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour 
{
	//Public variables
	public float countDownTime = 1f;
	public bool isCountingDown = false;

	//Private variables
	float timer = 0f;

	
	/*-----------------------
	Unity: Update
	-----------------------*/
	void Update () 
	{
		//Count down timer
		if(isCountingDown)
		{
			timer += Time.deltaTime;

			if(timer >= countDownTime)
				Destroy(gameObject);
		}
	}


	/*-----------------------
	Start to count down
	-----------------------*/
	public void startCountDown()
	{
		isCountingDown = true;
	}
}