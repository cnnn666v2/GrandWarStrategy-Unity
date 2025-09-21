using UnityEngine;
using System.Linq;

public class ProvinceManager : MonoBehaviour
{
    GameData gameData;
    public int selectedProvince;
    void Start()
    {
        gameData = GetComponent<GameData>();
    }

    public void constructBuilding(Building newBuilding)
    {
        var playerCountry = gameData.countries.FirstOrDefault(c => c.countryTag == gameData.playingAsTag);

        if (playerCountry == null) 
            return;

        if (playerCountry.money < newBuilding.cost)
            return;

        foreach (var province in gameData.provincesInformation)
        {
            if (province.owner == gameData.playingAsTag &&
                province.id == selectedProvince &&
                province.buildingLimit > province.buildings.Count &&
                !province.buildings.Contains(newBuilding))
            {
                playerCountry.money -= newBuilding.cost;
                province.AddBuilding(newBuilding);
                break;
            }
        }
    }
}
