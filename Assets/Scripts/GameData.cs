using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public List<Color> colors;
    public List<Country> countries;
    public List<Government> governments;
    public List<ProvinceData> provincesInformation;
    public List<GameObject> provincesMeshRender;
    public string playingAsTag;
    public int turnCount;
    public List<War> wars = new List<War>();
    public List<MovingDivisions> movingDivisions = new List<MovingDivisions>();
}

public class MovingDivisions
{
    public Army army;
    public ProvinceData stayingIn, destination;
    public int travelTime;

    public MovingDivisions(Army army, ProvinceData stayingIn, ProvinceData destination, int travelTime)
    {
        this.army = army;
        this.stayingIn = stayingIn;
        this.destination = destination;
        this.travelTime = travelTime;
    }
}
