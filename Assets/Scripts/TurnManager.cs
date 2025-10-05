using System.Linq;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    GameData gameData;
    GUIUpdater guiUpdater;

    void Start()
    {
        gameData = GetComponent<GameData>();
        guiUpdater = GetComponent<GUIUpdater>();
    }

    public void NextTurn()
    {
        gameData.turnCount++;
        int totalIncome = 0;
        //var playerCountry = gameData.countries.FirstOrDefault(c => c.countryTag == gameData.playingAsTag);
        foreach (Country country in gameData.countries)
        {
            totalIncome = gameData.provincesInformation.Where(p => p.owner == country.countryTag).Sum(p => p.cachedIncome);
            int maxMoney = gameData.provincesInformation.Where(p => p.owner == country.countryTag).Sum(p => p.cachedMoneyStorage);
            country.maxMoney = maxMoney;
            int newCurrentMoney = country.money + totalIncome;
            if (newCurrentMoney >= maxMoney) country.money = maxMoney; else country.money = newCurrentMoney;

            int totalPopulation = gameData.provincesInformation.Where(p => p.owner == country.countryTag).Sum(p => p.population);
            country.population = totalPopulation;
            country.manpower = Mathf.RoundToInt(totalPopulation * country.conscriptionModifier);
        }

        foreach (var province in gameData.provincesInformation)
        {
            for (int i = province.constructions.Count - 1; i >= 0; i--)
            {
                Construction construction = province.constructions[i];
                construction.turnsRemaining--;

                if (construction.turnsRemaining <= 0)
                {
                    province.ConstructBuilding(construction);
                }
            }
        }

        guiUpdater.updateTopBar(totalIncome);
    }
}
