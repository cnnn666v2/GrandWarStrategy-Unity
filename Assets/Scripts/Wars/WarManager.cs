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
}
