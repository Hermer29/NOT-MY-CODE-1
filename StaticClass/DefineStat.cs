using static ConstValueTrailer;
using static ConsValueGoods;

public static class DefineStat
{
    public static (int, int) GeneratorState(Rarity rarity, int Level)
    {
        var TimeCountMass = 0;
        var TimeCountÑargo = 0;
        switch (rarity)
        {
            case Rarity.Common:
                for (int u = 0; u < CommonLevel.Length; u++)
                {
                    if (LevelCard[u] == Level)
                    {
                        TimeCountMass += CommonLevel[u];
                        TimeCountÑargo += WeighåCommonTrailer;
                    }
                }
                return (TimeCountMass, TimeCountÑargo);
            case Rarity.Rare:
                for (int u = 0; u < LevelCard.Length; u++)
                {
                    if (LevelCard[u] == Level)
                    {
                        TimeCountMass += RareLevel[u];
                        TimeCountÑargo += WeighåRareTrailer;
                    }
                }
                return (TimeCountMass, TimeCountÑargo);
            case Rarity.Epic:
                for (int u = 0; u < LevelCard.Length; u++)
                {
                    if (LevelCard[u] == Level)
                    {
                        TimeCountMass += EpicLevel[u];
                        TimeCountÑargo += WeighåEpicTrailer;
                    }
                }
                return (TimeCountMass, TimeCountÑargo);
            case Rarity.Legendary:
                for (int u = 0; u < LevelCard.Length; u++)
                {
                    if (LevelCard[u] == Level)
                    {
                        TimeCountMass += LegendaryLevel[u];
                        TimeCountÑargo += WeighåLegendaryTrailer;
                    }
                }
                return (TimeCountMass, TimeCountÑargo);
        }
        return (TimeCountMass, TimeCountÑargo);
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

