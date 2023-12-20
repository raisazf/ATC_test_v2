using UnityEngine;
using System.Collections;
using RestSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class GetApiData : MonoBehaviour
{
    //[SerializeField] public Transform marker;
    [SerializeField] public string apiUrl = "https://airlabs.co/api/v9/"; // Replace with the actual AirLab API endpoint URL
    [SerializeField] public string apiKey = "&api_key=a206d42c-783a-494c-a21b-86bfaccdd9fd"; // Replace with your AirLab API key
    [SerializeField] public string endpoint_airport = "airports?iata_code=IAD"; //airport
    [SerializeField] public string endpoint_flights = "flights?flag=US,flight_iata=UA";
    [SerializeField] public float radius = 5; // globe ball radius (unity units)
    [SerializeField] public bool isAirport = false;
    public airports AirpotsResponse;
    public flights FlightResponse;
    public GameObject GlobalSystem;

    //public float latitude = 38.5072f; // lat
    //public float longitude = 77.1275f; // long

    private string newURL;
    private float latitude;
    private float longitude;
    private float altitude;
    private float direction;

    public void Start()
    {
        //StartCoroutine(GetDataFromAirLabApi());
        AirpotsResponse = new airports();
        FlightResponse = new flights();
        //plane = Resources.Load("Assets/Resources/PlaneHolder") as GameObject;
        Debug.Log(message: $"READY TO FLY");

        GetDataFromAirLabApi();
    }

    public flights GetDataFromAirLabApi()
    {

        //if (isAirport)
        //{
        //    //newURL = string.Concat(apiUrl, endpoint_airport, apiKey);
        //}
        //else
        //{
            //newURL = string.Concat(apiUrl, endpoint_flights, apiKey);
        //}

        //var client = new RestClient("https://airlabs.co/api/v9/flight?flight_iata=UA2029&api_key=a206d42c-783a-494c-a21b-86bfaccdd9fd");
        //var client = new RestClient("https://airlabs.co/api/v9/flights?view=array&_fields=hex,flag,lat,lng,dir,alt&api_key=a206d42c-783a-494c-a21b-86bfaccdd9fd");
        //var client = new RestClient("https://airlabs.co/api/v9/flights?fields=flag,lat,lng,dir,alt&api_key=a206d42c-783a-494c-a21b-86bfaccdd9fd&bbox=30,-90,37,-70");
        var client = new RestClient("https://airlabs.co/api/v9/flights?airline_iata=UA,fields=reg_number,lat,lng,dir,alt&bbox=30,-90,37,-70&api_key=a206d42c-783a-494c-a21b-86bfaccdd9fd");

        //Debug.Log($"{newURL}");
        //var client = new RestClient(newURL);

        var request = new RestRequest();


        RestResponse response = client.Execute(request);

        Debug.Log(message: $"{response.Content}");

        if (isAirport)
        {
            AirpotsResponse = AirportsResponse(response);
            return null;
        }
        else
        {
            FlightResponse = FlightsResponse(response);
            Debug.Log(message: $"number of flights = {FlightResponse.response.Count}");

            return FlightResponse;
        }


    }

    public airports AirportsResponse(RestResponse response)
    {

        airports AirpotsResponse = JsonConvert.DeserializeObject<airports>(response.Content);
        Debug.Log($"{AirpotsResponse.response.lat}");
        Debug.Log($"{AirpotsResponse.response.lng}");
        Debug.Log($"{AirpotsResponse.response.alt}");

        return AirpotsResponse;
    }

    public flights FlightsResponse(RestResponse response)
    {
        flights flightResponse = JsonConvert.DeserializeObject<flights>(response.Content);
        //var something = new GetApiData();
        GameObject plane = GameObject.FindGameObjectWithTag("Plane");
        PlaneLocation(flightResponse, plane);
        //Debug.Log(message: $"Flight1: {flightResponse.response[0].reg_number}");
        //Debug.Log($"Flight1: {flightResponse.response[0].lat}");
        //Debug.Log($"Flight1: {flightResponse.response[0].lng}");
        //Debug.Log($"Flight1: {flightResponse.response[0].alt}");
        //Debug.Log("");

        //Debug.Log($"Flight2: {flightResponse.response[1].flag}");
        //Debug.Log($"Flight2: {flightResponse.response[1].lat}");
        //Debug.Log($"Flight2: {flightResponse.response[1].lng}");
        //Debug.Log($"Flight2: {flightResponse.response[1].alt}");


        return flightResponse;
    }

    public void PlaneLocation(flights flightResponse, GameObject plane)
    {

        // calculation code taken from 
        // @miquael http://www.actionscript.org/forums/showthread.php3?p=722957#post722957
        // convert lat/long to radians
        // earth radius in feet 2.093e+7


        for (int i = 0; i < flightResponse.response.Count; i++)
        {
            //Instantiate("PlaneHolder", new Vector3(0, 0, 0), Quaternion.identity);
            //GameObject pplane = Instantiate(Resources.Load("Assets/Resources/PlaneHolder", typeof(GameObject))) as GameObject;
            //plane = Resources.Load<GameObject>("Assets/Resources/PlaneHolder");
            //plane = Resources.Load<GameObject>("Assets/Resources/PlaneHolder");
            //Debug.Log(message: $"Debugging");
            Instantiate(plane, Vector3.zero, Quaternion.identity);
            plane.transform.parent = GlobalSystem.transform;

            //GameObject instance = Instantiate(Resources.Load("Assets/Resources/PlaneHolder", typeof(GameObject))) as GameObject;

            latitude = Mathf.PI * flightResponse.response[i].lat / 180;
            longitude = Mathf.PI * flightResponse.response[i].lng / 180;

            // adjust position by radians	
            latitude -= 1.570795765134f; // subtract 90 degrees (in radians)

            // and switch z and y (since z is forward)
            float xPos = (radius) * Mathf.Sin(latitude) * Mathf.Cos(longitude);
            float zPos = (radius) * Mathf.Sin(latitude) * Mathf.Sin(longitude);
            float yPos = (radius) * Mathf.Cos(latitude);


            // move marker to position
            plane.transform.position = new Vector3(xPos, yPos, zPos);
            Debug.Log(message: $" Plane Location { xPos}, { yPos}, {zPos}");
        }
    }

}
