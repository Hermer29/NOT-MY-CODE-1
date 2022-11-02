using UnityEngine;
using UnityEngine.UI;

public class LinkCurrentCard : MonoBehaviour
{
    public DataCurrentCard<Driver> _dataCurrentCardDriver = new DataCurrentCard<Driver>();
    public DataCurrentCard<Truck> _dataCurrentCardTruck = new DataCurrentCard<Truck>();
    public DataCurrentCard<WareHouseGoodS> _dataCurrentCardWareHouseGoodS = new DataCurrentCard<WareHouseGoodS>();
    public DataCurrentCard<Trailer> _dataCurrentCardTrailer = new DataCurrentCard<Trailer>();
    public bool isActive;
    public NameProduct NameProduct;
    public int ActiveSetApp;
    public int IndexCard;
    public GameObject CurrentActiveSetupGameObjectText;
    public GameObject CurrentSetupText;
    public Button SetActiveButton;
    public GameObject UniversalButtonForTrailer;
}

[System.Serializable]
public class DataCurrentCard<T>
{
    public T CurrentDataCard;
}
