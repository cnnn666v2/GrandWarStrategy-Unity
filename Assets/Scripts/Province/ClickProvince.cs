// TODO: completely rewrite this broken shit i swear i will kill myself

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickProvince : MonoBehaviour
{

    public float maxDistance = 100f;
    public LayerMask hitLayers = 6;

    private GUIChanges gui;
    private GUIUpdater guiUpdater;
    private GameData gameData;

    private Country selectedCountry;
    private ProvinceManager provinceManager;
    public ProvinceData province;
    private Color selectedProvinceColor;
    private List<MeshRenderer> selectedProvinces = new List<MeshRenderer>();
    private MeshRenderer selectedProvinceRenderer;

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
            if (EventSystem.current.IsPointerOverGameObject()) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, hitLayers))
            {
                Debug.Log("Hit: " + hit.collider.name + " at " + hit.point);
                if (hit.collider.tag != "province")
                {
                    Debug.Log("Hit object isn't a \"province\" tag");
                    return;
                }

                paintProvinces(false);

                province = hit.collider.GetComponent<ProvinceData>();
                paintProvince(hit);
                provinceManager.selectedProvince = province.data.id;

                hidePanels();
                gui.showPanel(panel);
                gui.showPanel(panel2);

                guiUpdater.updateProvincePanel();
                guiUpdater.updateBuildingsPanel();
            }
            else
            {
                paintProvinces(false);
                Debug.Log("No hit");
                if (selectedProvinceRenderer) selectedProvinceRenderer.material.color = selectedProvinceColor;
                hidePanels();
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

                if (selectedProvinceRenderer) selectedProvinceRenderer.material.color = selectedProvinceColor;

                selectedCountry = gameData.countries.FirstOrDefault(c => c.countryTag == province.data.owner);
                paintProvinces(true);

                hidePanels();
                gui.showPanel(panel3);

                guiUpdater.updateDiplomacyPanel();

            }
            else
            {
                if (selectedProvinceRenderer) selectedProvinceRenderer.material.color = selectedProvinceColor;
                paintProvinces(false);
                Debug.Log("No hit");
                hidePanels();
            }
        }
    }

    private void paintProvince(RaycastHit hit)
    {
        if (selectedProvinceRenderer) selectedProvinceRenderer.material.color = selectedProvinceColor;
        selectedProvinceRenderer = hit.collider.GetComponent<MeshRenderer>();
        selectedProvinceRenderer.material.color = Color.red;
        selectedProvinceColor = province.data.color;
    }

    private void paintProvinces(bool isHitProvince)
    {
        if (selectedProvinces.Count > 0) foreach (var province in selectedProvinces) province.material.color = selectedProvinceColor;
        selectedProvinces.Clear();

        if (isHitProvince)
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
                    provinceInfo.GetComponent<MeshRenderer>().material.color = selectedProvinceColor;
            }
            return;
        }
    }

    private void hidePanels()
    {
        gui.hidePanel(panel);
        gui.hidePanel(panel2);
        gui.hidePanel(panel3);
    }
}
