using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum CurrentButtonMarcket
{
    PlayerResourses = 1,
    GoodsResourses,
    Stuff,

    BuyButton = 10,
    GetPriceButtom = 20,
    Null
}
public class ControllerPurchasesMarcketPanel : MonoBehaviour
{
    [SerializeField] private MarcketDataTextSetPricePanel _marcketDataTextSetPricePanel = new MarcketDataTextSetPricePanel(); //Цена товара
    [SerializeField] public ConstCountPricePurchases _countPricePurchases { get; private set; } = new ConstCountPricePurchases();
    [SerializeField] private CurrentPriceUIToMarcket _currentPriceUIToMarcket = new CurrentPriceUIToMarcket(); //Текущие цена выбранных товаров
    private MarcketEvent _marcketEvent = new MarcketEvent();
    public CurrentPricePurchases _currentPricePurchases { get; set; } = new CurrentPricePurchases();
    private bool isStartCorutine { get; set; }

    [field: SerializeField] private GameObject BuyPanel { get; set; }
    [field: SerializeField] private GameObject Buy { get; set; }
    [field: SerializeField] private int[] RandomPrice { get; set; } = new int[2];
    [field: SerializeField] private CurrentButtonMarcket  currentButtonMarcket { get; set; }

    private void Awake()
    {
        _marcketEvent = GetComponent<MarcketEvent>();
        Buy.GetComponentInChildren<Button>().onClick.AddListener(() => BuyGoods());
    }
    private void OnDestroy()
    {
        Buy.GetComponentInChildren<Button>().onClick.RemoveListener(() => BuyGoods());
    }
    private IEnumerator TimeCorutine()
    {
        if (_currentPricePurchases.Foods == 0)
        {
            UpdateNewPrice();
            isStartCorutine = false;
        }
        isStartCorutine = true;
        yield return new WaitForSeconds(600f);
        UpdateNewPrice();
        isStartCorutine = false;
    }
    private void UpdateNewPrice()
    {
        //for (int i = 0; i < _countPricePurchases.TestPlay.Length; i++)
        //{
        //    _countPricePurchases.TestPlay[i] = Random.Range(RandomPrice[0], RandomPrice[1]);
        //}
    }
    private void FixedUpdate()
    {
        if (!isStartCorutine)
        {
            StartCoroutine(TimeCorutine());
        }
    }
    public void CostCalculation() //Главный метод записи 
    {
        _currentPricePurchases.AllValue = 0;
        _currentPricePurchases.Foods = _countPricePurchases.Foods * _marcketEvent.CurrentCountPurchasesGoods.CurrentValueFood;
        _currentPricePurchases.Rests = _countPricePurchases.Rests * _marcketEvent.CurrentCountPurchasesGoods.CurrentValueRest;
        _currentPricePurchases.Parts = _countPricePurchases.Parts * _marcketEvent.CurrentCountPurchasesGoods.CurrentValueParts;
        _currentPricePurchases.Fuel = _countPricePurchases.Fuel * _marcketEvent.CurrentCountPurchasesGoods.CurrentValueFuel;
        _currentPricePurchases.CommonGoods = _countPricePurchases.CommonGoods * _marcketEvent.CurrentCountPurchasesGoods.CurrentValueCommonGoods;
        _currentPricePurchases.RareGoods = _countPricePurchases.RareGoods * _marcketEvent.CurrentCountPurchasesGoods.CurrentValueRareGoods;
        _currentPricePurchases.EpicGoods = _countPricePurchases.EpicGoods * _marcketEvent.CurrentCountPurchasesGoods.CurrentValueEpicGoods;
        _currentPricePurchases.LegendaryGoods = _countPricePurchases.LegendaryGoods * _marcketEvent.CurrentCountPurchasesGoods.CurrentValueLegendaryGoods;
        _currentPricePurchases.Stuff = _countPricePurchases.Stuff * _marcketEvent.CurrentCountPurchasesGoods.CurrentValueStuff;
        _currentPricePurchases.OneClockContracts = _countPricePurchases.OneClockContracts * _marcketEvent.CurrentCountPurchasesGoods.CurrentValueOneClockContracts;
        _currentPricePurchases.ThreeClockContracts = _countPricePurchases.ThreeClockContracts * _marcketEvent.CurrentCountPurchasesGoods.CurrentValueThreeClockContracts;
        _currentPricePurchases.SixClockContracts = _countPricePurchases.SixClockContracts * _marcketEvent.CurrentCountPurchasesGoods.CurrentValueSixClockContracts;
        _currentPricePurchases.NineClockContracts = _countPricePurchases.NineClockContracts * _marcketEvent.CurrentCountPurchasesGoods.CurrentValueNineClockContracts;
        _currentPricePurchases.TwelveClockContracts = _countPricePurchases.TwelveClockContracts * _marcketEvent.CurrentCountPurchasesGoods.CurrentValueTwelveClockContracts;

    }
    /* RandomValue
    public void CostCalculation() //Главный метод записи 
    {
        _currentPricePurchases.AllValue = 0;
        _currentPricePurchases.Foods = _countPricePurchases.Foods * _marcketEvent.CurrentCountPurchasesGoods.CurrentValueFood;
        _currentPricePurchases.Rests = _countPricePurchases.Rests * _marcketEvent.CurrentCountPurchasesGoods.CurrentValueRest;
        _currentPricePurchases.Parts = _countPricePurchases.Parts * _marcketEvent.CurrentCountPurchasesGoods.CurrentValueParts;
        _currentPricePurchases.Fuel = _countPricePurchases.Fuel * _marcketEvent.CurrentCountPurchasesGoods.CurrentValueFuel;
        _currentPricePurchases.CommonGoods = _countPricePurchases.CommonGoods * _marcketEvent.CurrentCountPurchasesGoods.CurrentValueCommonGoods;
        _currentPricePurchases.RareGoods = _countPricePurchases.RareGoods * _marcketEvent.CurrentCountPurchasesGoods.CurrentValueRareGoods;
        _currentPricePurchases.EpicGoods = _countPricePurchases.EpicGoods * _marcketEvent.CurrentCountPurchasesGoods.CurrentValueEpicGoods;
        _currentPricePurchases.LegendaryGoods = _countPricePurchases.LegendaryGoods * _marcketEvent.CurrentCountPurchasesGoods.CurrentValueLegendaryGoods;
        _currentPricePurchases.Stuff = _countPricePurchases.Stuff * _marcketEvent.CurrentCountPurchasesGoods.CurrentValueLegendaryGoods;
        _currentPricePurchases.OneClockContracts = _countPricePurchases.OneClockContracts * _marcketEvent.CurrentCountPurchasesGoods.CurrentValueStuff;
        _currentPricePurchases.ThreeClockContracts = _countPricePurchases.ThreeClockContracts * _marcketEvent.CurrentCountPurchasesGoods.CurrentValueStuff;
        _currentPricePurchases.SixClockContracts = _countPricePurchases.SixClockContracts * _marcketEvent.CurrentCountPurchasesGoods.CurrentValueStuff;
        _currentPricePurchases.NineClockContracts = _countPricePurchases.NineClockContracts * _marcketEvent.CurrentCountPurchasesGoods.CurrentValueStuff;
        _currentPricePurchases.TwelveClockContracts = _countPricePurchases.TwelveClockContracts * _marcketEvent.CurrentCountPurchasesGoods.CurrentValueStuff;

    }
    */
    public void OpenPanelMarkketAndParsetStringInInt(string CurrentValue)
    {
        string Value1 = "";
        string Value2 = "";
        bool isOneValue = true;
        for (int i = 0; i < CurrentValue.Length; i++)
        {
            if (CurrentValue[i].ToString() == "_")
            {
                isOneValue = false;
                continue;
            }
            if (isOneValue)
            {
                Value1 += CurrentValue[i];
            }
            else
            {
                Value2 += CurrentValue[i];
            }
            
        }
        GetPrisePanel(int.Parse(Value1), int.Parse(Value2));
    }
    public void GetPrisePanel(int CurentupdateButton, int CurerntButton) 
    {
        MarcketDataTextSetPricePanel Mar = null;
        DataPricePurchases dataPricePurchases = null;

        CostCalculation();
        OnDisableText();

        if (CurerntButton == 10)
        {
            Mar = _marcketDataTextSetPricePanel;
            dataPricePurchases = _countPricePurchases;
        }
        else
        {
            Mar = _currentPriceUIToMarcket;
            dataPricePurchases = _currentPricePurchases;
        }


        switch (CurentupdateButton)
        {
            case (int)CurrentButtonMarcket.PlayerResourses:
                Mar.SuppliesPanel.SetActive(true);

                Mar.Foods.text = dataPricePurchases.Foods.ToString();
                Mar.Rests.text = dataPricePurchases.Rests.ToString();
                Mar.Parts.text = dataPricePurchases.Parts.ToString();
                Mar.Fuel.text = dataPricePurchases.Fuel.ToString();
                break;
            case (int)CurrentButtonMarcket.GoodsResourses:
                Mar.GoodsPanel.SetActive(true);

                Mar.CommonGoods.text = dataPricePurchases.CommonGoods.ToString();
                Mar.RareGoods.text = dataPricePurchases.RareGoods.ToString();
                Mar.EpicGoods.text = dataPricePurchases.EpicGoods.ToString();
                Mar.LegendaryGoods.text = dataPricePurchases.LegendaryGoods.ToString();
                break;
            case (int)CurrentButtonMarcket.Stuff:
                Mar.StuffPanel.SetActive(true);
                Mar.Stuff.text = dataPricePurchases.Stuff.ToString();
                Mar.OneClockContracts.text = dataPricePurchases.OneClockContracts.ToString();
                Mar.ThreeClockContracts.text = dataPricePurchases.ThreeClockContracts.ToString();
                Mar.SixClockContracts.text = dataPricePurchases.SixClockContracts.ToString();
                Mar.NineClockContracts.text = dataPricePurchases.NineClockContracts.ToString();
                Mar.TwelveClockContracts.text = dataPricePurchases.TwelveClockContracts.ToString();
                break;
            default:
                break;
        }

        currentButtonMarcket = (CurrentButtonMarcket)CurentupdateButton;

        if (CurerntButton == 20)
        {
             _currentPriceUIToMarcket.AllCountPrice.text = StartCalcualationAllPriceResourse().ToString();
            Buy.gameObject.SetActive(true);
        }
        else
        {
            Mar.AllPrice.SetActive(true);
        }

        _currentPriceUIToMarcket.AllPrice.SetActive(true);
        BuyPanel.SetActive(true);
    }
    private int StartCalcualationAllPriceResourse()
    {
        int CurrentAllPrice = 0;
        switch (currentButtonMarcket)
        {
            case CurrentButtonMarcket.PlayerResourses:
                CurrentAllPrice = _currentPricePurchases.Foods + _currentPricePurchases.Rests + _currentPricePurchases.Parts + _currentPricePurchases.Fuel;
                break;
            case CurrentButtonMarcket.GoodsResourses:
                CurrentAllPrice = _currentPricePurchases.CommonGoods + _currentPricePurchases.RareGoods + _currentPricePurchases.EpicGoods + _currentPricePurchases.LegendaryGoods;
                break;
            case CurrentButtonMarcket.Stuff:
                CurrentAllPrice = _currentPricePurchases.Stuff + _currentPricePurchases.OneClockContracts + _currentPricePurchases.ThreeClockContracts + _currentPricePurchases.SixClockContracts + _currentPricePurchases.NineClockContracts + _currentPricePurchases.TwelveClockContracts;

                break;
        }
        return CurrentAllPrice;
    }
    private void BuyGoods()
    {
        int AllPrice = StartCalcualationAllPriceResourse();
        var _playerData = PlayerData.instanse.instanseSavePlayerState;
        var CurrentResourse = _marcketEvent.CurrentCountPurchasesGoods;
        if (AllPrice <= PlayerData.instanse.instanseSaveMoneyPlayer.Drive) 
        {
            PlayerData.instanse.instanseSaveMoneyPlayer.Drive -= AllPrice;

            switch (currentButtonMarcket)
            {
                case CurrentButtonMarcket.PlayerResourses:
                   
                    _playerData.Parts += CurrentResourse.CurrentValueParts;
                    _playerData.Fuel += CurrentResourse.CurrentValueFuel;
                    _playerData.Food += CurrentResourse.CurrentValueFood;
                    _playerData.Rest += CurrentResourse.CurrentValueRest;
                    break;
                case CurrentButtonMarcket.GoodsResourses:
                    _playerData.CommonGoods += CurrentResourse.CurrentValueCommonGoods;
                    _playerData.RareGoods += CurrentResourse.CurrentValueRareGoods;
                    _playerData.EpicGoods += CurrentResourse.CurrentValueEpicGoods;
                    _playerData.legendaryGoods += CurrentResourse.CurrentValueLegendaryGoods;
                    break;
                case CurrentButtonMarcket.Stuff:
                    _playerData.StuffMoney += CurrentResourse.CurrentValueStuff;
                    _playerData.ContratsOneHour += CurrentResourse.CurrentValueOneClockContracts; 
                    _playerData.ContratsThreeHour += CurrentResourse.CurrentValueThreeClockContracts; 
                    _playerData.ContratsSixHour += CurrentResourse.CurrentValueSixClockContracts; 
                    _playerData.ContratsNineHour += CurrentResourse.CurrentValueNineClockContracts; 
                    _playerData.ContratsTwelveHour += CurrentResourse.CurrentValueTwelveClockContracts; 
                    break;
            }
            _marcketEvent.OnDisable();
        }
        else
        {
            Debug.Log("Не хватает денег");
        }
    }
    private void OnDisableText()
    {
        for (int i = 0; i < 2; i++)
        {
            MarcketDataTextSetPricePanel Mar = null;
            if (i ==  0)
            {
                Mar = _marcketDataTextSetPricePanel; //Цена за одну единицу
            }
            else
            {
                Mar = _currentPriceUIToMarcket; //Цена выбранных товаров
                _currentPriceUIToMarcket.AllCountPrice.text = "";
            }

            Mar.Foods.text = "";
            Mar.Rests.text = "";
            Mar.Parts.text = "";
            Mar.Fuel.text = "";
            Mar.CommonGoods.text = "";
            Mar.RareGoods.text = "";
            Mar.EpicGoods.text = "";
            Mar.LegendaryGoods.text = "";
            Mar.Stuff.text = "";

            Mar.OneClockContracts.text = "";
            Mar.ThreeClockContracts.text = "";
            Mar.SixClockContracts.text = "";
            Mar.NineClockContracts.text = "";
            Mar.TwelveClockContracts.text = "";



            Mar.SuppliesPanel.SetActive(false);
            Mar.GoodsPanel.SetActive(false);
            Mar.StuffPanel.SetActive(false);
            Mar.AllPrice.SetActive(false);
            Buy.gameObject.SetActive(false);
        }
    }
}   