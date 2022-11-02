using static ConstValueTrailer;
using static ConsValueGoods;

public static class DefineStat
{
    public static (int, int) GeneratorState(Rarity rarity, int Level)
    {
        var TimeCountMass = 0;
        var TimeCount�argo = 0;
        switch (rarity)
        {
            case Rarity.Common:
                for (int u = 0; u < CommonLevel.Length; u++)
                {
                    if (LevelCard[u] == Level)
                    {
                        TimeCountMass += CommonLevel[u];
                        TimeCount�argo += Weigh�CommonTrailer;
                    }
                }
                return (TimeCountMass, TimeCount�argo);
            case Rarity.Rare:
                for (int u = 0; u < LevelCard.Length; u++)
                {
                    if (LevelCard[u] == Level)
                    {
                        TimeCountMass += RareLevel[u];
                        TimeCount�argo += Weigh�RareTrailer;
                    }
                }
                return (TimeCountMass, TimeCount�argo);
            case Rarity.Epic:
                for (int u = 0; u < LevelCard.Length; u++)
                {
                    if (LevelCard[u] == Level)
                    {
                        TimeCountMass += EpicLevel[u];
                        TimeCount�argo += Weigh�EpicTrailer;
                    }
                }
                return (TimeCountMass, TimeCount�argo);
            case Rarity.Legendary:
                for (int u = 0; u < LevelCard.Length; u++)
                {
                    if (LevelCard[u] == Level)
                    {
                        TimeCountMass += LegendaryLevel[u];
                        TimeCount�argo += Weigh�LegendaryTrailer;
                    }
                }
                return (TimeCountMass, TimeCount�argo);
        }
        return (TimeCountMass, TimeCount�argo);
    }
    public static int AllGeneratorResourse(Trailer dataGoods)
    {
        int AllMass = 0;
        AllMass += GeneratorResourse(Rarity.Common, dataGoods.CommonGoods);
        AllMass += GeneratorResourse(Rarity.Rare, dataGoods.RareGoods);
        AllMass += GeneratorResourse(Rarity.Epic, dataGoods.EpicGoods);
        AllMass += GeneratorResourse(Rarity.Legendary, dataGoods.LegendaryGoods);

        return AllMass;
    }
    public static int GeneratorResourse(Rarity rarity, int CountResurs)
    {
        int Mass = 0;
        switch (rarity)
        {
            case Rarity.Common:
                Mass = UnitWeightCommon * CountResurs;
                break;
            case Rarity.Rare:
                Mass = UnitWeightRare * CountResurs;
                break;
            case Rarity.Epic:
                Mass = UnitWeightEpic * CountResurs;
                break;
            case Rarity.Legendary:
                Mass = UnitWeightLegendary * CountResurs;
                break;
        }
        return Mass;
    }

}

