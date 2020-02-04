using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CameraMove : MonoBehaviour
{

    /* =============== Public variables =============== */
    public float yaw;
    public float pitch;
    public float mouseSensitivity = 1f;

    //public static float moveSpeed = 1.7f;
    public static float moveSpeed = 2.5f;

    public static Rigidbody camRb;
    public Vector3 camMovement;

    public AudioSource hitHurdleSound;
    public GameControl gameControl;
    /* ================================================ */



    void Start()
    {
        camRb = this.GetComponent<Rigidbody>();
        camRb.isKinematic = false;
    }


    void Update()
    {
        CamRotation();
        GetTurn(EnterArea.trigger_count);
    }


    void FixedUpdate()
    {
        SideMvmt(camMovement);
    }


    /* Use number of triggers hit to determine direction to move forward to.
       NOTE: Player will need to turn their gaze direction by themselves. */
    void GetTurn(int trigger)
    {
        if (trigger > 6)
        {
            camMovement = new Vector3(0, 0, 0);
            gameControl.GetComponent<GameControl>().endGame(); // end the game when reaches ending obstacle
        }
        else if (trigger == 4)
        {
            camMovement = new Vector3((-1) * (Input.GetAxis("Horizontal")), 0, -moveSpeed);
        }
        else if ((trigger % 2) == 1)
        {
            camMovement = new Vector3(-moveSpeed, 0, Input.GetAxis("Horizontal"));
        }
        else
        {
            camMovement = new Vector3(Input.GetAxis("Horizontal"), 0, moveSpeed);
        }
    }


    void SideMvmt(Vector3 direction)
    {
        camRb.MovePosition((Vector3)transform.position + (direction * moveSpeed * Time.deltaTime));
    }


    /* move character back to where it was 5 seconds ago. */
    public static void Penalty(Vector3 hitPos, int trigger)
    {
        Vector3 camRewind = camRb.position;
        if (trigger == 4) // negative case
        {
            camRewind.z = hitPos.z + CameraMove.moveSpeed * 5;
        }
        else if ((trigger % 2) == 1) // moving -x
        {
            camRewind.x = hitPos.x + CameraMove.moveSpeed * 5;
        }
        else // moving z (first and third)
        {
            camRewind.z = hitPos.z - CameraMove.moveSpeed * 5;
        }

        camRb.position = camRewind;
    }


    void CamRotation()
    {
        yaw += mouseSensitivity * Input.GetAxis("Mouse X");
        pitch -= mouseSensitivity * Input.GetAxis("Mouse Y");
        this.transform.localRotation = Quaternion.Euler(pitch, yaw, 0.0f);
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






















