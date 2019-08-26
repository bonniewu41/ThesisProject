using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	//Public variables
	public GameObject explosionPrefab;
	public int atk = 1;


	/*-----------------------
	Unity Event: on collision
	enter
	-----------------------*/
	void OnCollisionEnter(Collision other)
	{
		//1. Create explosion GameObject
		Instantiate(explosionPrefab, transform.position, transform.rotation);

		//2. Enemy is damaged
		Enemy e = other.gameObject.GetComponent<Enemy>();
		if(e != null) e.damage(atk);

		//3. Player is damaged
		Player p = other.gameObject.GetComponent<Player>();
		if(p != null) p.damage(atk);

		//4. Self destroy
		Destroy(gameObject);
	}
}
