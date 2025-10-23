// TODO: completely rewrite this broken shit i swear i will kill myself

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using GrandWarStrategy.Utility;
using GrandWarStrategy.Province;
using GrandWarStrategy.Division;

namespace GrandWarStrategy.Logic
{
    public class ClickProvince : MonoBehaviour
    {
        public ClickMode clickMode;
        private Country selectedCountry;
        public ProvinceData province;
        public Army selectedArmy;

        public float maxDistance = 100f;
        [SerializeField] LayerMask hitLayers;

        private GUIChanges gui;
        private GUIUpdater guiUpdater;
        private GameData gameData;
        private ButtonHandler buttonHandler;

        private ProvinceManager provinceManager;
        private Color selectedProvinceColor;
        private List<MeshRenderer> selectedProvinces = new List<MeshRenderer>();
        private MeshRenderer selectedProvinceRenderer;

        [SerializeField] GameObject panel, panel2, panel3;
        [SerializeField] Dictionary<int, ProvinceData> provinceLookup = new Dictionary<int, ProvinceData>();

        void Start()
        {
            gui = GetComponent<GUIChanges>();
            guiUpdater = GetComponent<GUIUpdater>();
            buttonHandler = GetComponent<ButtonHandler>();
            provinceManager = GetComponent<ProvinceManager>();
            gameData = GetComponent<GameData>();

            clickMode = ClickMode.province;

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
                    switch (clickMode)
                    {
                        case ClickMode.province:
                            Debug.Log("Hit: " + hit.collider.name + " at " + hit.point);
                            if (hit.collider.tag != "province")
                            {
                                Debug.Log("Hit object isn't a \"province\" tag");
                                return;
                            }

                            paintProvinces(false);

                            province = hit.collider.GetComponent<ProvinceData>();
                            paintProvince(hit);
                            provinceManager.selectedProvince = province.id;

                            buttonHandler.isBuilt();

                            hidePanels();
                            gui.showPanel(panel);
                            gui.showPanel(panel2);

                            guiUpdater.updateProvincePanel();
                            guiUpdater.updateBuildingsPanel();
                            break;
                        case ClickMode.army:
                            Debug.Log("Hit: " + hit.collider.name + " at " + hit.point);
                            if (hit.collider.tag != "army")
                            {
                                Debug.Log("Hit object isn't an \"army\" tag");
                                return;
                            }

                            selectedArmy = hit.collider.GetComponent<Army>();

                            if (selectedArmy.owner != gameData.playingAsTag)
                            {
                                Debug.Log($"Selected: {selectedArmy.owner} // Playing as: {gameData.playingAsTag}");
                                return;
                            }

                            selectedArmy.GetComponent<Transform>().Find("BackgroundImg").GetComponent<SpriteRenderer>().color = Color.green;
                            break;
                        default:
                            Debug.LogError("[ClickProvince/Error]: Provided wrong or none clickMode!");
                            break;
                    }
                }
                else
                {
                    paintProvinces(false);
                    Debug.Log("No hit");
                    if (selectedProvinceRenderer) selectedProvinceRenderer.material.color = selectedProvinceColor;
                    if (selectedArmy) selectedArmy.GetComponent<Transform>().Find("BackgroundImg").GetComponent<SpriteRenderer>().color = Color.blue;
                    selectedArmy = null;
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

                    selectedCountry = gameData.countries.FirstOrDefault(c => c.countryTag == province.owner);
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
            selectedProvinceColor = province.color;
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
            else if (selectedCountry != null)
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


    public enum ClickMode { province = 0, army = 1 };
}