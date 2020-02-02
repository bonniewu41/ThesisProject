using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameControl : MonoBehaviour
{

    /* =============== Private variables =============== */
    [SerializeField]
    private GameObject spawnTargets; 

    [SerializeField]
    private GameObject sceneCamera; //initial scene camera

    [SerializeField]
    private GameObject characterCamera; //main character camera

    [SerializeField]
    private GameObject menuUI; //start button

    [SerializeField]
    private GameObject gameUI; //score, time left, crosshair

    private Timer timer;
    /* ================================================ */



    void Start()
    {
        timer = gameObject.GetComponent<Timer>(); //We find the reference of the Timer Script
        sceneCamera.SetActive(true);
        menuUI.SetActive(true);

        spawnTargets.SetActive(false);
        characterCamera.SetActive(false);
        gameUI.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {
        //In each frame it is checked whether the R key was pressed.
        if (Input.GetKeyDown(KeyCode.R))
        { 
            SceneManager.LoadScene(0); //Load scene 0. So far we only have one scene.
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            endGame();
        }
    }

    public void startGame()
    {
        timer.startTimer();
        sceneCamera.SetActive(false);
        menuUI.SetActive(false);

        spawnTargets.SetActive(true);
        characterCamera.SetActive(true);
        gameUI.SetActive(true);

    }

    public void endGame()
    {
        timer.stopTimer();
        sceneCamera.SetActive(true);
        menuUI.SetActive(true);

        spawnTargets.SetActive(false);
        characterCamera.SetActive(false);
        gameUI.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }



    //private void placePlayerRandomly()
    //{

    //    spawnPoints = GameObject.FindGameObjectsWithTag(tag); //Find all the objects in the hierarchy that have the specified tag and assign it to the spawnPoints vector.

    //    int rand = Random.Range(0, spawnPoints.Length); //Define a random number that can be between 0 and the size of spawnPoints minus 1.

    //    selectedSpawnPoint = spawnPoints[rand]; //Assign the random spawn point to the selectedSpawnPoint GameObject.

    //    player = Instantiate(playerPrefab, selectedSpawnPoint.transform.position, selectedSpawnPoint.transform.rotation); //Instantiate the GameObject playerPrefab and keep it in the GameObject player.

    //}

}

