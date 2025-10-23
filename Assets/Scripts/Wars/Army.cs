using UnityEngine;
using GrandWarStrategy.Province;

namespace GrandWarStrategy.Division
{
    public class Army : MonoBehaviour
    {
        public int soldiers;
        public ProvinceData stayingIn;
        public string owner;
        public int maintenance;
    }

    public enum ArmyType { infantry, tank, artillery }
}