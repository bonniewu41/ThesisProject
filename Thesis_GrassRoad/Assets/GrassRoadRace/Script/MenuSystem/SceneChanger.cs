using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void PlayGame(string string_name)
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //Debug.Log("btn pressed");
        SceneManager.LoadScene(string_name);
    }

    //public void PlayXbox()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //}
}
