using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    void Awake()
    {
        //transform.position = new Vector3(0.0f, 0.0f, 0.0f);
    }
    void Update()
    {
        if (Input.GetKey("up"))
        {
            //Rotate object up
            transform.Rotate(1.0f,0f,0f, Space.World);
        }
        if (Input.GetKey("down"))
         {
            //Rotate object down
            transform.Rotate(-1.0f, 0f, 0f, Space.World);
        }
        if (Input.GetKey("left"))
        {
            //Rotera åt vänster eller höger
            transform.Rotate(0.0f, 1.0f, 0.0f, Space.World);
        }
        if (Input.GetKey("right"))
        {
            //Rotera åt vänster eller höger
            transform.Rotate(0.0f, -1.0f, 0f, Space.World);
        }
        if (Input.GetKey("r"))
        {   //Reset to start rotation
            ResetCube();
        }
    }

    void ResetCube()
    {
        //Reset the cubes rotation;
        transform.rotation = Quaternion.identity;
    }
}
