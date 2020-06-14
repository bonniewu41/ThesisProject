using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;


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

    [SerializeField]
    private AudioSource gameMusic;

    private Timer timer;
    


    PlayerControls controls;
    /* ================================================ */


    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.End.performed += context => endGame();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }


    void Start()
    {
        timer = gameObject.GetComponent<Timer>(); //We find the reference of the Timer Script
        //scenecamera.setactive(false);
        //menuui.setactive(false);


        //spawntargets.setactive(true);
        //charactercamera.setactive(true);
        //gameui.setactive(true);
        timer.startTimer();
        gameMusic.Play();

        Cursor.lockState = CursorLockMode.Locked;

        //sceneCamera.SetActive(true);
        //menuUI.SetActive(true);

        //spawnTargets.SetActive(false);
        //characterCamera.SetActive(false);
        //gameUI.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {
        ////In each frame it is checked whether the R key was pressed.
        //if (Input.GetKeyDown(KeyCode.R))
        //{ 
        //    SceneManager.LoadScene(0); //Load scene 0. So far we only have one scene.
        //}

        //if (Input.GetKeyDown(KeyCode.Escape))
        //{ 
        //    endGame();
        //}
    }

    //public void startGame()
    //{
    //    timer.startTimer();
    //    sceneCamera.SetActive(false);
    //    menuUI.SetActive(false);

    //    spawnTargets.SetActive(true);
    //    characterCamera.SetActive(true);
    //    gameUI.SetActive(true);

    //    Cursor.lockState = CursorLockMode.Locked;

    //}

    public void endGame()
    {
        timer.stopTimer();
        //sceneCamera.SetActive(true);
        //menuUI.SetActive(true);

        //spawnTargets.SetActive(false);
        //characterCamera.SetActive(false);
        //gameUI.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

