using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObstacle : MonoBehaviour
{

    public AudioSource hitHurdleSound;
    private Vector3 characterHitPos;


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Character")
        {
            hitHurdleSound.Play();
            //ScoringSystem.scoreValue -= 10;
            ScoringSystem.hurdleNum += 1;
            characterHitPos = collision.gameObject.transform.position;
            CombinedCameraMove.Penalty(characterHitPos, EnterArea.trigger_count); //calls the penalty function in CameraMove to modify the character position
        }
    }
}
