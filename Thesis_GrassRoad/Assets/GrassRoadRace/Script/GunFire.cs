﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public AudioSource gunShot;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        gunShot.Play();
        muzzleFlash.Play();

        Animation gunRecoil = GetComponent<Animation>();
        gunRecoil.Play("gunRecoil");

        

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage();
            }
        }
    }
}

