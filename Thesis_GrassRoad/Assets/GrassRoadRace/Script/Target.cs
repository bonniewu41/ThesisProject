using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Target : MonoBehaviour
{

    public AudioSource collectSound;
    public ParticleSystem hitEffects;
    public int rotateSpeed;

    private void Start()
    {
        rotateSpeed = 2;
    }

    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }


    public void TakeDamage()
    {
        ScoringSystem.scoreValue += 10;
        collectSound.Play();
        //hitEffects.Play();

        //health -= amount;
        //if (health <= 0f) {
            Die();
        //}
    }

    void Die()
    {
        Destroy(gameObject);
    }



}
