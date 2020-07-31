using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialHeadCameraMove : MonoBehaviour
{
    /* =============== Public variables =============== */
    public static float moveSpeed = 1.95f;

    public static Rigidbody camRb;
    public Vector3 camMovement;
    public Camera charCam;

    public GameControl gameControl;

    private bool isColliding = false;
    /* ================================================ */

    void Start()
    {
        camRb = this.GetComponent<Rigidbody>();
        camRb.isKinematic = false;
    }


    void Update()
    {
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
        float tilt_angle = charCam.transform.localEulerAngles.z;
        float zRotationOrg = charCam.transform.localEulerAngles.z;

        // sets different moving direction for different path
        if (trigger > 1) // end case
        {
            tilt_angle = _get_tilt_angle(zRotationOrg);
            camMovement = new Vector3(0, 0, 0);
            gameControl.GetComponent<GameControl>().endGame(); // end the game when reaches ending obstacle
        }
        else if (trigger == 1) // moving x (second path)
        {
            tilt_angle = _get_tilt_angle(zRotationOrg);
            camMovement = new Vector3(moveSpeed, 0, (-1) * tilt_angle);
        }
        else // moving z (first path)
        {
            tilt_angle = _get_tilt_angle(zRotationOrg);
            camMovement = new Vector3(tilt_angle, 0, moveSpeed);
        }
    }


    float _get_tilt_angle(float RotatedAngle)
    {
        float angle = 0;

        if (RotatedAngle < 6 | RotatedAngle > 354)
        {
            angle = 0;
        }
        else if (RotatedAngle > 270)
        {
            angle = 0.5f;
        }
        else
        {
            angle = -0.5f;
        }

        if (isColliding)
        {
            angle *= -0.5f;
            isColliding = false;
        }

        return angle;
    }


    /* check if character hits the fence */
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Fence")
        {
            isColliding = true;
        }
        else
        {
            isColliding = false;
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
        if (trigger == 1) // moving x (second path)
        {
            camRewind.x = hitPos.x - CameraMove.moveSpeed * 5;
        }
        else // moving z (first path)
        {
            camRewind.z = hitPos.z - CameraMove.moveSpeed * 5;
        }

        camRb.position = camRewind;
    }

}

