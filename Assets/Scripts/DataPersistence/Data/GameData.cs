[System.Serializable]
public class GameData
{
    public GameData(string newName)
    {
        name = newName;
        resources = new Resources();
    }

    public string name;
    public Resources resources;
}
