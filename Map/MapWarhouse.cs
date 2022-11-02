using Assets.Code.WareHouseGoods;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Map
{
    class MapWarhouse : MonoBehaviour
    {
        [field: SerializeField] public Rarity Rarity { get; set; }
        [field: SerializeField] public ResourcesGoods Resourse { get; set; } = new ResourcesGoods();
        [field: SerializeField] public int[] Сoordinates { get; set; } = new int[2];
        [field: SerializeField] public List<MapWarhouse> NearestPointsMapWarhouses { get; set; } = new List<MapWarhouse>();
        [field: SerializeField] public WareHouseGoodS WareHouseGoodS {get;set;}

        public void StartInizialization()
        {
            var CurrentCoordinate = GetComponent<RectTransform>().anchoredPosition;
            Сoordinates[0] = (int)CurrentCoordinate.x;
            Сoordinates[1] = (int)CurrentCoordinate.y;

            CheckWarhouseGoodsInAllData();

            if (WareHouseGoodS == null)
            { 
                CreateCardWarhouseGoods();
            }
            else
            {
                WareHouseGoodS.Cordinats[0] = (int)CurrentCoordinate.x;
                WareHouseGoodS.Cordinats[1] = (int)CurrentCoordinate.y;
            }
            PlayerData.instanse.DataMap.AllPointWarhouse.Add(WareHouseGoodS);

        }
        private bool CheckWarhouseGoodsInAllData()
        {
            List<WareHouseGoodS> AllwareHouseGoodS = PlayerData.instanse.DataMap.AllPointWarhouse;
            for (int i = 0; i < AllwareHouseGoodS.Count; i++)
            {
                if (AllwareHouseGoodS[i].Cordinats[0] == Сoordinates[0] && AllwareHouseGoodS[i].Cordinats[1] == Сoordinates[1])
                {
                    WareHouseGoodS = AllwareHouseGoodS[i];
                    return true;
                }
            }
            return false;
        }
        private void CreateCardWarhouseGoods()
        {
            int OneCoordinate = UnityEngine.Random.Range(0, 100);
            int TwoCoordinate = UnityEngine.Random.Range(100, 200);
            WareHouseGoodS = new WareHouseGoodS();
            WareHouseGoodS.Cordinats = Сoordinates;
            WareHouseGoodS.Rarity = Rarity;
            WareHouseGoodS.Icon = IconWarhouse(); 
            WareHouseGoodS.Name = "test";
            WareHouseGoodS.Core = $"Coordinate: {OneCoordinate}:{TwoCoordinate}";
            WareHouseGoodS.Rarity = Rarity;

        }
        private Sprite IconWarhouse()
        {
            IconSpriteToWarhouseGoods DataIconWarhouse  = PlayerData.instanse.iconSpriteToWarhouseGoods;
            Sprite Icon = null;
            switch (Rarity)
            {
                case Rarity.Common:
                    Icon = DataIconWarhouse.CommonIcon;
                    break;
                case Rarity.Rare:
                    Icon = DataIconWarhouse.RareIcon;
                    break;
                case Rarity.Epic:
                    Icon = DataIconWarhouse.EpicIcon;
                    break;
                case Rarity.Legendary:
                    Icon = DataIconWarhouse.LegendaryIcon;
                    break;
                
            }
            return Icon;
        }
    }
}
[Serializable]
public class ResourcesGoods
{
    [field: SerializeField] public int CommonGoods { get; set; }
    [field: SerializeField] public int RareGoods { get; set; }
    [field: SerializeField] public int EpicGoods { get; set; }
    [field: SerializeField] public int LegendaryGoods { get; set; }

}
