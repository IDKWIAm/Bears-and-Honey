using System.Collections.Generic;

[System.Serializable]
public struct LogStruct
{
    public string comment;
    public string player_name;
    public Dictionary<string, string> resources_changed;
}
