using System.Collections.Generic;

[System.Serializable]
public struct Resources
{
    public Resources(int newEnergy = 0, int newCrystals = 0, Dishes newDishes = new Dishes(), BuildingSpotData newSpotData = new BuildingSpotData())
    {
        energy = newEnergy;
        crystals = newCrystals;
        hats = new List<string>();
        dishes = newDishes;
        spotsData = newSpotData;
    }

    public int energy;
    public int crystals;
    public List<string> hats;
    public Dishes dishes;
    public BuildingSpotData spotsData;
}
