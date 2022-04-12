using StationDefense.Building;
using UnityEngine;
using UnityEngine.Events;

namespace StationDefense.UI
{
    public class BuildingMenu : SimplePage
    {        
        public GameObject menu;
        public GameObject button;
          
        public void AddButton(BuildingLevel buildLevel, UnityAction action)
        {
            GameObject g_Button = Instantiate(button, menu.transform);
            BuildingButton t_Button = g_Button.GetComponent<BuildingButton>();
            t_Button.SetName(buildLevel.fullname);
            t_Button.SetDescription(buildLevel.description);
            t_Button.SetIcon(buildLevel.icon);
            t_Button.SetPrice(buildLevel.cost);
            t_Button.OnButtonClicked.AddListener(action);            
        }

        public void Clear()
        {
            foreach (Transform child in menu.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void ChangeStateToMenu()
        {
            if (!BuildingManager.instanceExists) return;
            BuildingManager.instance.UpdateMenuState(MenuState.Menu);
        }

        public void ChangeStateToGame()
        {
            if (!BuildingManager.instanceExists) return;
            BuildingManager.instance.UpdateMenuState(MenuState.Game);
        }
    }
}
