using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Target : MonoBehaviour
{

    public AudioSource collectSound;
    public float rotateSpeed;

    private void Start()
    {
        rotateSpeed = 0.5f;
    }

    void Update()
    {
        //transform.Rotate(0, rotateSpeed, 0, Space.World);
    }


    public void TakeDamage()
    {
        ScoringSystem.scoreValue += 10;
        ScoringSystem.targetNum += 1;
        collectSound.Play();
        transform.position = Vector3.one * 9999f; // move the game object off screen while it finishes it's sound, then destroy it
        Destroy(gameObject, 2f);
    }

}
