using System.Collections.Generic;
using UnityEngine;

public class ProvinceInformation : MonoBehaviour
{
    public string provinceName;
    public int population;
    public int id;
    public string owner = "N/A";
    public Color color;
    public bool isCapital = false;
    public int buildingLimit = 5;
    public int cachedIncome;
    public List<Building> buildings = new List<Building>();

    public void AddBuilding(Building building)
    {
        buildings.Add(building);
        if (building is Market market) cachedIncome += market.income;
    }
}
