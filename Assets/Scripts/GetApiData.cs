using UnityEngine;
using System.Collections;
using RestSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class GetApiData : MonoBehaviour
{
    [SerializeField] public string apiUrl = "https://airlabs.co/api/v9/"; // Replace with the actual AirLab API endpoint URL
    [SerializeField] public string apiKey = "&api_key=a206d42c-783a-494c-a21b-86bfaccdd9fd"; // Replace with your AirLab API key
    [SerializeField] public string endpoint_airport = "airports?iata_code=IAD"; //airport
    [SerializeField] public string endpoint_flights = "flights?flag=US,flight_iata=UA";
    [SerializeField] public bool isAirport = false;
    public airports AirpotsResponse;
    public flights FlightResponse;

    private string newURL;

    public void Start()
    {
        //StartCoroutine(GetDataFromAirLabApi());
        AirpotsResponse = new airports();
        FlightResponse = new flights();

        GetDataFromAirLabApi();
    }

    public flights GetDataFromAirLabApi()
    {

        if (isAirport)
        {
            newURL = string.Concat(apiUrl, endpoint_airport, apiKey);
        }
        else
        {
            newURL = string.Concat(apiUrl, endpoint_flights, apiKey);
        }

        //var client = new RestClient("https://airlabs.co/api/v9/flight?flight_iata=UA2029&api_key=a206d42c-783a-494c-a21b-86bfaccdd9fd");
        //var client = new RestClient("https://airlabs.co/api/v9/flights?_view=array&_fields=hex,flag,lat,lng,dir,alt&api_key=a206d42c-783a-494c-a21b-86bfaccdd9fd");
        //var client = new RestClient("https://airlabs.co/api/v9/flights?bbox,&_fields=flag,lat,lng,dir,alt&api_key=a206d42c-783a-494c-a21b-86bfaccdd9fd");

        Debug.Log($"{newURL}");
        var client = new RestClient(newURL);

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

    private static airports  AirportsResponse(RestResponse response)
    {

        airports AirpotsResponse = JsonConvert.DeserializeObject<airports>(response.Content);
        Debug.Log($"{AirpotsResponse.response.lat}");
        Debug.Log($"{AirpotsResponse.response.lng}");
        Debug.Log($"{AirpotsResponse.response.alt}");

        return AirpotsResponse;
    }

    private static flights FlightsResponse(RestResponse response)
    {
        flights flightResponse = JsonConvert.DeserializeObject<flights>(response.Content);

        //Debug.Log($"Flight1: {FlightResponse.response[0].flag}");
        //Debug.Log($"Flight1: {FlightResponse.response[0].lat}");
        //Debug.Log($"Flight1: {FlightResponse.response[0].lng}");
        //Debug.Log($"Flight1: {FlightResponse.response[0].alt}");
        //Debug.Log("");

        //Debug.Log($"Flight2: {flightResponse.response[1].flag}");
        //Debug.Log($"Flight2: {flightResponse.response[1].lat}");
        //Debug.Log($"Flight2: {flightResponse.response[1].lng}");
        //Debug.Log($"Flight2: {flightResponse.response[1].alt}");

        return flightResponse;
    }

}
