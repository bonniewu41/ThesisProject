using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    // Use this for initialization
    [SerializeField]
    private int minutes; //Amount of initial minutes. This variable doesn't change its value during the execution of the game.
    [SerializeField]
    private int seconds; //Amount of initial seconds. This variable doesn't change its value during the execution of the game.

    private int m, s; //Current time values. These variables are modified as time goes by.

    [SerializeField]
    private Text timerText; //Here we will save a reference of the text element of the Canvas.

    private GameControl gameControl; //Here we store a reference to the GameControl component that is present in the GameObject Control hierarchy.


    void Start()
    {
        gameControl = gameObject.GetComponent<GameControl>();//We find the GameControl component and assign it to gameControl.
    }

    //The following method starts the timer.
    public void startTimer()
    {
        m = minutes; //We initialize the minutes.
        s = seconds; //We initialize the seconds.
        writeTimer(m, s); //We write the time values in the Text element of the Canvas.
        Invoke("updateTimer", 1f); //The "updateTimer" method is invoked after 1 second has passed from this point.

    }

    //The following method stops the timer.
    public void stopTimer()
    {
        CancelInvoke(); //We stop all invocations that may have been pending.

    }

    //The following method updates the time values every second.
    private void updateTimer()
    {
        s--; //We decrement the seconds.

        if (s < 0)
        { //If this statement is true, it means that the variable s was 0 and became  -1.

            //We have two possibilities, the first is that the minutes variable is at 0.
            //That is to say that in the previous second the time value is 0:00, consequently the time was expired.
            //The second possibility is that m is different from 0, in that case we decrease one minute and set the seconds variable becomes 59.
            if (m == 0)
            {
                //Time is up.
                gameControl.endGame(); //We make a call to the endGame() method present in the GameControl Script.
                return; //We interrupt the updateTimer() method.
            }
            else
            {
                //There is still time.
                m--; //Decrement the minutes.
                s = 59; //Assign the value 59 to the variable s.

                //An example of what can happen in this case is that the timer makes the following state change 1:00 -> 0:59 .
            }

        }

        writeTimer(m, s); //We write the new time values in the UI.
        Invoke("updateTimer", 1f); //We invoke the updateTimer() method after 1 second.

        //With this last instruction we get the updateTimer() method to invoke itself in intervals of one second.

    }

    //The next method formats the time values and writes them to the Canvas element.
    //Unlike the methods we have written before, this one will receive two parameters: the integers m and s,
    //which will be used within the method of doing operations.
    //Defining the method in this way is very interesting because it raises the following question:
    //When we use m and s within the writeTimer() method. Are we using the m and s defined in the class itself or are we using the parameters we received at the time of the call?
    //When we study programming we're going to come back to this.

    private void writeTimer(int m, int s)
    {

        if (s < 10)
        { //If this is true it means that the second variable has a single digit.
          //In this case we must concatenate a 0 to the left of the seconds to keep the format,
          //otherwise we could visualize the time for example in this way: 1:6 (indicating 1 minute 6 seconds).

            timerText.text = "Time Left: 0" + m.ToString() + ":0" + s.ToString();

        }
        else
        {
            //In this case the seconds variable has two digits, therefore we don't concatenate the 0 to the left.
            timerText.text = "Time Left: 0" + m.ToString() + ":" + s.ToString();

        }


    }



}

