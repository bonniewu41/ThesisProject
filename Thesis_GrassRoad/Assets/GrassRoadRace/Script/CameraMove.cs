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

    public float xVal = 0;
    public float yVal = 0;
    public float zVal = 3;

    //Private variables
    private const float PATH_WIDTH = 1.15f;
    private CharacterController controller;
    private int desiredLane = 1; // 0 = left, 1 = middle, 2 = right
    private Vector3 moveVector = Vector3.zero;
    private int left_curr = 1;
    private int right_curr = 1;



    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //controller = GetComponent<CharacterController>();
    }


    void Update()
    {

        ForwardMvmt();
        //Movement();
        //CamRotation();
        UnlockCursor();
    }



    void ForwardMvmt()
    {
        

        //GetComponent<Rigidbody>().velocity = new Vector3(0, 0, moveSpeed);


        //if (Input.GetKeyDown(KeyCode.LeftArrow) && GetComponent<Transform>().position.z > 178 && GetComponent<Transform>().position.z < 179)
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (left_curr == 1 && GetComponent<Transform>().position.z > 178 && GetComponent<Transform>().position.z < 179)
            {
                xVal = -3;
                zVal = 0;
                GetComponent<Rigidbody>().angularVelocity = new Vector3(0, -1, 0);
                StartCoroutine(StopRotation());
                left_curr = 2;
            } else if (left_curr == 2 && GetComponent<Transform>().position.z > 178 && GetComponent<Transform>().position.z < 179)
            {
                xVal = -3;
                zVal = 0;
                GetComponent<Rigidbody>().angularVelocity = new Vector3(0, -1, 0);
                StartCoroutine(StopRotation());
                left_curr = 3;
            } else if (left_curr == 3 && GetComponent<Transform>().position.z > 178 && GetComponent<Transform>().position.z < 179)
            {
                xVal = 0;
                zVal = -3;
                GetComponent<Rigidbody>().angularVelocity = new Vector3(0, -1, 0);
                StartCoroutine(StopRotation());
                left_curr = 0;
            } 
            
        }
                
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            switch (right_curr)
            {
                case 1 when GetComponent<Transform>().position.x > -185 && GetComponent<Transform>().position.z < -184:
                    xVal = 0;
                    zVal = 3;
                    GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 1, 0);
                    StartCoroutine(StopRotation());
                    right_curr = 2;
                    break;
                case 2:
                    xVal = -3;
                    zVal = 0;
                    GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 1, 0);
                    StartCoroutine(StopRotation());
                    right_curr = 3;
                    break;
                case 3:
                    xVal = 0;
                    zVal = 3;
                    GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 1, 0);
                    StartCoroutine(StopRotation());
                    right_curr = 0;
                    break;
            }
        }
        GetComponent<Rigidbody>().velocity = new Vector3(xVal, yVal, zVal);

    }

    IEnumerator StopRotation()
    {
        yield return new WaitForSeconds(.8f);
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);

        if (left_curr == 2 && right_curr == 1)
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0, -90, 0);
        }
        else if (left_curr == 2 && right_curr == 2)
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);
        }
        else if (left_curr == 3 && right_curr == 2)
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0, -90, 0);
        }
        else if (left_curr == 0 && right_curr == 2)
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0, -180, 0);
        }
        else if (left_curr == 0 && right_curr == 3)
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0, -90, 0);
        }
        else if (left_curr == 0 && right_curr == 0)
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);
        }

    }
    


    void Movement()
    {
        //this.transform.Rotate(0, Input.GetAxisRaw("Horizontal"), 0);

        //Vector3 currentPosition = transform.TransformPoint(Vector3.zero);

        // Gather input on which lane we should be
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            //GetComponent<Rigidbody>().velocity = new Vector3(moveVector.x, moveVector.y, moveVector.z);

            this.transform.Rotate(Vector3.up, -90);
            moveVector.x = -moveSpeed;
            moveVector.y = -0.001f;
            //moveVector.z = currentPosition.z;
            moveVector.z = 0;

            //controller.Move(moveVector * Time.deltaTime);

            //moveVector.z = this.transform.position.z;
            //moveVector.x = -moveSpeed;
            //controller.Move(moveVector * Time.deltaTime);

            //if (canTurn)
            //{
            //TurnLeft();

            //}
            //else
            //{
            //MoveLane(false);
            //}

        } else
        {
            GoStraight();

            //this.transform.Rotate(Vector3.up, -90);


            //controller.Move(moveVector * Time.deltaTime);
        }
        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    //TurnRight();

        //    //if (canTurn)
        //    //{
        //    TurnRight();
        //    //}
        //    //else
        //    //{
        //    //MoveLane(true);
        //    //}
        //}


        //// Calculate where we should be in the future
        //Vector3 targetPos = transform.position.z * Vector3.forward;
        //if (desiredLane == 0)
        //{
        //    targetPos += Vector3.left * PATH_WIDTH;
        //}
        //else if (desiredLane == 2)
        //{
        //    targetPos += Vector3.right * PATH_WIDTH;
        //}


        //// Calculate move vector
        ////Vector3 moveVector = Vector3.zero;
        //moveVector.x = (targetPos - transform.position).normalized.x * moveSpeed;
        //moveVector.y = -0.001f;
        //moveVector.z = moveSpeed;


        // Move Camera
        //controller.Move(moveVector * Time.deltaTime);

    }

    private Vector3 GoStraight()
    {
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
        //Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPos - transform.position).normalized.x * moveSpeed;
        moveVector.y = -0.001f;
        moveVector.z = moveSpeed;

        return moveVector;
    }


    void MoveLane(bool goingRight)
    {
        desiredLane += (goingRight) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);
    }


    //void TurnLeft()
    //{
    //    this.transform.Rotate(Vector3.up, -90);
    //}


    //void TurnRight()
    //{
    //    this.transform.Rotate(Vector3.up, 90);
    //}



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



    //var dir:int = 0;
    //if(rotateRight && rotateLeft) dir = 0;
    //else if(rotateRight) dir = 1;
    //elseif(rotateLeft) dir = -1;

    //gameObject.transform.RotateAround(Vector3.zero, Vector3.up, dir* 20 * Time.deltaTime);

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





















