using Assets.Code.StaticClass;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Map.MainMap
{
    public class InizializationMap : MonoBehaviour
    {
        [SerializeField] private RectTransform _map;
        [SerializeField] private Slider _sliderMap;

        private void OnEnable()
        {
            if (PlayerData.instanse.DataMap.AddWarhousePlayer != 0)  return;
  
            StartCoroutine(ManagerMainMenu.instanse.DebugCoroutine("Add WarhouseGoods"));
            var RandomWarhouse = PlayerData.instanse.DataMap.AllPointWarhouse[UnityEngine.Random.Range(0, PlayerData.instanse.DataMap.AllPointWarhouse.Count)];
            TransferPos.TransferToPointToMap(_map, RandomWarhouse.Cordinats, _sliderMap);
            AddWarhouseToPlayer(RandomWarhouse);
            PlayerData.instanse.DataMap.AddWarhousePlayer++;
        }
        private void AddWarhouseToPlayer(WareHouseGoodS  wareHouseGoodS)
        {
            PlayerData.instanse.instanseSaveCard.ListGarageCardWareHouseGoodS.Add(wareHouseGoodS);
            AddResourse(wareHouseGoodS);
             var a = new List<WareHouseGoodS>();
            a.Add(wareHouseGoodS);
            wareHouseGoodS.IsActive = true;
            Handler.handler.FactoryWarhouseGoodS.CreateActiveAndInGarageCard(a);
            ControllerMap.instanse.OpenMapAndSowMyLand?.Invoke();
            PlayerData.instanse.DataMap.OnePointWarhouseGoods = wareHouseGoodS;
        }
        private void AddResourse(WareHouseGoodS wareHouseGoodS)
        {
            wareHouseGoodS.CommonGoods = 100;
            wareHouseGoodS.RareGoods = 100;
            wareHouseGoodS.EpicGoods = 100;
            wareHouseGoodS.LegendaryGoods = 100;

        }
    }
}