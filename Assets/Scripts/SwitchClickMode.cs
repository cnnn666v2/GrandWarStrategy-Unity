using UnityEngine;
using GrandWarStrategy.Logic;

public class SwitchClickMode : MonoBehaviour
{
    ClickProvince clickProvince;

    void Start()
    {
        clickProvince = GetComponent<ClickProvince>();
    }

    public void ChangeClickMode(int selectedClickMode)
    {
        clickProvince.clickMode = (ClickMode)selectedClickMode;
    }
}