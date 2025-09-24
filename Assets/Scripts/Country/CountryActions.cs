using System.Linq;
using UnityEngine;

public class CountryActions : MonoBehaviour
{
    GameData gameData;
    void Start()
    {
        gameData = GetComponent<GameData>();
    }

    public void DeclareWar(string offender, string defender)
    {
        var offenderCountry = gameData.countries.FirstOrDefault(p => p.countryTag == offender);
        var defenderCountry = gameData.countries.FirstOrDefault(p => p.countryTag == defender);

        if (offenderCountry == null || defenderCountry == null)
        {
            Debug.LogError("Invalid offender or defender tag");
            return;
        }

        gameData.warList.Add((offender, defender));
    }
}
