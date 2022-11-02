
public class ConstValueTrailer
{
    public static readonly int[] LevelCard = new int[3] { 1, 2, 3 };
    public static readonly int[] CommonLevel = new int[3] { 1, 3, 9 };
    public const int WeighеCommonTrailer = 1000;
    public const int CommonTrailerLevel1 = 1;
    public const int CommonTrailerLevel2 = 3;
    public const int CommonTrailerLevel3 = 9;

    public static readonly int[] RareLevel = new int[3] { 1, 3, 9 };
    public const int WeighеRareTrailer = 1200;
    public const int RareTrailerLevel1 = 1;
    public const int RareTrailerLevel2 = 3;
    public const int RareTrailerLevel3 = 9;

    public static readonly int[] EpicLevel = new int[3] { 1, 3, 9 };
    public const int WeighеEpicTrailer = 1600;
    public const int EpicTrailerLevel1 = 1;
    public const int EpicTrailerLevel2 = 3;
    public const int EpicTrailerLevel3 = 9;

    public static readonly int[] LegendaryLevel = new int[3] { 1, 3, 9 };
    public const int WeighеLegendaryTrailer = 2000;
    public const int LegendaryTrailerLevel1 = 1;
    public const int LegendaryTrailerLevel2 = 3;
    public const int LegendaryTrailerLevel3 = 9;
}
public class ConsValueGoods
{
    public const int StockGoods = 200;


    //Вес товара
    public const int UnitWeightCommon = 100;
    public const int UnitWeightRare = 150;
    public const int UnitWeightEpic = 200;
    public const int UnitWeightLegendary = 300;


    //Вместимость склада
    public const int MaxCommonGoods = 250;
    public const int MaxRareGoods = 500;
    public const int MaxEpicGoods = 800;
    public const int MaxLegendaryGoods = 1000;

    //Пока фиксированная цена
    public const int PriceCommonGoods = 1;
    public const int PriceRareGoods = 3;
    public const int PriceEpicGoods = 6;
    public const int PriceLegendaryGoods = 10;
}
