using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    public GameObject scoreText;
    public GameObject targetText;
    public GameObject collisionText;
    public static int scoreValue;
    public static int targetNum;
    public static int hurdleNum;

    void Update()
    {
        //scoreText.GetComponent<Text>().text = "Score: " + scoreValue;

        targetText.GetComponent<Text>().text = "Targets: " + targetNum;
        collisionText.GetComponent<Text>().text = "Collisions: " + hurdleNum;
    }

}
