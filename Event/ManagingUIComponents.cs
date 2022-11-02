using Assets.Code.MainMenu;
using UnityEngine;

public class ManagingUIComponents : MonoBehaviour
{
    private void HidesAllPanel(GameObject[] AllPanel) 
    {
        for (int i = 0; i < AllPanel.Length; i++)
        {
            AllPanel[i].SetActive(false);
        }
        CurrentActiveSpritePanel.EventDisablePanel?.Invoke();

    }
    private void OnActivePAnel(GameObject onActivePanel)
    {
        onActivePanel.SetActive(true);
    }  
    private void Start()
    {
        EventButtonController.EventFalsePanel += HidesAllPanel;
        EventButtonController.EventTruePanelMainMenu += OnActivePAnel;
        EventButtonController.EventTruePanelMap += OnActivePAnel;
        EventButtonController.EventTruePanelMarket += OnActivePAnel;
        EventButtonController.EventTruePanelStacking += OnActivePAnel;
        EventButtonController.EventTruePanelDriver += OnActivePAnel;
        EventButtonController.EventTruePanelTruck += OnActivePAnel;
        EventButtonController.EventTruePanelTralier += OnActivePAnel;
        EventButtonController.EventTruePanelMyStocks += OnActivePAnel;
        EventButtonController.EventTruePanelWareHouseGoods += OnActivePAnel;

    }

    private void OnDestroy()
    {
        EventButtonController.EventFalsePanel -= HidesAllPanel;
        EventButtonController.EventTruePanelMainMenu -= OnActivePAnel;
        EventButtonController.EventTruePanelMap -= OnActivePAnel;
        EventButtonController.EventTruePanelMarket -= OnActivePAnel;
        EventButtonController.EventTruePanelStacking -= OnActivePAnel;
        EventButtonController.EventTruePanelDriver -= OnActivePAnel;
        EventButtonController.EventTruePanelTruck -= OnActivePAnel;
        EventButtonController.EventTruePanelTralier -= OnActivePAnel;
        EventButtonController.EventTruePanelMyStocks -= OnActivePAnel;
        EventButtonController.EventTruePanelWareHouseGoods -= OnActivePAnel;
    }
}