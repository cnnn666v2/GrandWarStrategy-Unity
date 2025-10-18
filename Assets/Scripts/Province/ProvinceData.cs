using System.Collections.Generic;
using UnityEngine;

public class ProvinceData : MonoBehaviour
{
    //public ProvinceInformation data;
    public List<ProvinceData> neighbours;
    [SerializeField] Collider[] colliders;
    //public Color color;

    GameData gameData;

    // Province Information
    public string provinceName;
    public int population;
    public int id;
    public string owner = "N/A";
    public Color color;
    public bool isCapital = false;
    public int infrastructure = 100;
    public int infrastructureLimit = 250;
    public string resource = "Wood"; // treated as string for easier modding in future

    //Buildings
    public int buildingLimit = 5;
    public List<Building> buildings = new List<Building>();
    public List<Construction> constructions = new List<Construction>();

    // Cache
    public int cachedIncome;
    public int cachedMoneyStorage;
    public int cachedMaxArmy;
    public int cachedMaintenance;

    public List<Army> housingArmies = new List<Army>();

    void Start()
    {
        //data = GetComponent<ProvinceInformation>();

        //if (data != null)
        //{
        name = provinceName;
        gameData = GameObject.FindWithTag("gm").GetComponent<GameData>();

        colliders = GetComponentsInChildren<Collider>();
        List<Collider> tempColliders = new List<Collider>(colliders);
        tempColliders.Remove(GetComponent<Collider>());
        colliders = tempColliders.ToArray();

        /*Color newColor = Color.red;
        if (gameData.colors.Contains(newColor))
        {
            newColor = new Color(Random.Range(0f, 255f) / 255f, Random.Range(0f, 255f) / 255f, Random.Range(0f, 255f) / 255f, 0.5f);
        }
        gameData.colors.Add(newColor);
        color = newColor;
        GetComponent<MeshRenderer>().material.color = newColor;*/

        foreach (var col in colliders)
        {
            BoxCollider box = col as BoxCollider;
            Vector3 worldCenter = box.transform.TransformPoint(box.center);
            Vector3 worldHalfExtents = Vector3.Scale(box.size * 0.5f, box.transform.lossyScale);

            Collider[] hits = Physics.OverlapBox(worldCenter, worldHalfExtents, box.transform.rotation);
            Debug.Log($"[{name} // {col}]: Performing foreach\nHits: {hits[0]}");
            foreach (Collider hit in hits)
            {
                Debug.Log($"[{name}]: Collider ({col}) has hit: {hit}");
                ProvinceData hitParent = hit.GetComponentInParent<ProvinceData>();
                if (hit != col && hitParent != gameObject && !neighbours.Contains(hitParent))
                {
                    neighbours.Add(hitParent);
                }
            }
        }
        //GetComponent<MeshRenderer>().material.color = new Color(0f,0f,0f,1f);

        gameData.provincesInformation.Add(this);
        gameData.provincesMeshRender.Add(gameObject);

        /*MeshCollider childCollider = child.GetComponent<MeshCollider>();
        Transform childTransform = child.GetComponent<Transform>();
        hits = Physics.OverlapBox(transform.position, childCollider.bounds.extents, childTransform.rotation);
        Debug.Log("[LOG]: " + childTransform.position.ToString() + " ||| " + childCollider.bounds.extents.ToString() + " ||| " + childTransform.rotation.ToString());

        foreach (MeshCollider hit in hits)
        {
            if (hit.gameObject != gameObject)
            {
                neighbours.Add(hit.gameObject);
                Debug.Log("Colliding with: " + hit.name);
            }
        }*/
        /*}
        else
        {
            Debug.LogError("Object does not have data attached to it!");
        }*/

        cachedIncome = Mathf.RoundToInt(infrastructure / 5);
        cachedMoneyStorage = Mathf.RoundToInt(infrastructure / 2);

        int armyMaintenance = 0;
        int infrastructureMaintenance = Mathf.RoundToInt(infrastructure / 10);
        foreach (Army army in housingArmies) armyMaintenance += army.maintenance;
        cachedMaintenance = infrastructureMaintenance + armyMaintenance;
    }

    public void AddBuilding(Building building)
    {
        constructions.Add(new Construction(building, building.constructionTime));
    }

    public void ConstructBuilding(Construction construction)
    {
        Building building = construction.building;
        buildings.Add(building);
        if (building is Market market) cachedIncome += market.income;
        if (building is Bank bank) cachedMoneyStorage += bank.goldStorage;
        if (building is Barracks barracks) cachedMaxArmy += barracks.maxSoldiers;
        constructions.Remove(construction);
    }

    public void AddArmy(Army army)
    {
        housingArmies.Add(army);
        cachedMaintenance += army.maintenance;
    }

    public void RemoveArmy(Army army)
    {
        housingArmies.Remove(army);
        cachedMaintenance -= army.maintenance;
    }
}

public class Construction
{
    public Building building;
    public int turnsRemaining;

    public Construction(Building building, int turnsRemaining)
    {
        this.building = building;
        this.turnsRemaining = turnsRemaining;
    }
}
