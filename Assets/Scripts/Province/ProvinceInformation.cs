using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProvinceInformation : MonoBehaviour
{
    public string provinceName;
    public int population;
    public int id;
    public string owner = "N/A";
    public Color color;
    public bool isCapital = false;
    public int infrastructure = 100;
    public int infrastructureLimit = 250;
    public string resource = "Wood"; // treated as string for easier modding in future

    //Buildings
    public int buildingLimit = 5;
    public List<Building> buildings = new List<Building>();
    public List<Construction> constructions = new List<Construction>();
    
    // Cache
    public int cachedIncome;
    public int cachedMoneyStorage;

    void Start()
    {
        cachedIncome = Mathf.RoundToInt(infrastructure / 5);
        cachedMoneyStorage = Mathf.RoundToInt(infrastructure / 2);
    }

    public void AddBuilding(Building building)
    {
        constructions.Add(new Construction(building, building.constructionTime));
    }

    public void ConstructBuilding(Construction construction)
    {
        Building building = construction.building;
        buildings.Add(building);
        if (building is Market market) cachedIncome += market.income;
        if (building is Bank bank) cachedMoneyStorage += bank.goldStorage;
        constructions.Remove(construction);
    }
}

public class Construction
{
    public Building building;
    public int turnsRemaining;

    public Construction(Building building, int turnsRemaining)
    {
        this.building = building;
        this.turnsRemaining = turnsRemaining;
    }
}
