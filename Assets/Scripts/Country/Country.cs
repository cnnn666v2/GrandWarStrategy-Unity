using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Country
{
    public string countryName;
    public string countryTag;
    public Color color;
    public Government government;
    public List<int> ownedProvinces;
    public List<int> occupiedProvinces;
    public int money;
    public int maxMoney;
    public int population;
    public int manpower;
    public float conscriptionModifier = 0.5f;
}
