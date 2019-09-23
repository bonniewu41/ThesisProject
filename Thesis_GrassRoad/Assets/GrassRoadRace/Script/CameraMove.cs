using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CameraMove : MonoBehaviour
{

    //Public variables
    public float yaw;
    public float pitch;
    public float moveSpeed = 3.0f;
    public float mouseSensitivity = 1f;

    //Private variables
    private const float PATH_WIDTH = 1.15f;
    private CharacterController controller;
    private int desiredLane = 1; // 0 = left, 1 = middle, 2 = right



    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {

        //ForwardMvmt();
        Movement();
        CamRotation();
        UnlockCursor();
    }



    //void ForwardMvmt()
    //{
    //    this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, moveSpeed);
    //}


    void Movement()
    {
        // Gather input on which lane we should be
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLane(false);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveLane(true);
        }


        // Calculate where we should be in the future
        Vector3 targetPos = transform.position.z * Vector3.forward;
        if (desiredLane == 0)
        {
            targetPos += Vector3.left * PATH_WIDTH;
        }
        else if (desiredLane == 2)
        {
            targetPos += Vector3.right * PATH_WIDTH;
        }


        // Calculate move vector
        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPos - transform.position).normalized.x * moveSpeed;
        moveVector.y = -0.1f;
        moveVector.z = moveSpeed;


        // Move Camera
        controller.Move(moveVector * Time.deltaTime);

    }


    void MoveLane(bool goingRight)
    {
        desiredLane += (goingRight) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);
    }


    void CamRotation()
    {
        yaw += mouseSensitivity * Input.GetAxis("Mouse X");
        pitch -= mouseSensitivity * Input.GetAxis("Mouse Y");
        this.transform.localRotation = Quaternion.Euler(pitch, yaw, 0.0f);
    }


    void UnlockCursor()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

}











/*----------------------------------------------------------------------------*/


//void MouseRotate()
//{
//    var md = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

//    md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
//    smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
//    smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
//    mouseLook += smoothV;

//    transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
//    character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
//}



//public float moveSpeed;


//void FixedUpdate()
//{
//   MoveObj();

//   if (Input.GetKeyDown(KeyCode.A)) {
//      ChangeView01();
//   }

//   if (Input.GetKeyDown(KeyCode.S)) {
//      ChangeView02();
//   }
//}


//void MoveObj()
//{
//    float moveAmount = Time.smoothDeltaTime * moveSpeed;
//    transform.Translate(0f, 0f, moveAmount);
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



///*-----------------------
//Do rotation from mouse
//-----------------------*/
//void doRotation()
//{
//    rotate(
//        -mouseSensitivity * Input.GetAxis("Mouse Y"),
//         mouseSensitivity * Input.GetAxis("Mouse X")
//    );
//}

///*-----------------------
//Rotate the game object
//-----------------------*/
//void rotate(float deltaX, float deltaY)
//{
//    //1. Rotate around x-axis & y-axis
//    Vector3 angles = transform.localRotation.eulerAngles;
//    angles.x += deltaX;
//    angles.y += deltaY + 180;

//    //2. Constrain the angle around x-axis in [0 ~ 90] or [270 ~ 360] degrees
//    //if (angles.x > 90f && angles.x < 270f)
//    //{
//    //    if (deltaX > 0) angles.x = 90f;
//    //    else angles.x = 270f;
//    //}

//    //3. Assign rotation
//    this.transform.localRotation = Quaternion.Euler(angles);
//}





//Vector3 pos = transform.position;

//if (Input.GetKeyDown(KeyCode.LeftArrow) && pos.x > -1)
//{
//    pos.x -= 1;
//}

//if (Input.GetKeyDown(KeyCode.RightArrow) && pos.x < 1)
//{
//    pos.x += 1;
//}

//transform.position = pos;





















