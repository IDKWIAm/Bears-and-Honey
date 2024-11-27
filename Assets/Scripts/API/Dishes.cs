using System.Collections.Generic;

[System.Serializable]
public struct Dishes
{
    public Dishes(Dictionary<string, int> newDish)
    {
        dish = newDish;
    }
    public Dictionary<string, int> dish;
}
