using UnityEngine;
using UnityEngine.UI;

public class WrapperPanel : MonoBehaviour
{
    public Button ButtonMinus;
    public Button ButtonPlus;
    public Text CurrentCountGoods;
    public Text PlayerStateCountGoods;

    [SerializeField] private int TotalCountGoodsI;
    [SerializeField] private int DataCountPlayerGoodsI;
    public int TotalCountGoods
    {
        get { return TotalCountGoodsI; }
        set { TotalCountGoodsI = value; }
    }
    public int DataCountPlayerGoods
    {
        get { return DataCountPlayerGoodsI; }
        set { DataCountPlayerGoodsI = value; }
    }

    public Rarity Rarity
    {
        get { return RarityE; }
        set { RarityE = value; }
    }

    public Rarity RarityE;

}
