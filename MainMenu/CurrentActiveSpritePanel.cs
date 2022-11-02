using UnityEngine;
using System;
using UnityEngine.UI;

namespace Assets.Code.MainMenu
{
    public class CurrentActiveSpritePanel : MonoBehaviour 
    {
        [field: SerializeField] private DataCurrentActivePanel _dataCurrentActivePanel { get; set; } = new DataCurrentActivePanel();
        public static Action<PanelEnum> EventTransferActivatorPanel { get; set; }
        public static Action EventDisablePanel;
        private void Awake()
        {
            EventTransferActivatorPanel += CurrentControlerActiveImagePanel;
            EventDisablePanel += OnDisableAllPanel;
        }
        private void OnDestroy()
        {

            EventTransferActivatorPanel -= CurrentControlerActiveImagePanel;
            EventDisablePanel -= OnDisableAllPanel;
        }
        private void CurrentControlerActiveImagePanel(PanelEnum panelEnum)
        {
            OnDisableAllPanel();
            switch (panelEnum)
            {
                case PanelEnum.DriverPanel:
                    _dataCurrentActivePanel.DriverPanelToScene.sprite = _dataCurrentActivePanel.SpriteOnEnableDriverPanel;
                    break;
                case PanelEnum.TruckPanel:
                    _dataCurrentActivePanel.TruckPanelToScene.sprite = _dataCurrentActivePanel.SpriteOnEnableTruckPanel;                  
                    break;
                case PanelEnum.TrailerPanel:
                    _dataCurrentActivePanel.TrilerPanelToScene.sprite = _dataCurrentActivePanel.SpriteOnEnableTrailerPanel;
                    break;
                case PanelEnum.MyStocksPanel:
                    _dataCurrentActivePanel.MyStoksPanelToScene.sprite = _dataCurrentActivePanel.SpriteOnEnableMyStocksPanel;
                    break;
                case PanelEnum.Map:
                    _dataCurrentActivePanel.MapPanelToScene.sprite = _dataCurrentActivePanel.SpriteOnEnableMapPanel;
                    break;
                case PanelEnum.Bank:
                    _dataCurrentActivePanel.BankPanelToScene.sprite = _dataCurrentActivePanel.SpriteOnEnableBankPanel;
                    break;
                case PanelEnum.Market:
                    _dataCurrentActivePanel.MarketPanelToScene.sprite = _dataCurrentActivePanel.SpriteOnEnableMarketPanel;
                    break;
                case PanelEnum.WarhouseGoods:
                    _dataCurrentActivePanel.WarhouseGoodsPanelToScene.sprite = _dataCurrentActivePanel.SpriteOnEnableWarhouseGoodsPanel;
                    break;
            }

        }
        private void OnDisableAllPanel()
        {
            _dataCurrentActivePanel.DriverPanelToScene.sprite = _dataCurrentActivePanel.SpriteOnDisableDriverPanel;
            _dataCurrentActivePanel.TruckPanelToScene.sprite = _dataCurrentActivePanel.SpriteOnDisableTruckPanel;
            _dataCurrentActivePanel.TrilerPanelToScene.sprite = _dataCurrentActivePanel.SpriteOnDisableTrailerPanel;
            _dataCurrentActivePanel.MyStoksPanelToScene.sprite = _dataCurrentActivePanel.SpriteOnDisableMyStocksPanel;

            _dataCurrentActivePanel.MapPanelToScene.sprite = _dataCurrentActivePanel.SpriteOnDisableMapPanel;
            _dataCurrentActivePanel.MarketPanelToScene.sprite = _dataCurrentActivePanel.SpriteOnDisableMarketPanel;
            _dataCurrentActivePanel.BankPanelToScene.sprite = _dataCurrentActivePanel.SpriteOnDisableBankPanel;
            _dataCurrentActivePanel.WarhouseGoodsPanelToScene.sprite = _dataCurrentActivePanel.SpriteOnDisableWarhouseGoodsPanel;

        }
    }
}
[Serializable]
public class DataCurrentActivePanel 
{
    //Sprite
    [field: SerializeField] public Sprite SpriteOnEnableDriverPanel { get; private set; }
    [field: SerializeField] public Sprite SpriteOnDisableDriverPanel { get; private set; }
    [field: SerializeField] public Sprite SpriteOnEnableTruckPanel { get; private set; }
    [field: SerializeField] public Sprite SpriteOnDisableTruckPanel { get; private set; }
    [field: SerializeField] public Sprite SpriteOnEnableTrailerPanel { get; private set; }
    [field: SerializeField] public Sprite SpriteOnDisableTrailerPanel { get; private set; }
    [field: SerializeField] public Sprite SpriteOnEnableMyStocksPanel { get; private set; }
    [field: SerializeField] public Sprite SpriteOnDisableMyStocksPanel { get; private set; }


    [field: SerializeField] public Sprite SpriteOnEnableMapPanel { get; private set; }
    [field: SerializeField] public Sprite SpriteOnDisableMapPanel { get; private set; }
    [field: SerializeField] public Sprite SpriteOnEnableBankPanel { get; private set; }
    [field: SerializeField] public Sprite SpriteOnDisableBankPanel { get; private set; }
    [field: SerializeField] public Sprite SpriteOnEnableMarketPanel { get; private set; }
    [field: SerializeField] public Sprite SpriteOnDisableMarketPanel { get; private set; }
    [field: SerializeField] public Sprite SpriteOnEnableWarhouseGoodsPanel { get; private set; }
    [field: SerializeField] public Sprite SpriteOnDisableWarhouseGoodsPanel { get; private set; }

    //Image
    [field: SerializeField] public Image DriverPanelToScene  { get; private set; }
    [field: SerializeField] public Image TruckPanelToScene  { get; private set; }
    [field: SerializeField] public Image TrilerPanelToScene  { get; private set; }
    [field: SerializeField] public Image MyStoksPanelToScene  { get; private set; }

    [field: SerializeField] public Image MapPanelToScene { get; private set; }
    [field: SerializeField] public Image BankPanelToScene { get; private set; }
    [field: SerializeField] public Image MarketPanelToScene { get; private set; }
    [field: SerializeField] public Image WarhouseGoodsPanelToScene { get; private set; }
}
