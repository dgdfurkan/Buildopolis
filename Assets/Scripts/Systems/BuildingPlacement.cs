using UnityEngine;

namespace GunduzDev
{
    public class BuildingPlacement : MonoBehaviour
    {
        private bool _currentlyPlacing, _currentlyBulldozering;

        private BuildingData _currentData;

        private float _indicatorUpdateTime = 0.05f;
        private float _lastUpdateTime;
        private Vector3 currentIndicatorPos;

        [SerializeField] private GameObject placementIndicator, bulldozerIndicator;

        public void BeginNewBuildingPlacement(BuildingData buildingData)
        {
            // Check for money

            _currentlyPlacing = true;
            _currentData = buildingData;
            placementIndicator.SetActive(true);
            bulldozerIndicator.SetActive(false);
        }

        void CancelBuildingPlacement()
        {
            _currentlyPlacing = false;
            placementIndicator.SetActive(false);
        }

        public void ToggleBulldoze()
        {
            _currentlyBulldozering = !_currentlyBulldozering;
            bulldozerIndicator.SetActive(_currentlyBulldozering);
        }

        private void Update()
        {
            if(Input.GetKey(KeyCode.Escape)) CancelBuildingPlacement();

            if (Time.time - _lastUpdateTime > _indicatorUpdateTime)
            {
                _lastUpdateTime = Time.time;

                currentIndicatorPos = Selector.Instance.GetCurTilePosition();

                if (_currentlyPlacing)
                {
                    placementIndicator.transform.position = currentIndicatorPos;
                }else if (_currentlyBulldozering)
                {
                    bulldozerIndicator.transform.position = currentIndicatorPos;
                }
            }

            if (Input.GetMouseButtonDown(0) && _currentlyPlacing)
            {
                PlaceBuilding();
            }else if (Input.GetMouseButtonDown(0) && _currentlyBulldozering)
            {
                Bulldoze();
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        void PlaceBuilding()
        {
            GameObject go = Instantiate(_currentData.Model, currentIndicatorPos, Quaternion.identity);
            City.Instance.OnPlaceBuilding(go.GetComponent<Building>());
            CancelBuildingPlacement();
        }

        void Bulldoze()
        {
            Building obj = City.Instance.Buildings.Find(x => x.transform.position == currentIndicatorPos);
            if (obj != null)
            {
                City.Instance.OnRemoveBuilding(obj);
            }
        }
    }
}
