using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class NaturalCameraMove : MonoBehaviour
{

    /* =============== Public variables =============== */
    public static float moveSpeed = 1.95f;

    public static Rigidbody camRb;
    public Vector3 camMovement;
    public Camera charCam;

    public GameControl gameControl;
    private bool isColliding = false;
    public float prevPosition = 0f;
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
        float currPositionX = charCam.transform.localPosition.x;
        float currPositionZ = charCam.transform.localPosition.z;
        float stepPosition = 0;


        if (trigger > 6) // end case
        {
            stepPosition = _get_step(currPositionX);
            camMovement = new Vector3(0, 0, 0);
            gameControl.GetComponent<GameControl>().endGame(); // end the game when reaches ending obstacle
        }
        else if (trigger == 4) // negative case
        {
            stepPosition = _get_step(currPositionX);
            camMovement = new Vector3(stepPosition, 0, -moveSpeed);
        }
        else if ((trigger % 2) == 1) // moving -x
        {
            stepPosition = _get_step(currPositionZ);
            camMovement = new Vector3(-moveSpeed, 0, stepPosition);
        }
        else
        {
            stepPosition = _get_step(currPositionX);
            camMovement = new Vector3(stepPosition, 0, moveSpeed);
        }
    }


    float _get_step (float position)
    {
        float step = 0f;

        if (0.2 < position)
        {
            step = 0.8f; // move right
        }
        else if (position < -0.2)
        {
            step = -0.8f; // move left
        }

        if (isColliding)
        {
            step *= -0.2f;
            isColliding = false;
        }

        return step;
    }

    /* check if character hits the fence */
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Fence")
        {
            // Debug.Log("collides");
            isColliding = true;
        }
        else
        {
            // Debug.Log("not collides");
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

}

