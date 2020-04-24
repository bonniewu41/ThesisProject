using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    //public Camera CameraFacing;

    public Transform gunBarrelTransform;
    private Vector3 originalScale;

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        float distance;
        if (Physics.Raycast(new Ray(gunBarrelTransform.transform.position, gunBarrelTransform.transform.rotation * Vector3.forward), out hit))
        {
            distance = hit.distance;
        }
        else
        {
           // distance = gunBarrelTransform.farClipPlane * 0.95f;
           distance = gunBarrelTransform.position.z * 10000;
        }

        transform.position = gunBarrelTransform.transform.position + gunBarrelTransform.transform.rotation * Vector3.forward * distance;
        transform.LookAt(gunBarrelTransform.transform.position);
        transform.Rotate(0.0f, 180.0f, 0.0f);
        
        if (distance < 10.0f)
        {
            distance *= 1 + 5 * Mathf.Exp(-distance);
        }
        transform.localScale = originalScale * distance;

    }
}