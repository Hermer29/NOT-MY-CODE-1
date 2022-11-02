
using System;
using UnityEngine.Serialization;

public enum PanelEnum
{
    DriverPanel, TruckPanel, TrailerPanel, MyStocksPanel, Map, Bank, Market, WarhouseGoods, Defolt
}

public enum ActiveSetApp
{
    One, Two, Three, Four, Five
}

public enum SkillPlayer 
{ 
    Luck, Energy, Hunger
}

[Serializable]
public enum Contract : int
{
    ContratsOneHour = 1,
    ContratsThreeHour = 3,
    ContratsSixHour = 6,
    ContratsNineHour = 9,
    ContratsTwelveHour = 12,
} 
