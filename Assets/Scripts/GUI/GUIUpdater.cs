using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class GUIUpdater : MonoBehaviour
{
    GUIChanges gui;
    ClickProvince clickProvince;
    CountryActions countryAction;
    GameData gameData;
    [SerializeField] GameObject panel, panel2, panel3;
    [SerializeField]
    TMP_Text provinceName, provincePopulation, provinceNeighbours, provinceOwner, provinceID,
                                provinceBuildingCount, provinceBuildingList,
                                topPlayingAs, topMoney, topTurn,
                                diplomacyCountry, diplomacyGovernment, diplomacyAtWar;
    [SerializeField] Button diplomacyWar;
    int lastIncome;


    void Start()
    {
        gui = GetComponent<GUIChanges>();
        clickProvince = GetComponent<ClickProvince>();
        gameData = GetComponent<GameData>();
        countryAction = GetComponent<CountryActions>();
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
        string money = "", maxMoney = "";
        for (int i = 0; i < gameData.countries.Count; i++) if (gameData.countries[i].countryTag == gameData.playingAsTag)
            {
                countryName = gameData.countries[i].countryName;
                money = gameData.countries[i].money.ToString();
                maxMoney = gameData.countries[i].maxMoney.ToString();
            }
        gui.updateText(topPlayingAs, "Controlling: ", countryName);
        gui.updateText(topMoney, "Money: ", $"{money} / {maxMoney} (<color=green>+{lastIncome}</color>)");
        gui.updateText(topTurn, "Turn: ", gameData.turnCount.ToString());
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
        var selectedCountry = gameData.countries.FirstOrDefault(c => c.countryTag == clickProvince.province.data.owner);

        if (selectedCountry == null) return;

        var enemies = gameData.warList
            .Where(w => w.offender == selectedCountry.countryTag || w.defender == selectedCountry.countryTag)
            .Select(w => w.offender == selectedCountry.countryTag ? w.defender : w.offender)
            .Distinct()
            .ToList();

        var enemyNames = enemies
            .Select(tag => gameData.countries.FirstOrDefault(c => c.countryTag == tag)?.countryName ?? tag)
            .ToList();

        diplomacyCountry.text = selectedCountry.countryName;
        diplomacyGovernment.text = selectedCountry.government.governmentName;
        diplomacyAtWar.text = enemyNames.Count > 0 ? string.Join(", ", enemyNames) : "At peace";

        diplomacyWar.onClick.RemoveAllListeners();
        if(selectedCountry.countryTag != gameData.playingAsTag) diplomacyWar.onClick.AddListener(() => countryAction.DeclareWar(gameData.playingAsTag, selectedCountry.countryTag));
    }
}
