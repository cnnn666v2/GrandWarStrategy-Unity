using UnityEngine;

public class MoveArmy : MonoBehaviour
{
    ClickProvince clickProvince;
    //RecruitArmy recruitArmy;

    void Start()
    {
        clickProvince = GetComponent<ClickProvince>();
        //recruitArmy = GetComponent<RecruitArmy>();
    }

    public void TransportArmy()
    {
        //ProvinceInformation destination = recruitArmy.selectedProvince.GetComponent<ProvinceInformation>();
        ProvinceData destination = clickProvince.province;
        Transform armyPosition = clickProvince.selectedArmy.GetComponent<Transform>();
        Army army = clickProvince.selectedArmy.GetComponent<Army>();

        // Fix: if is not checking properly
        if (!army.stayingIn.neighbours.Contains(destination)) { Debug.Log("Hi"); return; }
        Vector3 destinationVector = destination.GetComponent<Renderer>().bounds.center;
        clickProvince.selectedArmy.GetComponent<Army>().stayingIn = destination;
        armyPosition.position = new Vector3(destinationVector.x, armyPosition.position.y, destinationVector.z);
    }
}
