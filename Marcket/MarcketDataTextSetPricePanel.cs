using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MarcketDataTextSetPricePanel
{
    public Text Foods;
    public Text Rests;
    public Text Parts;
    public Text Fuel;
    public Text CommonGoods;
    public Text RareGoods;
    public Text EpicGoods;
    public Text LegendaryGoods;
    public Text Stuff;

    public Text OneClockContracts;
    public Text ThreeClockContracts;
    public Text SixClockContracts;
    public Text NineClockContracts;
    public Text TwelveClockContracts;

    [field: SerializeField] public GameObject SuppliesPanel { get; set; }
    [field: SerializeField] public GameObject GoodsPanel { get; set; }
    [field: SerializeField] public GameObject StuffPanel { get; set; }
    [field: SerializeField] public GameObject AllPrice { get; set; }

}

[System.Serializable]
public class CurrentPriceUIToMarcket: MarcketDataTextSetPricePanel
{
    public Text AllCountPrice;
}
public abstract class DataPricePurchases 
{
    public int Foods = 1;
    public int Rests = 1;
    public int Parts = 1;
    public int Fuel = 1;
    public int CommonGoods = 1;
    public int RareGoods = 1;
    public int EpicGoods = 1;
    public int LegendaryGoods = 1;
    public int Stuff = 1;

    public int OneClockContracts = 1;
    public int ThreeClockContracts = 3;
    public int SixClockContracts = 6;
    public int NineClockContracts = 9;
    public int TwelveClockContracts = 12;

    public int AllValue;
}

public class ConstCountPricePurchases : DataPricePurchases { }
public class CurrentPricePurchases : DataPricePurchases { }
