using System.Collections.Generic;
using UnityEngine;

public class WarManager : MonoBehaviour
{
    GameData gameData;

    void Start()
    {
        gameData = GetComponent<GameData>();
    }

    public List<string> GetEnemiesOf(string country)
    {
        List<string> enemies = new List<string>();

        foreach (War war in gameData.wars)
        {

            // If country is an offender, enemies are defenders
            if (war.offenders.Contains(country))
            {
                foreach (var enemy in war.defenders)
                {
                    if (!enemies.Contains(enemy))
                        enemies.Add(enemy);
                }
            }

            // If country is a defender, enemies are offenders
            else if (war.defenders.Contains(country))
            {
                foreach (var enemy in war.offenders)
                {
                    if (!enemies.Contains(enemy))
                        enemies.Add(enemy);
                }
            }
        }

        return enemies;
    }

    public bool AreAtWar(string country1, string country2)
    {
        foreach (War war in gameData.wars)
        {
            if (war.offenders.Contains(country1) && war.defenders.Contains(country2))
                return true;

            if (war.defenders.Contains(country1) && war.offenders.Contains(country2))
                return true;
        }

        return false;
    }
}
