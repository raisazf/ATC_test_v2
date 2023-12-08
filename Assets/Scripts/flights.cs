using System.Collections.Generic;

[System.Serializable]
public class FlightsEmbeddedField
{
    public string flag;
    public float lat;
    public float lng;
    public float alt;
    public float dir;
}

[System.Serializable]
public class flights
{
    public List<FlightsEmbeddedField> response = null; // Embedded field in JSON
}