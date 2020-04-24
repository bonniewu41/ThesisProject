using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTouchController : MonoBehaviour
{
    public ParticleSystem muzzleFlash;
    public AudioSource gunShot;

    public Transform gunBarrelTransform;


    //private float rightTrigger = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
    //private float rightTrigger = OVRInput.Get(OVRInput.Button.One);

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.Get(OVRInput.Button.One))
        {
            Debug.Log("trigger button pressed");
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
        if (Physics.Raycast(gunBarrelTransform.position, gunBarrelTransform.forward, out hit))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage();
            }
        }
    }

    //private void RaycastGun()
    //{
    //    RaycastHit hit;

    //    if(Physics.Raycast(gunBarrelTransform.position, gunBarrelTransform.forward, out hit))
    //    {
    //        if (hit.collider.gameObject.compareTag("Target"))
    //        {
    //            Destroy(hit.collider.gameObject);
    //        }
    //    }
    //}
}
