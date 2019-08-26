using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour 
{
	//Public variables
	public GameObject damagePanel;
	public GameObject deathPanel;
	public GameObject enemyPrefab;
	public Text killNumText;
	public Transform targetTransform;
	public float damageTime = 0.1f;
	public float spawnInterval = 5f;
	public int maxEnemyCount = 5;
	public Transform[] spawnPoints;

	//Private variables
	bool isDamaged = false;
	float damageTimer = 0;
	float spawnTimer = 0;
	int killNum = 0;
	int enemyCount = 0;


	/*-----------------------
	Unity: Start
	-----------------------*/
	void Start () 
	{
		//Hide cursor
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		//Show killNum
		killNumText.text = killNum.ToString();
	}


	/*-----------------------
	Unity: Update
	-----------------------*/
	void Update()
	{
		//Damage timer
		if(isDamaged)
		{
			damageTimer += Time.deltaTime;

			if(damageTimer >= damageTime)
			{
				damageTimer = 0;
				damagePanel.SetActive(false);
				isDamaged = false;
			}
		}

		//Spawn timer
		if(enemyCount < maxEnemyCount)
		{
			spawnTimer += Time.deltaTime;

			if(spawnTimer >= spawnInterval)
			{
				spawnTimer = 0;
				spawn();
			}
		}
	}


	/*-----------------------
	When player damaged
	-----------------------*/
	public void playerDamaged()
	{
		//1. Set damagePanel active
		damagePanel.SetActive(true);

		//2. Start damage timer
		isDamaged = true;
	}


	/*-----------------------
	When player died
	-----------------------*/
	public void playerDied()
	{
		//1. Set deathPanel active
		deathPanel.SetActive(true);

		//2. Show cursor
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}


	/*-----------------------
	When enemy died
	-----------------------*/
	public void enemyDied()
	{
		enemyCount--;
		killNum++;
		killNumText.text = killNum.ToString();
	}


	/*-----------------------
	Restart the game
	-----------------------*/
	public void restart()
	{
		SceneManager.LoadScene("Main");
	}


	/*-----------------------
	Spawn an enemy
	-----------------------*/
	public void spawn()
	{
		//1. Randomly choose a spawn point
		Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

		//2. Create an enemy
		Enemy e = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation).GetComponent<Enemy>();
		e.targetTransform = targetTransform;
		e.onDiedEvent.AddListener(enemyDied);
		enemyCount++;
	}
}
