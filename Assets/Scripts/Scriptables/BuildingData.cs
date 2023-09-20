using System.Collections.Generic;
using UnityEngine;

namespace GunduzDev
{
    [CreateAssetMenu(fileName = "BuildingData", menuName = "Scriptable Objects/BuildingData", order = 0)]
    public class BuildingData : ScriptableObject
    {
        [Header("General Info")]
        public string Name;
        public string Description;
        public Sprite Thumbnail;
        public GameObject Model;

        [Header("Costs")]
        public int BuildCost;
        public int MaintenanceCostPerTurn;

        [Header("Population")]
        public int Population;
        public int Jobs;

        [Header("Resources")]
        public int FoodProduction;
        public int EnergyProduction;

        [Header("Happiness")]
        public int HappinessBonus;

        [Header("Upgrades")]
        public List<BuildingUpgrade> Upgrades;
        
        [System.Serializable]
        public struct BuildingUpgrade
        {
            public string UpgradeName;
            public int UpgradeCost;
            public int PopulationBonus;
            public int ResourceProductionBonus;
        }
    }
}
