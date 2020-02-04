using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    /* =============== Public variables =============== */
    [SerializeField]
    private int minutes;
    [SerializeField]
    private int seconds;

    private int m, s;

    [SerializeField]
    private Text timerText;

    //store a reference to the GameControl component that is present in the GameObject Control hierarchy.
    private GameControl gameControl; 
    /* ================================================ */


    void Start()
    {
        gameControl = gameObject.GetComponent<GameControl>(); //We find the GameControl component and assign it to gameControl.
    }


    public void startTimer()
    {
        m = minutes;
        s = seconds;
        writeTimer(m, s);
        Invoke("updateTimer", 1f); //The "updateTimer" method is invoked after 1 second has passed from this point.
    }


    public void stopTimer()
    {
        CancelInvoke(); //We stop all invocations that may have been pending.
    }


    //updates the time values every second.
    private void updateTimer()
    {
        s--; //We decrement the seconds

        if (s < 0)
        { 
            if (m == 0) //Time is up
            {
                gameControl.endGame();
                return; //We interrupt the updateTimer() method.
            }
            else //There is still time
            {
                m--;
                s = 59;
            }
        }

        writeTimer(m, s);
        Invoke("updateTimer", 1f); //We invoke the updateTimer() method after 1 second.
    }


    //formats the time values and writes them to the Canvas element
    private void writeTimer(int m, int s)
    {
        if (s < 10) //second variable has a single digit therefore need to concatenate 0 to second digit
        { 
            timerText.text = "Time Left: 0" + m.ToString() + ":0" + s.ToString();
        }
        else // no concatenation of 0 required
        {
            timerText.text = "Time Left: 0" + m.ToString() + ":" + s.ToString();
        }
    }

}

