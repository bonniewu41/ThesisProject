using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeRemaining : MonoBehaviour
{

    public Text timerText;
    private float time = 420; //7min

    void Start()
    {
        StartCoundownTimer();
    }

    void StartCoundownTimer()
    {
        if (timerText != null)
        {
            time = 420;
            timerText.text = "Time Left: 07:00";
            InvokeRepeating("UpdateTimer", 0.0f, 0.01667f);
        }
    }

    void UpdateTimer()
    {
        if (timerText != null)
        {
            time -= Time.deltaTime;
            string minutes = Mathf.Floor(time / 60).ToString("00");
            string seconds = ((time % 60)).ToString("00");
            timerText.text = "Time Left: " + minutes + ":" + seconds;
        }
    }



    //[SerializeField] private Text uiText;
    ////[SerializeField] private float mainTimer;

    //private float timer;
    //private bool canCount = true;
    //private bool doOnce = false;



    //// Update is called once per frame
    //void Update()
    //{
    //    if (timer >= 0.0f && canCount)
    //    {
    //        timer -= Time.deltaTime;

    //        int seconds = (int)(timer % 60);
    //        int minutes = (int)(timer / 60) % 60;

    //        string timerString = string.Format("{0:00}:{1:00}", minutes,seconds);

    //        uiText.text = timerString;
    //            //"Time Left: " + timer.ToString("F");

    //    } else if (timer <= 0.0f && !doOnce)
    //    {
    //        canCount = false;
    //        doOnce = true;
    //        //uiText.text = "0.00";
    //        //timer = 0.0f;
    //    }

    //}
}
