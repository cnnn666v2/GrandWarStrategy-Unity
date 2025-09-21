using UnityEngine;
using TMPro;

public class GUIUpdater : MonoBehaviour
{
    GUIChanges gui;
    ClickProvince clickProvince;
    GameData gameData;
    [SerializeField] GameObject panel, panel2;
    [SerializeField] TMP_Text provinceName, provincePopulation, provinceNeighbours, provinceOwner, provinceID, provinceBuildingCount, provinceBuildingList, topPlayingAs, topMoney, topTurn;


    void Start()
    {
        gui = GetComponent<GUIChanges>();
        clickProvince = GetComponent<ClickProvince>();
        gameData = GetComponent<GameData>();
    }

    public void updateProvincePanel()
    {
        provinceName.text = clickProvince.province.data.provinceName;
        gui.updateText(provincePopulation, "Province population: ", clickProvince.province.data.population.ToString());
        gui.updateText(provinceOwner, "Owner: ", clickProvince.province.data.owner);
        gui.updateText(provinceID, "ID: ", clickProvince.province.data.id.ToString());

        string printedNeighbours = "";
        foreach (var n in clickProvince.province.neighbours) printedNeighbours += n.name + ", ";
        gui.updateText(provinceNeighbours, "Neighbours: ", printedNeighbours);
    }

    public void updateBuildingsPanel()
    {
        gui.updateText(provinceBuildingCount, "Total buildings: ", clickProvince.province.data.buildings.Count + "/" + clickProvince.province.data.buildingLimit.ToString());

        string printedBuildings = "";
        foreach (var b in clickProvince.province.data.buildings) printedBuildings += b + ", ";
        gui.updateText(provinceBuildingList, "Constructed buildings: ", printedBuildings);
    }

    public void updateTopBar()
    {
        string countryName = "";
        string money = "";
        for (int i = 0; i < gameData.countries.Count; i++) if (gameData.countries[i].countryTag == gameData.playingAsTag) {
                countryName = gameData.countries[i].countryName;
                money = gameData.countries[i].money.ToString();
            }
        gui.updateText(topPlayingAs, "Controlling: ", countryName);
        gui.updateText(topMoney, "Money: ", money);
        gui.updateText(topTurn, "Turn: ", gameData.turnCount.ToString());
    }
}
