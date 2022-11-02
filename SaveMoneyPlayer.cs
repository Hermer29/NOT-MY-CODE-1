using System.Collections.Generic;
using Assets.Code.Ui;
using UnityEngine;
[System.Serializable]
public class SaveMoneyPlayer
{
    [field: SerializeField]
    private float DriveM;
    public float Drive
    {
        get { return DriveM; }
        set 
        {
            DriveM = value;
            UpdateMoneyToDisplay.EventUpdateMoney?.Invoke(DriveM);
        }
    }

}
[System.Serializable]
public class SavePlayerState
{
    public int Food = 99;
    public int Parts = 99;
    public int Fuel =99;
    public int Rest = 99;

    public int StuffMoney = 0;

    public int CommonGoods = 100;
    public int RareGoods = 100;
    public int EpicGoods = 100;
    public int legendaryGoods = 100;

    //Data Contrats
    public int ContratsOneHour = 100;
    public int ContratsThreeHour = 100;
    public int ContratsSixHour = 100;
    public int ContratsNineHour = 100;
    public int ContratsTwelveHour = 100;

}
[System.Serializable]
public class CardSave
{
    //Driver

    public List<Driver> ListActiveCardDriver;
    public List<Driver> ListInWalletCardDriver;
    public List<Driver> ListLoungeCardDriver;

    //Truck
    public List<Truck> ListActiveCardTruck;
    public List<Truck> ListInWalletCardTruck;
    public List<Truck> ListGarageCardTruck;

    //Trailer
    public List<Trailer> ListCurrentSaveSetAcrive;
    public List<Trailer> ListActiveCardTrailer;
    public List<Trailer> ListInWalletCardTrailer;
    public List<Trailer> ListGarageCardTrailer;

    //WareHouseGoodS

    public List<WareHouseGoodS> ListActiveCardWareHouseGoodS;
    public List<WareHouseGoodS> ListInWalletCardWareHouseGoodS;
    public List<WareHouseGoodS> ListGarageCardWareHouseGoodS;

    //DataSetApp
    public List<SetAppCard> SetAppCards;
    public int CurrentSetupPlayer { get; set; } = 1;
    [field: SerializeField] public int AllSetup { get; set; } = 1;
    [field: SerializeField] public List<int> CurrentTravelCard { get; set; } = new List<int>(5);

    public List<int> ActiveTravel;
}
[System.Serializable]
public class SaveAddedPlayerPayment
{
    public List<AddedavePlayerStock> AddedavePlayertock = new List<AddedavePlayerStock>();
}

//“екущий класс будет содержать все точки на карте.
[System.Serializable]
public class DataMap
{
    [field: SerializeField] public List<WareHouseGoodS> AllPointWarhouse { get; set; } = new List<WareHouseGoodS>();
    public int AddWarhousePlayer { get; set; } = 0;
    public WareHouseGoodS OnePointWarhouseGoods { get; set; }
    
}