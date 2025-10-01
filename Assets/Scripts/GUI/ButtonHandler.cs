using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] List<Button> constructionButtons;
    ProvinceManager provinceManager;
    BuildingsManager buildingsManager;
    GUIUpdater guiUpdater;

    void Start()
    {
        provinceManager = GetComponent<ProvinceManager>();
        buildingsManager = GetComponent<BuildingsManager>();
        guiUpdater = GetComponent<GUIUpdater>();
        for (int i = 0; i < constructionButtons.Count; i++)
        {
            int index = i;
            TMP_Text btnText = constructionButtons[index].GetComponentInChildren<TMP_Text>();
            btnText.text = $"Build {buildingsManager.buildings[index].buildingName} - {buildingsManager.buildings[index].cost} money";

            constructionButtons[i].onClick.AddListener(() => provinceManager.constructBuilding(buildingsManager.buildings[index]));
            constructionButtons[i].onClick.AddListener(() => guiUpdater.updateBuildingsPanel());
            constructionButtons[i].onClick.AddListener(() => guiUpdater.updateTopBar());
        }

    }

    public void isBuilt()
    {
        for (int i = 0; i < constructionButtons.Count; i++)
        {
            int index = i;
            if (provinceManager.isConstructedBuilding(buildingsManager.buildings[index]))
            {
                constructionButtons[index].interactable = false;
            }
            else
            {
                constructionButtons[index].interactable = true;
            }
        }
    }
}
