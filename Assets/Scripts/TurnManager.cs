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
        var playerCountry = gameData.countries.FirstOrDefault(c => c.countryTag == gameData.playingAsTag);
        foreach (var country in gameData.countries)
        {
            int totalIncome = gameData.provincesInformation.Where(p => p.owner == country.countryTag).Sum(p => p.cachedIncome);
            country.money += totalIncome;
        }

        guiUpdater.updateTopBar();
    }
}
