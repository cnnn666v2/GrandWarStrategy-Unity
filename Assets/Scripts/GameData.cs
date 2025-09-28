using System;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public List<Color> colors;
    public List<Country> countries;
    public List<Government> governments;
    public List<ProvinceInformation> provincesInformation;
    public List<GameObject> provincesMeshRender;
    public string playingAsTag;
    public int turnCount;
    public List<War> wars = new List<War>();
}
