using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        constructionButtons[0].onClick.AddListener(() => provinceManager.constructBuilding(buildingsManager.buildings[0]));
        constructionButtons[0].onClick.AddListener(() => guiUpdater.updateBuildingsPanel());

        constructionButtons[1].onClick.AddListener(() => provinceManager.constructBuilding(buildingsManager.buildings[1]));
        constructionButtons[1].onClick.AddListener(() => guiUpdater.updateBuildingsPanel());
    }
}
