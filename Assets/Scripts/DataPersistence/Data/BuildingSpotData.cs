using System.Collections.Generic;

[System.Serializable]
public struct BuildingSpotData
{
    public BuildingSpotData(Dictionary<string, string> newSpotData)
    {
        spotData = new Dictionary<string, string>();
    }

    public Dictionary<string, string> spotData;
}
