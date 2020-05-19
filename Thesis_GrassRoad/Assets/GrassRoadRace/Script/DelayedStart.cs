using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedStart : MonoBehaviour
{
    public GameObject countDown;
    public AudioSource countDownSound;

    void Start()
    {
        StartCoroutine("StartDelay");
        countDownSound.Play();
    }

    IEnumerator StartDelay()
    {
        Time.timeScale = 0;
        float pauseTime = Time.realtimeSinceStartup + 3.5f;
        while (Time.realtimeSinceStartup < pauseTime)
        {
            yield return 0;
        }
        countDown.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
