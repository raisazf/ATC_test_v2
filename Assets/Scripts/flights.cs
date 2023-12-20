using System.Collections.Generic;

[System.Serializable]
public class FlightsEmbeddedField
{
    public string reg_number;
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