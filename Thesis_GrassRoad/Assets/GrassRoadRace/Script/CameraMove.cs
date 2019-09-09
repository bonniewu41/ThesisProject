﻿
using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    //public float moveSpeed;
    public GameObject mainCamera;

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    public float yaw = 0.0f;
    public float pitch = 0.0f;


    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        mainCamera.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -3);
        //mainCamera.transform.localPosition = new Vector3 ( 0, 0, 0 );
		//mainCamera.transform.localRotation = Quaternion.Euler (18, 180, 0);
	
	}

    // Update is called once per frame
    void Update()
    {
        FollowCam();
        MoveLR();
        MouseAppear();

    }


    void MouseAppear()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }


    void FixedUpdate()
	{
		//MoveObj ();
		
		//if (Input.GetKeyDown (KeyCode.A)) {
		//	ChangeView01();
		//}
		
		//if (Input.GetKeyDown (KeyCode.S)) {
		//	ChangeView02();
		//}
	}


    //void MoveObj()
    //{
    //    float moveAmount = Time.smoothDeltaTime * moveSpeed;
    //    transform.Translate(0f, 0f, moveAmount);
    //}

    void FollowCam()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");
        mainCamera.transform.localRotation = Quaternion.Euler(pitch, yaw + 180, 0.0f);
    }

    void MoveLR()
    {
        Vector3 pos = transform.position;

        if (Input.GetKeyDown(KeyCode.RightArrow) && pos.x > -1) {
            pos.x -= 1;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && pos.x < 1) {
            pos.x += 1;
        }

        transform.position = pos;
    }



    /*-----------------------
    Do rotation from mouse
    -----------------------*/
    //void doRotation()
    //{
    //    movement.rotate(
    //        -mouseSensitivity * Input.GetAxis("Mouse Y"),
    //            mouseSensitivity * Input.GetAxis("Mouse X")
    //    );
    //}

    /*-----------------------
    Rotate the game object
    -----------------------*/
    //public void rotate(float deltaX, float deltaY)
    //{
    //    //1. Rotate around x-axis & y-axis
    //    Vector3 angles = transform.localRotation.eulerAngles;
    //    angles.x += deltaX;
    //    angles.y += deltaY;

    //    //2. Constrain the angle around x-axis in [0 ~ 90] or [270 ~ 360] degrees
    //    if (angles.x > 90f && angles.x < 270f)
    //    {
    //        if (deltaX > 0) angles.x = 90f;
    //        else angles.x = 270f;
    //    }

    //    //3. Assign rotation
    //    transform.localRotation = Quaternion.Euler(angles);
    //}











    //void ChangeView01() {
    //	transform.position = new Vector3 (0, 2, 10);
    //	// x:0, y:1, z:52
    //	mainCamera.transform.localPosition = new Vector3 ( -8, 2, 0 );
    //	mainCamera.transform.localRotation = Quaternion.Euler (14, 90, 0);
    //}

    //void ChangeView02() {
    //	transform.position = new Vector3 (0, 2, 10);
    //	// x:0, y:1, z:52
    //	mainCamera.transform.localPosition = new Vector3 ( 0, 0, 0 );
    //	mainCamera.transform.localRotation = Quaternion.Euler ( 19, 180, 0 );
    //	moveSpeed = -20f;

    //}
}























