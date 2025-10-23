using System.Collections.Generic;
using UnityEngine;
using GrandWarStrategy.Province;
using GrandWarStrategy.Division;

namespace GrandWarStrategy.Logic
{
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
        public List<TrainingDivisions> trainingDivisions = new List<TrainingDivisions>();
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

    public class TrainingDivisions
    {
        public int amount;
        public int cost;
        public int maintenance;
        public int trainingTime;
        public Transform trainingDestination;

        public TrainingDivisions(int amount, int cost, int maintenance, int trainingTime, Transform trainingDestination)
        {
            this.amount = amount;
            this.cost = cost;
            this.maintenance = maintenance;
            this.trainingTime = trainingTime;
            this.trainingDestination = trainingDestination;
        }
    }
}