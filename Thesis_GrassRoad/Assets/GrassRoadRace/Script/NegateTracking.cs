using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegateTracking : MonoBehaviour
{
    private Camera cam;
    private Vector3 startPos;

    void Start () {
        cam = GetComponentInChildren<Camera>();
        startPos = transform.localPosition;
    }
	
    void Update () {
        //transform.localPosition = startPos - cam.transform.localPosition;
        transform.localRotation = Quaternion.Inverse(cam.transform.localRotation);
    }
}
