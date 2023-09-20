using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GunduzDev
{
    public class City : MonoSingleton<City>
    {
        public int Money;
        public int Day;
        public int CurrentPopulation;
        public int MaxPopulation;
        public int CurrentJobs;
        public int MaxJobs;
        public int CurrentFoods;
        public int IncomePerJob;
        public List<Building> Buildings = new List<Building>();
        [SerializeField] private Text statText;

        private void Start()
        {
            StartGame();
        }

        void StartGame()
        {
            UpdateStatText();
        } 
        
        public void OnPlaceBuilding(Building building)
        {
            Money -= building.data.BuildCost;
            MaxPopulation += building.data.Population;
            MaxJobs += building.data.Jobs;
            Buildings.Add(building);
            UpdateStatText();
        }
        
        public void OnRemoveBuilding(Building building)
        {
            MaxPopulation -= building.data.Population;
            MaxJobs -= building.data.Jobs;
            Buildings.Remove(building);
            Destroy(building.gameObject);
            UpdateStatText();
        }

        void UpdateStatText()
        {
            statText.text = string.Format("Day: {0} Money: {1} Pop: {2} / {3} Jobs: {4} / {5} Food: {6}",
                new object[7] {Day, Money, CurrentPopulation, MaxPopulation, CurrentJobs, MaxJobs, CurrentFoods});
        }

        public void EndDay()
        {
            Day++;
            CalculateMoney();
            CalculatePopulation();
            CalculateJob();
            CalculateFood();
            UpdateStatText();
        }

        void CalculateMoney()
        {
            Money += CurrentJobs * IncomePerJob;
            foreach (Building obj in Buildings)
            {
                Money -= obj.data.MaintenanceCostPerTurn;
            }
        }
        
        void CalculatePopulation()
        {
            if (CurrentFoods >= CurrentPopulation && CurrentPopulation < MaxPopulation)
            {
                CurrentFoods -= CurrentPopulation / 4;
                CurrentPopulation = Mathf.Min(CurrentPopulation + (CurrentFoods / 4), MaxPopulation);
            }else if (CurrentFoods < CurrentPopulation)
            {
                CurrentPopulation = CurrentFoods;
            }
        }
        
        void CalculateJob()
        {
            CurrentJobs = Mathf.Min(CurrentPopulation, MaxJobs);
        }
        
        void CalculateFood()
        {
            CurrentFoods = 0;
            foreach (Building obj in Buildings)
            {
                CurrentFoods += obj.data.FoodProduction;
            }
        }
    }
}
