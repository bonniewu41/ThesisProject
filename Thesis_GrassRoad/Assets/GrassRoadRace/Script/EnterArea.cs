using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterArea : MonoBehaviour
{

    public static bool canTurn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public bool OnTriggerEnter(Collider other)
    {
        canTurn = true;
        Debug.Log("Collision with " + other.gameObject.name + " canTurn? : " + canTurn);

        return canTurn;
    }

    public bool OnTriggerExit(Collider other)
    {
        canTurn = false;
        Debug.Log("Collision with " + other.gameObject.name + " canTurn? : " + canTurn);

        return canTurn;
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    //if (collision.gameObject.name == "Character")
    //    //{
    //        //isColliding = false;
    //        canTurn = true;
    //        Debug.Log("Collision with " + collision.gameObject.name + " canTurn? : " + canTurn);
    //    //}
    //}

    //void OnCollisionExit(Collision collision)
    //{
    //    //isColliding = false;
    //    canTurn = false;
    //    Debug.Log("Exit " + collision.gameObject.name + " bool result : " + canTurn);
    //}
}
