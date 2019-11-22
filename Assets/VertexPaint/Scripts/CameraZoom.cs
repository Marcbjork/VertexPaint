using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public int zoom = 20;
    public int normal = 60;
    public float smooth = 5;

    void Update()
    {
        //Simple mousescroll when using the wheel.
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, zoom, Time.deltaTime * smooth);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, normal, Time.deltaTime * smooth);
        }
    }

}
