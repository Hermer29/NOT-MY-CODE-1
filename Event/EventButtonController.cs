using Assets.Code;
using Assets.Code.MainMenu;
using Assets.Code.Map;
using Assets.Code.StaticClass;
using System;
using System.Collections;
using UnityEngine;

public class EventButtonController : MonoBehaviour
{
    public static Action<GameObject[]> EventFalsePanel { get; set; }
    public static Action<GameObject> EventTruePanelMainMenu { get; set; }
    public static Action<GameObject> EventTruePanelMap { get; set; }
    public static Action<GameObject> EventTruePanelWareHouseGoods { get; set; }
    public static Action<GameObject> EventTruePanelMarket { get; set; }
    public static Action<GameObject> EventTruePanelStacking;
    public static Action<GameObject> EventTruePanelDriver;
    public static Action<GameObject> EventTruePanelTruck;
    public static Action<GameObject> EventTruePanelTralier;
    public static Action<GameObject> EventTruePanelMyStocks;

    public static Action EventOpenMap;

    public static Action<GameObject> EventInfoPanel;
    public static Action<GameObject> EventSetActive;

    [SerializeField] private Canvas _canvas; 
    [SerializeField] public DataUIGamePanel _dataUIGamePanel;

    public void ActivePanel(string EnumActivationPaneL )
    {
        StartCoroutine(SwitchEventPanel(EnumActivationPaneL));
    }
    private IEnumerator SwitchEventPanel(string EnumActivationPaneL)
    {
        PanelEnum panelEnum = PanelEnum.Defolt;
        yield return new WaitForSeconds(0.01f);
        Debug.Log($"[{nameof(EventButtonController)}] Received event (name: {EnumActivationPaneL}) ");
        EventFalsePanel?.Invoke(_dataUIGamePanel.AllPanel);
        switch (EnumActivationPaneL)
        {
            case EventPanel.EventOnEnableMainPanel:
                EventTruePanelMainMenu?.Invoke(_dataUIGamePanel.PanelMainMenu);
                CurrentActivePanel.EventRemoveCurrentPanel?.Invoke(null, true);
                CurrentActivePanel.EventOnSwitchingButtonBack?.Invoke(false);
                MainHeadlerSetapp.EventUpdateHeaderMainMenu?.Invoke();
                break;
            case EventPanel.EventOnEnableMapPanel:
                EventTruePanelMap?.Invoke(_dataUIGamePanel.PanelMap);
                panelEnum = PanelEnum.Map;
                ControllerMap.EventActionPanel?.Invoke(false);
                break;
            case EventPanel.EventOnEnableMarketPanel:
                EventTruePanelMarket?.Invoke(_dataUIGamePanel.PanelMarket);
                panelEnum = PanelEnum.Market;
                break;
            case EventPanel.EventOnEnableStakingPanel:
                EventTruePanelStacking?.Invoke(_dataUIGamePanel.PanelStacking);
                panelEnum = PanelEnum.Bank;
                break;
            case EventPanel.EventOnEnableDriverPanel:
                EventTruePanelDriver?.Invoke(_dataUIGamePanel.PanelDriver);
                panelEnum = PanelEnum.DriverPanel;
                break;
            case EventPanel.EventOnEnableTruckPanel:
                EventTruePanelTruck?.Invoke(_dataUIGamePanel.PanelTruck);
                panelEnum = PanelEnum.TruckPanel;
                break;
            case EventPanel.EventOnEnableTrailerPanel:
                EventTruePanelTralier?.Invoke(_dataUIGamePanel.PanelTralier);
                panelEnum = PanelEnum.TrailerPanel;
                break;
            case EventPanel.EventOnEnableMyStocksPanel:
                EventTruePanelMyStocks?.Invoke(_dataUIGamePanel.PanelMyStocks);
                panelEnum = PanelEnum.MyStocksPanel;
                break;
            case EventPanel.EventOnEnableWarhouseGoodsPanel:
                EventTruePanelWareHouseGoods?.Invoke(_dataUIGamePanel.PanelMyLand);
                panelEnum = PanelEnum.WarhouseGoods;
                break;
            default:
                Debug.LogError("NULL");
                break;
        }
        CurrentActiveSpritePanel.EventTransferActivatorPanel?.Invoke(panelEnum);
        CurrentActivePanel.EventAddCurrentPanel?.Invoke(EnumActivationPaneL);
    }
}


