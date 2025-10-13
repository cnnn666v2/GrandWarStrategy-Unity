using System.Linq;
using TMPro;
using UnityEngine;

public class RecruitArmy : MonoBehaviour
{
    GameData gameData;
    ClickProvince clickProvince;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] GameObject armyPrefab;
    [SerializeField] Transform armiesParent;
    //private bool selectMode = false;
    public Transform selectedProvince;

    //public float maxDistance = 100f;
    //public LayerMask hitLayers = 6;

    void Start()
    {
        gameData = GetComponent<GameData>();
        clickProvince = GetComponent<ClickProvince>();
    }

    public void PerformRecruit()
    {
        int amount = int.Parse(inputField.text);
        int cost = amount * 10;

        Recruit(amount, cost);
    }

    private void Recruit(int soldiers, int cost)
    {
        selectedProvince = clickProvince.province.GetComponent<Transform>();
        if (!selectedProvince) return;

        Country country = gameData.countries.FirstOrDefault(c => c.countryTag == gameData.playingAsTag);
        if ((country.currentArmy + soldiers) <= country.maxArmy && country.money >= cost)
        {
            country.currentArmy += soldiers;
            country.money -= cost;
            country.manpower -= soldiers;
            //selectedProvince.housingArmy = soldiers;

            Vector3 center = selectedProvince.GetComponent<Renderer>().bounds.center;

            GameObject army = Instantiate(armyPrefab, new Vector3(center.x, armiesParent.position.y, center.z), Quaternion.identity, armiesParent);
            army.GetComponentInChildren<TMP_Text>().text = soldiers.ToString();
            army.GetComponent<SpriteRenderer>().color = gameData.countries.FirstOrDefault(c => c.countryTag == gameData.playingAsTag).color;
            Army armyData = army.GetComponent<Army>();
            armyData.soldiers = soldiers;
            armyData.stayingIn = selectedProvince.GetComponent<ProvinceData>();
            armyData.owner = gameData.playingAsTag;
        }
    }

    public void SelectProvince()
    {
        clickProvince.clickMode = ClickMode.province;
    }

    /*void Update()
    {
        if (Input.GetMouseButtonDown(0) && selectMode)
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

                selectedProvince = hit.collider.GetComponent<Transform>();
                Debug.Log($"nigga Selected: {selectedProvince.GetComponent<ProvinceInformation>().id}");
            }
            else
            {
                Debug.Log("No nigga");
            }
            selectMode = false;
        }
    }*/
}
