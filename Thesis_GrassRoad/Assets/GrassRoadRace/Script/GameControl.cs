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
    private AudioSource gameMusic;
    private Timer timer;
    private float backToMenuDelay = 10f;
    
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

        timer.startTimer();
        gameMusic.Play();

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void endGame()
    {
        timer.stopTimer();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Invoke("BackToMenu", backToMenuDelay);
    }

    void BackToMenu ()
    {
        SceneManager.LoadScene(0);
    }
}

