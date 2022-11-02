using Assets.Code.StaticClass;
using UnityEngine;

namespace Assets.Code.My_Stocks
{
    public class ControllerMyStoksPanel : MonoBehaviour
    {
        [field: SerializeField] private EventButtonController eventButtonController { get; set; } // Прокинуть руками.
        [field: SerializeField] private GameObject CurrentMarketPanelWarhouseGoods { get; set; } // Прокинуть руками.

        private HeaderMyStoks _headerMyStoks { get; set; }
        private UiDataMyStoksPanel _uiDataMyStoksPanel1 { get; set;}
        private void Awake()
        {
            _headerMyStoks = GetComponent<HeaderMyStoks>();
            _uiDataMyStoksPanel1 = GetComponent<UiDataMyStoksPanel>();
            Subscriptions();
        }
        private void Subscriptions()
        {
            foreach (var item in _uiDataMyStoksPanel1.BuyOnMarket)
            {
                item.onClick.AddListener(() => eventButtonController.ActivePanel(EventPanel.EventOnEnableMarketPanel));
            }
            _uiDataMyStoksPanel1.BuyOnWarhouseGoods.onClick.AddListener(() => eventButtonController.ActivePanel(EventPanel.EventOnEnableWarhouseGoodsPanel));
            _uiDataMyStoksPanel1.BuyOnWarhouseGoods.onClick.AddListener(() => CurrentMarketPanelWarhouseGoods.SetActive(true)); 
        }
        private void OnDestroy()
        {
            foreach (var item in _uiDataMyStoksPanel1.BuyOnMarket)
            {
                item.onClick.RemoveAllListeners();
            }
            _uiDataMyStoksPanel1.BuyOnWarhouseGoods.onClick.RemoveAllListeners();
        }
        private void OnEnable()
        {
            StartUpdateDisplay();
        }
        private void StartUpdateDisplay()
        {
            _headerMyStoks.UpdateDisplay(_uiDataMyStoksPanel1, PlayerData.instanse.instanseSavePlayerState);
        }
    }
}