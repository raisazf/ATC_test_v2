using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlaneLocation : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject marker;
    public GameObject GlobalSystem;
    public GameObject marker;
    public float radius = 1.0095f; // globe ball radius (unity units)
    public float latitude = 0f; // lat
    public float longitude = 0f; // long
    public float altitude = 0f;
    public float direction = 0f;
    private TouchScreenKeyboard overlayKeyboard;
    public static string inputText = "";

    void Start()
    {
        var planes = new GameObject[3];
        var buttons = new GameObject[3];

        //GameObject sphereTry = GameObject.CreatePrimitive(PrimitiveType.Sphere);

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
            planes[i] = Instantiate(marker, new Vector3(xPos, yPos, zPos), Quaternion.identity, GlobalSystem.transform);
            planes[i].tag = "Untagged";

            planes[i].transform.LookAt(Vector3.zero);
            planes[i].transform.Rotate(0f, 0f, i*direction, Space.Self);

        }
    }


    // Update is called once per frame
    void Update()
    {
        //overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        //if (overlayKeyboard != null)
        //    inputText = overlayKeyboard.text;
    }
}
