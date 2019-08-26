using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Magazine : MonoBehaviour 
{
	//Public variables
	public Text magazineText;
	public int capacity = 100;
	public int bulletCount = 100;
	public int magazineCount = 500;


	/*-----------------------
	Unity: Start
	-----------------------*/
	void Start () 
	{
		magazineText.text = bulletCount.ToString() + " / " + magazineCount.ToString();
	}


	/*-----------------------
	Decrease bullet count
	-----------------------*/
	public void decreaseBulletCount(int n)
	{
		bulletCount -= n;
		if(bulletCount < 0) bulletCount = 0;

		magazineText.text = bulletCount.ToString() + " / " + magazineCount.ToString();
	}


	/*-----------------------
	Reload
	-----------------------*/
	public void reload()
	{
		//1. Check magazine
		if(magazineCount <= 0) return;

		//2. Compute reload count
		int reloadCount = capacity - bulletCount;
		if(reloadCount > magazineCount) reloadCount = magazineCount;

		//3. Set bullet count
		bulletCount += reloadCount;
		magazineCount -= reloadCount;

		magazineText.text = bulletCount.ToString() + " / " + magazineCount.ToString();
	}
}
