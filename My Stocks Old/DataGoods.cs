using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataGoods : MonoBehaviour
{
    [SerializeField] private int CommonGoodsI;
    [SerializeField] private int RareGoodsI;
    [SerializeField] private int EpicGoodsI;
    [SerializeField] private int LegendaryGoodsI;

    [SerializeField] private int TotalCountResourceI;
    [SerializeField] private int CurrentCountResourceI;

    public int CommonGoods
    {
        get { return CommonGoodsI; }
        set { CommonGoodsI = value; }
    }
    public int RareGoods
    {
        get { return RareGoodsI; }
        set { RareGoodsI = value; }
    }
    public int EpicGoods
    {
        get { return EpicGoodsI; }
        set { EpicGoodsI = value; }
    }
    public int LegendaryGoods
    {
        get { return LegendaryGoodsI; }
        set { LegendaryGoodsI = value; }
    }

    public int TotalCountResource
    {
        get { return TotalCountResourceI; }
        set { TotalCountResourceI = value; }
    }
    public int CurrentCountResource
    {
        get { return CurrentCountResourceI; }
        set { CurrentCountResourceI = value; }
    }
}
public interface IDataGoods 
{
    [field: SerializeField] public int CommonGoods { get; set; }
    [field: SerializeField] public int RareGoods { get; set; }
    [Field: SerializeField] public int EpicGoods { get; set; }
    [Field: SerializeField] public int LegendaryGoods { get; set; }

    [Field: SerializeField] public int TotalCountResource { get; set; }
    [Field: SerializeField] public int CurrentCountResource { get; set; }

}
