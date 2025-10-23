using System.Collections.Generic;
using UnityEngine;
using GrandWarStrategy.Buildings;

namespace GrandWarStrategy.Logic
{
    public class BuildingsManager : MonoBehaviour
    {
        public List<Building> buildings = new List<Building>();
        void Start()
        {
            Bank bank = new Bank { buildingName = "Bank", cost = 150, constructionTime = 5, goldStorage = 500 };
            buildings.Add(bank);

            Market market = new Market { buildingName = "Market", cost = 100, constructionTime = 2, income = 10 };
            buildings.Add(market);

            Barracks barracks = new Barracks { buildingName = "Barracks", cost = 200, constructionTime = 3, maxSoldiers = 1000, maxRecruit = 100, costRecruit = 10 };
            buildings.Add(barracks);
        }
    }
}