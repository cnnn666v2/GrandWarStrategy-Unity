using System.Collections.Generic;
using UnityEngine;

public class BuildingsManager : MonoBehaviour
{
    public List<Building> buildings = new List<Building>();
    void Start()
    {
        Bank bank = new Bank { buildingName = "Bank", cost = 250, goldStorage = 1000 };
        buildings.Add(bank);

        Market market = new Market { buildingName = "Market", cost = 100, income = 10 };
        buildings.Add(market);
    }
}
