using Assets.Code.StaticClass;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Map
{
    public class UIControllerMap : MonoBehaviour 
    {
        [field: SerializeField] private Button _resetPoint { get; set; }
        [field: SerializeField] private Button _currentWarhouseGoods { get; set; }
        [field: SerializeField] private Button _showMyTruck { get; set; }
        [field: SerializeField] private Button _showMyWarhouse { get; set; }
        [field: SerializeField] private EventButtonController _eventButtonController { get; set; }
        [field: SerializeField] private ControllerMap _controllerMap { get; set; }
        

        private void Awake()
        {
            _resetPoint.onClick.AddListener(()=> CurrentManagerPoint.EventResetPoint.Invoke(null, null));
            _resetPoint.onClick.AddListener(() => ControllerMap.EventResetPoint?.Invoke());
            _currentWarhouseGoods.onClick.AddListener(() => _eventButtonController.ActivePanel(EventPanel.EventOnEnableWarhouseGoodsPanel));
            _showMyTruck.onClick.AddListener(() => _controllerMap.ShowMyTruckToMapPanel());
            _showMyWarhouse.onClick.AddListener(() => _controllerMap.ShowWarhouseGoodsToMapPanel()); 
            
        }
        private void OnDestroy()
        {
            _resetPoint.onClick.RemoveAllListeners();
            _currentWarhouseGoods.onClick.RemoveAllListeners();
            _showMyTruck.onClick.RemoveAllListeners();
            _showMyWarhouse.onClick.RemoveAllListeners();
        }
    }
}
