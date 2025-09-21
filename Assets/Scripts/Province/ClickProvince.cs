using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickProvince : MonoBehaviour
{

    public float maxDistance = 100f;
    public LayerMask hitLayers = 6;
    private MeshRenderer selectedProvinceRenderer;
    private Color selectedProvinceColor;
    private GUIChanges gui;
    private GUIUpdater guiUpdater;
    private ProvinceManager provinceManager;
    public ProvinceData province;
    [SerializeField] GameObject panel, panel2;

    void Start()
    {
        gui = GetComponent<GUIChanges>();
        guiUpdater = GetComponent<GUIUpdater>();
        provinceManager = GetComponent<ProvinceManager>();
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

                guiUpdater.updateProvincePanel();
                guiUpdater.updateBuildingsPanel();

            }
            else
            {
                Debug.Log("No hit");
                if (selectedProvinceRenderer) selectedProvinceRenderer.material.color = selectedProvinceColor;
                gui.hidePanel(panel);
                gui.hidePanel(panel2);
            }
        }
    }
}
