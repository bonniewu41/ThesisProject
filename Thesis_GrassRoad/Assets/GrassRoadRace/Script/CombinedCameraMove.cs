using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CombinedCameraMove : MonoBehaviour
{

    /* =============== Declare variables =============== */
    public static float moveSpeed = 1.93f;

    public static Rigidbody camRb;
    public Vector3 camMovement;

    public GameControl gameControl;

    int temp_trigger = 0;

    bool is_XBOX = false;
    bool is_Head = false;
    bool is_Natural = false;
    public static bool version2 = false;

    public static Scene scene;


    /* Specifically for XBOX */
    public float yaw;
    public float pitch;
    public float mouseSensitivity = 1f;

    PlayerControls controls;
    float leftRightMvmt;
    Vector2 gazeDirection;


    /* Specifically for HEAD and NATURAL */
    public Camera charCam;
    private bool isColliding = false;


    /* Specifically for NATURAL */
    public float prevPosition = 0f;
    /* =============================================== */


    void Start()
    {
        scene = SceneManager.GetActiveScene();
        identify_scene(scene);
        camRb = this.GetComponent<Rigidbody>();
        camRb.isKinematic = false;
    }


    void Update()
    {
        if (is_XBOX)                        // initilize this is_XBOX variable
        {
            DoCamRotation();
        }
        GetTurn(EnterArea.trigger_count);
    }


    void FixedUpdate()
    {
        SideMvmt(camMovement);
    }



    /* ~~ XBOX ONLY ~~ */
    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Movement.performed += context => leftRightMvmt = context.ReadValue<float>();

        controls.Gameplay.Rotation.performed += context => gazeDirection = context.ReadValue<Vector2>();
        controls.Gameplay.Rotation.canceled += context => gazeDirection = Vector2.zero;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
    /* ~~~~~~~~~~~~~~~ */

    void identify_scene(Scene scene)
    {

        if (scene.buildIndex > 0 && scene.buildIndex < 5)
            is_XBOX = true;

        else if (scene.buildIndex > 4 && scene.buildIndex < 9)
            is_Head = true;

        else if (scene.buildIndex > 8 && scene.buildIndex < 13)
            is_Natural = true;


        if ((scene.buildIndex % 2) == 0)
            version2 = true;

    }

    /* Use number of triggers hit to determine direction to move forward to.
       NOTE: Player will need to turn their gaze direction by themselves. */
    void GetTurn(int trigger)
    {
        // Initialize Variable for HEAD
        float tilt_angle = charCam.transform.localEulerAngles.z;
        float zRotationOrg = charCam.transform.localEulerAngles.z;

        // Initialize Variable for NATURAL
        float currPositionX = charCam.transform.localPosition.x;
        float currPositionZ = charCam.transform.localPosition.z;
        float stepPosition = 0;

        // Player at last path and should END GAME
        if (trigger > 6)
        {
            camMovement = new Vector3(0, 0, 0);

            // end the game when reaches ending obstacle
            gameControl.GetComponent<GameControl>().endGame();
        }

        // Player at odd numbered paths
        else if ((trigger % 2) == 1)
        {
            if (is_Head)
                camMovement = new Vector3(-moveSpeed, 0, _get_tilt_angle(zRotationOrg));

            else if (is_Natural)
                camMovement = new Vector3(-moveSpeed, 0, _get_step(currPositionZ));

            else
                camMovement = new Vector3(-moveSpeed, 0, leftRightMvmt);
        }


        // Check last two conditions if is version 2
        else if (version2) {
            if (trigger == 2) {
                if (is_Head)
                    camMovement = new Vector3((-1) * _get_tilt_angle(zRotationOrg), 0, -moveSpeed);

                else if (is_Natural)
                    camMovement = new Vector3(_get_step(currPositionX), 0, -moveSpeed);

                else
                    camMovement = new Vector3((-1) * leftRightMvmt, 0, -moveSpeed);
            } else {
                if (is_Head)
                    camMovement = new Vector3(_get_tilt_angle(zRotationOrg), 0, moveSpeed);

                else if (is_Natural)
                    camMovement = new Vector3(_get_step(currPositionX), 0, moveSpeed);

                else
                    camMovement = new Vector3(leftRightMvmt, 0, moveSpeed);
            }
        }

        // Check last two conditions if is version 1
        else {
            if (trigger == 4) {
                if (is_Head)
                    camMovement = new Vector3((-1) * _get_tilt_angle(zRotationOrg), 0, -moveSpeed);

                else if (is_Natural)
                    camMovement = new Vector3(_get_step(currPositionX), 0, -moveSpeed);

                else
                    camMovement = new Vector3((-1) * leftRightMvmt, 0, -moveSpeed);
            } else {
                if (is_Head)
                    camMovement = new Vector3(_get_tilt_angle(zRotationOrg), 0, moveSpeed);

                else if (is_Natural)
                    camMovement = new Vector3(_get_step(currPositionX), 0, moveSpeed);

                else
                    camMovement = new Vector3(leftRightMvmt, 0, moveSpeed);
            }
        }
    }


    /* HELPER for HEAD GET_TURN */
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


    /* HELPER for NATURAL GET_TURN */
    float _get_step(float position)
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
            isColliding = true;

        else
            isColliding = false;
    }


    void SideMvmt(Vector3 direction)
    {
        camRb.MovePosition((Vector3)transform.position + (direction * moveSpeed * Time.deltaTime));
    }


    /* move character back to where it was 5 seconds ago. */
    public static void Penalty(Vector3 hitPos, int trigger)
    {
        Vector3 camRewind = camRb.position;
        if ((trigger % 2) == 1) // moving -x
        {
            camRewind.x = hitPos.x + CombinedCameraMove.moveSpeed * 5;
        }
        else if (version2)
        {
            if(trigger == 2)
                camRewind.z = hitPos.z + CombinedCameraMove.moveSpeed * 5;
            else
                camRewind.z = hitPos.z - CombinedCameraMove.moveSpeed * 5;
        }
        else
        {
            if (trigger == 4)
                camRewind.z = hitPos.z + CombinedCameraMove.moveSpeed * 5;
            else
                camRewind.z = hitPos.z - CombinedCameraMove.moveSpeed * 5;
        }

        camRb.position = camRewind;
    }


    /* rotation using xBox joystick */
    void DoCamRotation()
    {
        yaw += mouseSensitivity * gazeDirection.x;
        pitch -= mouseSensitivity * gazeDirection.y;
        this.transform.localRotation = Quaternion.Euler(pitch, yaw, 0.0f);
        // Rotate(pitch, yaw, EnterArea.trigger_count);
    }


    /* Constrain rotation angle on y-axis to allow only 180deg each case*/
    //void Rotate(float deltaX, float deltaY, int trigger)
    //{
    //    Vector3 angles = transform.localRotation.eulerAngles;
    //    angles.x = deltaX;
    //    angles.y = deltaY;

    //    if (trigger == 4) // negative case
    //    { // [-270deg to -90deg]
    //        if (angles.y > -90f && angles.y < 90f || angles.y < -270f && angles.y > -450f)
    //        {
    //            if (deltaY > -180) angles.y = -90f;
    //            else angles.y = -270f;
    //        }
    //    }
    //    else if ((trigger % 2) == 1) // moving -x
    //    { // [-180deg to 0deg]
    //        if (angles.y > 0f && angles.y < 180f || angles.y < -180f && angles.y > -360f)
    //        {
    //            if (deltaY > -90) angles.y = 0f;
    //            else angles.y = -180f;
    //        }
    //    }
    //    else // moving z (first and third)
    //    { // [-90deg to 90deg]
    //        if (angles.y > 90f && angles.y < 270f || angles.y < -90f && angles.y > -270f)
    //        {
    //            if (deltaY > 0) angles.y = 90f;
    //            else angles.y = -90f;
    //        }
    //    }

    //    if (angles.x > 90f && angles.x < 270f)
    //    {
    //        if (deltaX > 0) angles.x = 90f;
    //        else angles.x = 270f;
    //    }

    //    this.transform.localRotation = Quaternion.Euler(angles.x, angles.y, 0.0f);
    //}
}
