using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseIndicator : MonoBehaviour
{
    int segments = 10;
    float xradius = 0.6f;
    float yradius = 0.6f;
    LineRenderer line;
    
    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();

        line.positionCount = segments;
        line.useWorldSpace = false;
        SetupCircle();
    }
    void Update()
    {
        Ray worldRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        

        if (Physics.Raycast(worldRay, out hit, 500f))
        {
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            transform.position = hit.point;
            transform.rotation = Quaternion.FromToRotation(transform.forward, hit.normal) * transform.rotation;

            //transform.rotation = transform.rotation = new Quaternion(0f, 0f, 0f, 0f); 

        }
    }

    public void SetupCircle()
    {
        float x;
        float y;
        float z = 0f;

        float angle = 2f;

        

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition(i,new Vector3(x, y, z));

            //line.SetColors(Color.green, Color.green);

            angle += (360f / segments);

        }

    }

    public void GreenColor()
    {
        Debug.Log("GreenCOl");
        line.SetColors(Color.green, Color.green);
        

    }
    public void BlueColor()
    {
        line.endColor = Color.green;
    }
}
