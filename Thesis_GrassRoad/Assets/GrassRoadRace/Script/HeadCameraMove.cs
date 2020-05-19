 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCameraMove : MonoBehaviour
{
    /* =============== Public variables =============== */
    public float yaw;
    public float pitch;
    public float mouseSensitivity = 1f;

    //public static float moveSpeed = 1.8f;
    public static float moveSpeed = 0f;

    public static Rigidbody camRb;
    public Vector3 camMovement;
    public Camera charCam; 

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
        //DoCamRotation();
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
        float zRotationOrg = charCam.transform.localEulerAngles.z;
        //Debug.Log("original: " + zRotationOrg);

        // convert angle for calculation
        float tilt_angle = 0;

        if (zRotationOrg < 6 | zRotationOrg > 354)
        {
            tilt_angle = 0;
        } 
        else if (zRotationOrg > 270) 
        {
            tilt_angle = (tilt_angle - 270)/90f;
        } 
        else 
        {
            tilt_angle = tilt_angle / 90f;
        }
        // use tilt_ angle [-1, 1]

        Debug.Log("angle: " + tilt_angle);

        if (trigger > 6) // end case
        {
            camMovement = new Vector3(0, 0, 0);
            gameControl.GetComponent<GameControl>().endGame(); // end the game when reaches ending obstacle
        }
        else if (trigger == 4) // negative case
        {
            camMovement = new Vector3((-1) * (Input.GetAxis("Horizontal")), 0, -moveSpeed);
        }
        else if ((trigger % 2) == 1) // moving -x
        {
            camMovement = new Vector3(-moveSpeed, 0, Input.GetAxis("Horizontal"));
        }
        else
        {
            camMovement = new Vector3(tilt_angle, 0, moveSpeed);
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


    /* rotation based on mouse */
    void DoCamRotation()
    {
        //yaw += mouseSensitivity * Input.GetAxis("Mouse X");
        //pitch -= mouseSensitivity * Input.GetAxis("Mouse Y");
        //yaw += mouseSensitivity * gazeDirection.x;
        //pitch -= mouseSensitivity * gazeDirection.y;
        //Rotate(pitch, yaw, EnterArea.trigger_count);
    }


    /* Constrain rotation angle on y-axis to allow only 180deg each case*/
    void Rotate(float deltaX, float deltaY, int trigger)
    {
        Vector3 angles = transform.localRotation.eulerAngles;
        angles.x = deltaX;
        angles.y = deltaY;

        if (trigger == 4) // negative case
        { // [-270deg to -90deg]
            if (angles.y > -90f && angles.y < 90f || angles.y < -270f && angles.y > -450f)
            {
                if (deltaY > -180) angles.y = -90f;
                else angles.y = -270f;
            }
        }
        else if ((trigger % 2) == 1) // moving -x
        { // [-180deg to 0deg]
            if (angles.y > 0f && angles.y < 180f || angles.y < -180f && angles.y > -360f)
            {
                if (deltaY > -90) angles.y = 0f;
                else angles.y = -180f;
            }
        }
        else // moving z (first and third)
        { // [-90deg to 90deg]
            if (angles.y > 90f && angles.y < 270f || angles.y < -90f && angles.y > -270f)
            {
                if (deltaY > 0) angles.y = 90f;
                else angles.y = -90f;
            }
        }

        if (angles.x > 90f && angles.x < 270f)
        {
            if (deltaX > 0) angles.x = 90f;
            else angles.x = 270f;
        }

        this.transform.localRotation = Quaternion.Euler(angles.x, angles.y, 0.0f);
    }

}
