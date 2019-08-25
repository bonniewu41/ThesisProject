
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
        mainCamera.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -3);
        //mainCamera.transform.localPosition = new Vector3 ( 0, 0, 0 );
		//mainCamera.transform.localRotation = Quaternion.Euler (18, 180, 0);
	
	}

    // Update is called once per frame
    void Update()
    {
        FollowCam();
        MoveLR();

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

        // Debug.Log("yaw " + yaw + "   pitch " + pitch);
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























