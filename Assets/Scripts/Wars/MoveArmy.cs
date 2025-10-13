using UnityEngine;

public class MoveArmy : MonoBehaviour
{
    ClickProvince clickProvince;
    GameData gameData;
    WarManager warManager;
    //RecruitArmy recruitArmy;

    void Start()
    {
        clickProvince = GetComponent<ClickProvince>();
        gameData = GetComponent<GameData>();
        warManager = GetComponent<WarManager>();
        //recruitArmy = GetComponent<RecruitArmy>();
    }

    public void TransportArmy()
    {
        //ProvinceInformation destination = recruitArmy.selectedProvince.GetComponent<ProvinceInformation>();
        ProvinceData destination = clickProvince.province;
        Transform armyPosition = clickProvince.selectedArmy.GetComponent<Transform>();
        Army army = clickProvince.selectedArmy.GetComponent<Army>();

        if (destination.owner != gameData.playingAsTag && !warManager.AreAtWar(gameData.playingAsTag, destination.owner))
        {
            Debug.Log("not owner or not at war");
            return;
        }

        if (!army.stayingIn.neighbours.Contains(destination)) return;

        int travelTime = 2;
        gameData.movingDivisions.Add(new MovingDivisions(army, army.stayingIn, destination, travelTime));
        
        /*Vector3 destinationVector = destination.GetComponent<Renderer>().bounds.center;
        clickProvince.selectedArmy.GetComponent<Army>().stayingIn = destination;
        armyPosition.position = new Vector3(destinationVector.x, armyPosition.position.y, destinationVector.z);*/
    }
}
