using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialHitObstacle : MonoBehaviour
{

    public AudioSource hitHurdleSound;
    private Vector3 characterHitPos;


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Character")
        {
            hitHurdleSound.Play();
            ScoringSystem.scoreValue -= 10;
            characterHitPos = collision.gameObject.transform.position;
            TrialCameraMove.Penalty(characterHitPos, EnterArea.trigger_count); //calls the penalty function in CameraMove to modify the character position
        }
    }
}
