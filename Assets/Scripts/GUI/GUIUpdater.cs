using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;

public class GUIUpdater : MonoBehaviour
{
    GUIChanges gui;
    ButtonHandler btnHandler;
    ClickProvince clickProvince;
    CountryActions countryAction;
    GameData gameData;
    WarManager warManager;

    [SerializeField] GameObject panel, panel2, panel3, panel4;
    [SerializeField]
    TMP_Text provinceName, provincePopulation, provinceNeighbours, provinceOwner, provinceID,
                                provinceBuildingCount, provinceBuildingList,
                                topPlayingAs, topMoney, topTurn, topManpower,
                                diplomacyCountry, diplomacyGovernment, diplomacyAtWar, diplomacyRelations,
                                armyMax;
    [SerializeField] Button diplomacyWar;
    int lastIncome;


    void Start()
    {
        gui = GetComponent<GUIChanges>();
        btnHandler = GetComponent<ButtonHandler>();
        clickProvince = GetComponent<ClickProvince>();
        gameData = GetComponent<GameData>();
        countryAction = GetComponent<CountryActions>();
        warManager = GetComponent<WarManager>();
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
        foreach (Building b in clickProvince.province.data.buildings) printedBuildings += b + ", ";
        gui.updateText(provinceBuildingList, "Constructed buildings: ", printedBuildings);
        btnHandler.isBuilt();
    }

    public void updateTopBar()
    {
        string countryName = "";
        string money = "", maxMoney = "", population = "", manpower = "";
        for (int i = 0; i < gameData.countries.Count; i++) if (gameData.countries[i].countryTag == gameData.playingAsTag)
            {
                countryName = gameData.countries[i].countryName;
                money = gameData.countries[i].money.ToString();
                maxMoney = gameData.countries[i].maxMoney.ToString();
                population = gameData.countries[i].population.ToString();
                manpower = gameData.countries[i].manpower.ToString();
                break;
            }
        gui.updateText(topPlayingAs, "Controlling: ", countryName);
        gui.updateText(topMoney, "Money: ", $"{money} / {maxMoney} (<color=green>+{lastIncome}</color>)");
        gui.updateText(topTurn, "Turn: ", gameData.turnCount.ToString());
        gui.updateText(topManpower, "Manpower / Population: ", $"{manpower} / {population}");
    }

    public void updateTopBar(int income)
    {
        lastIncome = income;
        string countryName = "";
        string money = "", maxMoney = "";
        for (int i = 0; i < gameData.countries.Count; i++) if (gameData.countries[i].countryTag == gameData.playingAsTag)
            {
                countryName = gameData.countries[i].countryName;
                money = gameData.countries[i].money.ToString();
                maxMoney = gameData.countries[i].maxMoney.ToString();
            }
        gui.updateText(topPlayingAs, "Controlling: ", countryName);
        gui.updateText(topMoney, "Money: ", $"{money} / {maxMoney} (<color=green>+{income}</color>)");
        gui.updateText(topTurn, "Turn: ", gameData.turnCount.ToString());
    }

    public void updateDiplomacyPanel()
    {
        Country selectedCountry = gameData.countries.FirstOrDefault(c => c.countryTag == clickProvince.province.data.owner);
        if (selectedCountry == null) return;

        diplomacyRelations.text = "Peace";

        foreach (War war in gameData.wars)
        {
            if ((war.offenders.Contains(selectedCountry.countryTag) || war.defenders.Contains(selectedCountry.countryTag))
                && (war.offenders.Contains(gameData.playingAsTag) || war.defenders.Contains(gameData.playingAsTag)))
                diplomacyRelations.text = "At war";
        }

        diplomacyCountry.text = selectedCountry.countryName;
        diplomacyGovernment.text = selectedCountry.government.governmentName;
        List<string> enemies = warManager.GetEnemiesOf(selectedCountry.countryTag);
        if (enemies.Count > 0)
        {
            string printedEnemies = "";
            foreach (string enemy in enemies) printedEnemies += enemy + ", ";
            gui.updateText(diplomacyAtWar, "At war with: ", printedEnemies);
        }
        else diplomacyAtWar.text = "At war with no one";

        diplomacyWar.onClick.RemoveAllListeners();
        if (selectedCountry.countryTag != gameData.playingAsTag) diplomacyWar.onClick.AddListener(() => countryAction.DeclareWar(gameData.playingAsTag, selectedCountry.countryTag));
    }

    public void updateRecruitmentPanel()
    {
        string maxArmy = "", army = "";
        for (int i = 0; i < gameData.countries.Count; i++) if (gameData.countries[i].countryTag == gameData.playingAsTag)
            {
                army = gameData.countries[i].currentArmy.ToString();
                maxArmy = gameData.countries[i].maxArmy.ToString();
                break;
            }
        gui.updateText(armyMax, "Army: ", $"{army} / {maxArmy}");
    }
}
