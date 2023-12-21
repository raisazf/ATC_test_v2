using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneLocation : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform marker;
    public GameObject parent;
    public float radius = 5; // globe ball radius (unity units)
    public float latitude = 0f; // lat
    public float longitude = 0f; // long

    void Start()
    {

        // calculation code taken from 
        // @miquael http://www.actionscript.org/forums/showthread.php3?p=722957#post722957
        // convert lat/long to radians

        latitude = Mathf.PI * latitude / 180;
        longitude = Mathf.PI * longitude / 180;

        // adjust position by radians	
        //latitude -= 1.570795765134f; // subtract 90 degrees (in radians)

        // and switch z and y (since z is forward)
        //float xPos = (radius) * Mathf.Sin(latitude) * Mathf.Cos(longitude);
        //float zPos = (radius) * Mathf.Sin(latitude) * Mathf.Sin(longitude);
        //float yPos = (radius) * Mathf.Cos(latitude);

        float xPos = (radius) * Mathf.Cos(latitude) * Mathf.Cos(longitude);
        float zPos = (radius) * Mathf.Cos(latitude) * Mathf.Sin(longitude);
        float yPos = (radius) * Mathf.Sin(latitude);

        // move marker to position
        marker.position = new Vector3(xPos, yPos, zPos);
        transform.LookAt(Vector3.zero);
        marker.transform.parent = parent.transform;
        Debug.Log(message:$" Plane Location { xPos}, { yPos}, {zPos}");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
