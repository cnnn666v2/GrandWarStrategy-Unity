using System.Linq;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    GameData gameData;
    GUIUpdater guiUpdater;
    RecruitArmy recruitArmy; // TODO: move the Recruit() from that script over here

    void Start()
    {
        gameData = GetComponent<GameData>();
        guiUpdater = GetComponent<GUIUpdater>();
        recruitArmy = GetComponent<RecruitArmy>();
    }

    public void NextTurn()
    {
        gameData.turnCount++;
        int totalIncome = 0;
        //var playerCountry = gameData.countries.FirstOrDefault(c => c.countryTag == gameData.playingAsTag);
        foreach (Country country in gameData.countries)
        {
            totalIncome = gameData.provincesInformation.Where(p => p.owner == country.countryTag).Sum(p => p.cachedIncome - p.cachedMaintenance);
            int maxMoney = gameData.provincesInformation.Where(p => p.owner == country.countryTag).Sum(p => p.cachedMoneyStorage);
            country.maxMoney = maxMoney;
            int newCurrentMoney = country.money + totalIncome;
            if (newCurrentMoney >= maxMoney) country.money = maxMoney; else country.money = newCurrentMoney;

            int totalPopulation = gameData.provincesInformation.Where(p => p.owner == country.countryTag).Sum(p => p.population);
            country.population = totalPopulation;
            country.manpower = Mathf.RoundToInt(totalPopulation * country.conscriptionModifier);

            int maxArmy = gameData.provincesInformation.Where(p => p.owner == country.countryTag).Sum(p => p.cachedMaxArmy);
            country.maxArmy = maxArmy;
        }

        foreach (ProvinceData province in gameData.provincesInformation)
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

        MoveDivisions();
        TrainDivisions();

        guiUpdater.updateTopBar(totalIncome);
        guiUpdater.updateDiplomacyPanel();
        guiUpdater.updateRecruitmentPanel();
        guiUpdater.updateBuildingsPanel();
        guiUpdater.updateProvincePanel();
    }

    private void MoveDivisions()
    {
        for (int i = gameData.movingDivisions.Count - 1; i >= 0; i--)
        {
            MovingDivisions movingDivision = gameData.movingDivisions[i];
            movingDivision.travelTime--;

            if (movingDivision.travelTime <= 0)
            {
                movingDivision.army.stayingIn.RemoveArmy(movingDivision.army);
                Vector3 destinationVector = movingDivision.destination.GetComponent<Renderer>().bounds.center;
                movingDivision.destination.AddArmy(movingDivision.army);
                movingDivision.army.stayingIn = movingDivision.destination;
                Transform armyPosition = movingDivision.army.GetComponent<Transform>();
                armyPosition.position = new Vector3(destinationVector.x, armyPosition.position.y, destinationVector.z);
                gameData.movingDivisions.RemoveAt(i);
            }
        }
    }
    
    private void TrainDivisions()
    {
        for (int i = gameData.trainingDivisions.Count - 1; i >= 0; i--)
        {
            TrainingDivisions trainDivision = gameData.trainingDivisions[i];
            trainDivision.trainingTime--;

            if (trainDivision.trainingTime <= 0)
            {
                recruitArmy.Recruit(trainDivision.amount, trainDivision.cost, trainDivision.maintenance, trainDivision.trainingDestination);
            }
        }
    }
}
