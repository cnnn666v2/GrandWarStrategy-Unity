using System.Collections.Generic;
using UnityEngine;

namespace GrandWarStrategy.Logic
{
    public class BoardData : MonoBehaviour
    {
        GameData gameData;
        void Start()
        {
            gameData = GetComponent<GameData>();
            Country test = new Country { countryName = "Poland", countryTag = "POL", color = new Color(1, 1, 1, 1), government = gameData.governments[0], ownedProvinces = new List<int> { 0, 1, 2 } };
            Country test2 = new Country { countryName = "Germany", countryTag = "GER", color = new Color(0, 0, 0, 1), government = gameData.governments[0], ownedProvinces = new List<int> { 3, 4, 5 } };
            Country test3 = new Country { countryName = "Russia", countryTag = "RUS", color = new Color(0, 0, 0.9f, 1), government = gameData.governments[0], ownedProvinces = new List<int> { 6 } };

            gameData.countries.Add(test);
            gameData.countries.Add(test2);
            gameData.countries.Add(test3);

            PaintProvinces(test.ownedProvinces, test.color, test.countryTag);
            PaintProvinces(test2.ownedProvinces, test2.color, test2.countryTag);
            PaintProvinces(test3.ownedProvinces, test3.color, test3.countryTag);
        }

        void PaintProvinces(List<int> provinces, Color color, string country)
        {
            for (int i = 0; i < gameData.provincesInformation.Count; i++)
            {
                if (provinces.Contains(gameData.provincesInformation[i].id))
                {
                    gameData.provincesMeshRender[i].GetComponent<MeshRenderer>().material.color = color;
                    gameData.provincesInformation[i].owner = country;
                    gameData.provincesInformation[i].color = color;
                }
            }
        }
    }
}