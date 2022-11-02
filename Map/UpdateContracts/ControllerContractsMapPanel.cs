using Assets.Code.My_Stocks;
using UnityEngine;

namespace Assets.Code.Map.UpdateContracts
{
    public class ControllerContractsMapPanel : MonoBehaviour
    {
        [SerializeField] private HeaderMyStoks _headerMyStoks;
        [SerializeField] private UiDataMyStoksPanel _uiDataMyStoksPanel;
        private void OnEnable()
        {
            _headerMyStoks.UpdateContracts(_uiDataMyStoksPanel, PlayerData.instanse.instanseSavePlayerState);
        }
    }
}