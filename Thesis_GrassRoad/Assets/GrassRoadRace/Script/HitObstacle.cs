using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObstacle : MonoBehaviour
{

    public AudioSource hitHurdleSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Character")
        {
            Debug.Log("Collision with " + collision.gameObject.name);
            hitHurdleSound.Play();
            ScoringSystem.scoreValue -= 10;
        }
    }

    void onHit ()
    {
        //ScoringSystem.scoreValue -= 10;
        //hitHurdleSound.Play();
    }
}
