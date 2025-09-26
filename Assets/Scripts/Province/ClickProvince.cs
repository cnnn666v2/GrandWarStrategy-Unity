using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickProvince : MonoBehaviour
{

    public float maxDistance = 100f;
    public LayerMask hitLayers = 6;
    private MeshRenderer selectedProvinceRenderer;
    private Color selectedProvinceColor;
    private Country selectedCountry;
    private GUIChanges gui;
    private GUIUpdater guiUpdater;
    private ProvinceManager provinceManager;
    public ProvinceData province;
    private List<MeshRenderer> selectedProvinces = new List<MeshRenderer>();
    private GameData gameData;
    [SerializeField] GameObject panel, panel2, panel3;
    [SerializeField] Dictionary<int, ProvinceInformation> provinceLookup = new Dictionary<int, ProvinceInformation>();

    void Start()
    {
        gui = GetComponent<GUIChanges>();
        guiUpdater = GetComponent<GUIUpdater>();
        provinceManager = GetComponent<ProvinceManager>();
        gameData = GetComponent<GameData>();

        foreach (var province in gameData.provincesInformation)
        {
            provinceLookup[province.id] = province;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Ignore if clicked on UI
            if (EventSystem.current.IsPointerOverGameObject()) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, hitLayers))
            {
                province = hit.collider.GetComponent<ProvinceData>();
                Debug.Log("Hit: " + hit.collider.name + " at " + hit.point);
                if (hit.collider.tag != "province")
                {
                    Debug.Log("Hit object isn't a \"province\" tag");
                    return;
                }

                if (selectedProvinceRenderer) selectedProvinceRenderer.material.color = selectedProvinceColor;
                selectedProvinceRenderer = hit.collider.GetComponent<MeshRenderer>();
                selectedProvinceRenderer.material.color = Color.red;
                selectedProvinceColor = province.data.color;

                provinceManager.selectedProvince = province.data.id;

                gui.showPanel(panel);
                gui.showPanel(panel2);
                gui.hidePanel(panel3);

                guiUpdater.updateProvincePanel();
                guiUpdater.updateBuildingsPanel();

            }
            else
            {
                Debug.Log("No hit");
                if (selectedProvinceRenderer) selectedProvinceRenderer.material.color = selectedProvinceColor;
                gui.hidePanel(panel);
                gui.hidePanel(panel2);
                gui.hidePanel(panel3);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, hitLayers))
            {
                province = hit.collider.GetComponent<ProvinceData>();
                Debug.Log("Hit: " + hit.collider.name + " at " + hit.point);
                if (hit.collider.tag != "province")
                {
                    Debug.Log("Hit object isn't a \"province\" tag");
                    return;
                }

                selectedCountry = gameData.countries.FirstOrDefault(c => c.countryTag == province.data.owner);
                if (selectedProvinceColor == null) selectedProvinceColor = selectedCountry.color;
                paintProvinces();

                gui.showPanel(panel3);
                gui.hidePanel(panel);
                gui.hidePanel(panel2);
                guiUpdater.updateDiplomacyPanel();

            }
            else
            {
                if (selectedProvinceRenderer) selectedProvinceRenderer.material.color = selectedProvinceColor;
                Debug.Log("No hit");
                gui.hidePanel(panel);
                gui.hidePanel(panel2);
                gui.hidePanel(panel3);
            }
        }
    }

    private void paintProvinces()
    {
        if (selectedProvinces.Count > 0) foreach (var province in selectedProvinces) province.material.color = selectedCountry.color;
        selectedProvinces.Clear();

        if (selectedCountry != null)
        {
            selectedProvinceColor = selectedCountry.color;

            foreach (var paintProvinceId in selectedCountry.ownedProvinces)
            {
                if (provinceLookup.TryGetValue(paintProvinceId, out var provinceInfo))
                {
                    provinceInfo.GetComponent<MeshRenderer>().material.color = Color.blue;
                    selectedProvinces.Add(provinceInfo.GetComponent<MeshRenderer>());
                }
            }
        }
        else
        {
            foreach (var paintProvinceId in selectedCountry.ownedProvinces)
            {
                if (provinceLookup.TryGetValue(paintProvinceId, out var provinceInfo))
                {
                    provinceInfo.GetComponent<MeshRenderer>().material.color = selectedProvinceColor;
                }
            }
            return;
        }
    }
}
