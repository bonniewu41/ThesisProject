using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObstacle : MonoBehaviour
{

    public AudioSource hitHurdleSound;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Character")
        {
            hitHurdleSound.Play();
            ScoringSystem.scoreValue -= 10;
        }
    }

}
