using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturalGunFire : MonoBehaviour
{
    public ParticleSystem muzzleFlash;
    public AudioSource gunShot;

    public Transform gunTransform;

    void Update()
    {
        gunTransform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        gunTransform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);

        if (OVRInput.Get(OVRInput.Button.Two))
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
        if (Physics.Raycast(gunTransform.position, gunTransform.forward, out hit))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage();
            }

        }
    }
}
