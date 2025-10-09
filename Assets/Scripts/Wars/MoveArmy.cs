using UnityEngine;

public class MoveArmy : MonoBehaviour
{
    ClickArmy clickArmy;
    RecruitArmy recruitArmy;

    void Start()
    {
        clickArmy = GetComponent<ClickArmy>();
        recruitArmy = GetComponent<RecruitArmy>();
    }

    public void TransportArmy(ProvinceInformation destination)
    {
        //ProvinceInformation destination = recruitArmy.selectedProvince.GetComponent<ProvinceInformation>();

        if (!clickArmy.selectedArmy.GetComponent<Army>().stayingIn.GetComponent<ProvinceData>().neighbours.Contains(destination.GetComponent<GameObject>())) return;
        Vector3 destinationVector = destination.GetComponent<Renderer>().bounds.center;
        clickArmy.selectedArmy.GetComponent<Army>().stayingIn = destination;
        clickArmy.selectedArmy.position = new Vector3(destinationVector.x, clickArmy.selectedArmy.position.y, destinationVector.z);
    }
}
