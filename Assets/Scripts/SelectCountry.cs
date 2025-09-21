using UnityEngine;

public class SelectCountry : MonoBehaviour
{
    GameData gameData;
    GUIUpdater guiUpdater;

    void Start()
    {
        gameData = GetComponent<GameData>();
        guiUpdater = GetComponent<GUIUpdater>();
    }

    public void SelectCountryTag(string countryTag)
    {
        gameData.playingAsTag = countryTag;
        guiUpdater.updateTopBar();
    }
}
