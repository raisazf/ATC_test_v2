using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlaneLocation : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject marker;
    public GameObject parent;
    public float radius = 5; // globe ball radius (unity units)
    public float latitude = 0f; // lat
    public float longitude = 0f; // long
    public float altitude = 0f;
    public float direction = 0f;

    void Start()
    {

            //GameObject sphereTry = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            GameObject marker = GameObject.FindGameObjectWithTag("Plane");
        for (int i = 0; i < 3; i++)
        {
            //Instantiate(sphereTry, Vector3.zero, Quaternion.identity);
            //Instantiate(marker, Vector3.zero, Quaternion.identity, parent.transform);
            //Instantiate(marker, Vector3.zero, Quaternion.identity);

            latitude = 38.94846f;
            longitude = -77.44057f;

            latitude = Mathf.PI * latitude / 180;
            longitude = Mathf.PI * longitude / 180;

            float newRadius = (float)((float)(2.093e7 + (altitude + i*10000f)) * radius / 2.093e7);
            float xPos = (newRadius) * Mathf.Cos(latitude) * Mathf.Cos(longitude);
            float zPos = (newRadius) * Mathf.Cos(latitude) * Mathf.Sin(longitude);
            float yPos = (newRadius) * Mathf.Sin(latitude);


            //GameObject plane = Instantiate(marker, new Vector3(xPos, yPos, zPos), Quaternion.Euler(0, i*direction, 0 ), parent.transform);
            GameObject plane = Instantiate(marker, new Vector3(xPos, yPos, zPos), Quaternion.identity, parent.transform);
            plane.tag = "Untagged";

            plane.transform.LookAt(Vector3.zero);
            plane.transform.Rotate(0f, 0f, i*direction, Space.Self);

            Debug.Log(message: $" Plane Location { marker.transform.position}");
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
