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
        foreach (var country in gameData.countries)
        {
            totalIncome = gameData.provincesInformation.Where(p => p.owner == country.countryTag).Sum(p => p.cachedIncome);
            int maxMoney = gameData.provincesInformation.Where(p => p.owner == country.countryTag).Sum(p => p.cachedMoneyStorage);

            country.maxMoney = maxMoney;

            int newCurrentMoney = country.money + totalIncome;
            if (newCurrentMoney >= maxMoney) country.money = maxMoney; else country.money = newCurrentMoney;
        }

        guiUpdater.updateTopBar(totalIncome);
    }
}
