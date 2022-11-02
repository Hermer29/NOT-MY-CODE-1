using Assets.Code.WareHouseGoods;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DataWareHouseGoodS", menuName = "ScriptableObject/DataWareHouseGoodS")]
public class WareHouseGoodS : ScriptableObject
{
    public Sprite Icon;
    public int indexCard; 
    public Rarity Rarity;
    public string Name = "Test";
    public string Core = "null";  

    public bool IsActive;

    public int[] Cordinats;

    [field: SerializeField]public int CommonGoods { get; set; }
    [field: SerializeField]public int RareGoods { get; set; }
    [field: SerializeField]public int EpicGoods { get; set; }
    [field: SerializeField]public int LegendaryGoods { get; set; }

    public List<DataSubscription> dataSubscriptions = new List<DataSubscription>();
}