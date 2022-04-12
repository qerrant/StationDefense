using StationDefense.Components;
using StationDefense.Game;
using StationDefense.Patterns;
using StationDefense.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace StationDefense.Building
{
    public class BuildingManager : Singleton<BuildingManager>
    {
        public BuildingMenu buildingMenu;
        public LayerMask selectableLayer;
        public GameObject[] buildList;
        public float clickTime = 0.3f;
        public UnityEvent enemyEvent;
        private float _currentClickTime;
        private MenuState _state = MenuState.Game;

        private void BuildObject(GameObject building, Tile tile)
        {            
            GameObject go_Building = Instantiate(building, tile.transform.position, Quaternion.identity, transform);
            if (go_Building != null)
            {
                go_Building.GetComponent<Building>()?.SetGround(tile);
                tile.Used = true;                
                enemyEvent?.Invoke();
            }
            UpdateMenuState(MenuState.Close);
        }

        private void BuildingUpgrade(Building building)
        {
            building.Upgrade();
            UpdateMenuState(MenuState.Close);
        }

        private GameObject DetectSelectedObject()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, selectableLayer))
            {
                return hit.transform.gameObject;
            }
            return null;
        }

        private void TryBuild(GameObject go)
        {
            Tile tile = go.GetComponent<Tile>();

            if (tile != null && !tile.Used)
            {
                buildingMenu.Clear();
                foreach (GameObject build in buildList)
                {
                    BuildingLevel buildLevel = build.GetComponent<Building>()?.buildings[0];
                    UnityAction action = delegate { BuildObject(build, tile); };
                    buildingMenu.AddButton(buildLevel, action);
                }

                UpdateMenuState(MenuState.Open);
            }
        }

        private void TryUpgrade(GameObject go)
        {
            Building building = go.GetComponent<Building>();

            if (building != null && building.CanUpgrade())
            {
                buildingMenu.Clear();
                BuildingLevel buildLevel = building.GetUpgradeInfo();
                UnityAction action = delegate { BuildingUpgrade(building); };
                buildingMenu.AddButton(buildLevel, action);
                UpdateMenuState(MenuState.Open);
            }
        }

        public static bool IsPointerOverGameObject()
        {
            //check mouse
            if (EventSystem.current.IsPointerOverGameObject())
                return true;

            //check touch
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
                    return true;
            }

            return false;
        }

        void Update()
        {
            if (GameManager.instance.State != GameState.Game) return;
            if (IsPointerOverGameObject()) return;

            if (Input.GetMouseButtonDown(0))
            {
                switch(_state)
                {
                    case MenuState.Menu:
                        UpdateMenuState(MenuState.Close);
                        break;
                    case MenuState.Game:
                        _currentClickTime = 0.0f;
                        break;
                }                
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (_state != MenuState.Game) return;
                if (_currentClickTime > clickTime) return;
                GameObject go = DetectSelectedObject();
                if (go == null) return;
                TryBuild(go);
                TryUpgrade(go);
            }

            if (Input.GetMouseButton(0))
            {
                if (_state == MenuState.Game)
                {
                    _currentClickTime += Time.deltaTime;
                }
            }
        }

        public void UpdateMenuState(MenuState state)
        {
            _state = state;

            switch (state)
            {
                case MenuState.Game:
                    break;
                case MenuState.Menu:
                    break;
                case MenuState.Open:
                    buildingMenu.Show();
                    break;
                case MenuState.Close:
                    buildingMenu.Hide();
                    break;
            }
        }
    }
}
