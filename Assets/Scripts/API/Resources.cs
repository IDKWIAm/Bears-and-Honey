using System.Collections.Generic;

[System.Serializable]
public struct Resources
{
    public Resources(int newEnergy = 0, int newCrystals = 0)
    {
        energyHoney = newEnergy;
        //crystals = newCrystals;
        hats = new List<string>();
        dishes = new List<string>();
        spotsData = new Dictionary<string, string>();
    }

    public int energyHoney;
    //public int crystals;
    public List<string> hats;
    public List<string> dishes;
    public Dictionary<string, string> spotsData;
}
