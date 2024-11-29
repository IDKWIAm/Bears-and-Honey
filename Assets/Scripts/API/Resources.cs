using System.Collections.Generic;

[System.Serializable]
public struct Resources
{
    public Resources(int newEnergy = 0, int newCrystals = 0)
    {
        energy = newEnergy;
        crystals = newCrystals;
        hats = new List<string>();
        dishes = new Dictionary<string, int>();
        spotsData = new Dictionary<string, string>();
    }

    public int energy;
    public int crystals;
    public List<string> hats;
    public Dictionary<string, int> dishes;
    public Dictionary<string, string> spotsData;
}
