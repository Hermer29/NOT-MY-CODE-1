using UnityEngine;
using UnityEngine.UI;

public class MarcketEvent : MonoBehaviour
{
    [SerializeField] Button BuyFoodPlus;
    [SerializeField] Button BuyFoodMinus;
    [SerializeField] Button BuyRestPlus;
    [SerializeField] Button BuyRestMinus;
    [SerializeField] Button BuyPartsPlus;
    [SerializeField] Button BuyPartsMinus;
    [SerializeField] Button BuyFuelPlus;
    [SerializeField] Button BuyFuelMinus;

    [SerializeField] Button BuyGood1Plus;
    [SerializeField] Button BuyGood1Minus;
    [SerializeField] Button BuyGood2Plus;
    [SerializeField] Button BuyGood2Minus;
    [SerializeField] Button BuyGood3Plus;
    [SerializeField] Button BuyGood3Minus;
    [SerializeField] Button BuyGood4Plus;
    [SerializeField] Button BuyGood4Minus;

    [SerializeField] Button BuyStuffMinus;
    [SerializeField] Button BuyStuffPlus;

    [SerializeField] Button BuyOneClockContractsMinus;
    [SerializeField] Button BuyOneClockContractsPlus;

    [SerializeField] Button BuyThreeClockContractsMinus;
    [SerializeField] Button BuyThreeClockContractsPlus;

    [SerializeField] Button BuySixClockContractsMinus;
    [SerializeField] Button BuySixClockContractsPlus;

    [SerializeField] Button BuyNineClockContractsMinus;
    [SerializeField] Button BuyNineClockContractsPlus;

    [SerializeField] Button BuyTwelveClockContractsMinus;
    [SerializeField] Button BuyTwelveClockContractsPlus;

    private MarcketData _marcketData = new MarcketData();
    private  Marcket _marcket;
    public MarcketData CurrentCountPurchasesGoods { get => _marcketData; set => _marcketData = value; }
    [field: SerializeField] private MarcketUpdateUI marcketUpdateUI; //Прокинуть руками
    //DataMarcket;
    private void Awake()
    {
      _marcket = new Marcket(_marcketData, marcketUpdateUI);
    }
    private void Start()
    {
        #region CardState
        BuyFoodPlus.onClick.AddListener(() => _marcket.MethodsPlus(ref _marcketData.CurrentValueFood));
        BuyRestPlus.onClick.AddListener(() => _marcket.MethodsPlus(ref _marcketData.CurrentValueRest));
        BuyPartsPlus.onClick.AddListener(() => _marcket.MethodsPlus(ref _marcketData.CurrentValueParts));
        BuyFuelPlus.onClick.AddListener(() => _marcket.MethodsPlus(ref _marcketData.CurrentValueFuel));
      

        BuyFoodMinus.onClick.AddListener(() => _marcket.MethodsMinus(ref _marcketData.CurrentValueFood));
        BuyRestMinus.onClick.AddListener(() => _marcket.MethodsMinus(ref _marcketData.CurrentValueRest));
        BuyPartsMinus.onClick.AddListener(() => _marcket.MethodsMinus(ref _marcketData.CurrentValueParts));
        BuyFuelMinus.onClick.AddListener(() => _marcket.MethodsMinus(ref _marcketData.CurrentValueFuel));

        #endregion
        #region Goods

        BuyGood1Plus.onClick.AddListener(() => _marcket.MethodsPlus(ref _marcketData.CurrentValueCommonGoods));
        BuyGood2Plus.onClick.AddListener(() =>  _marcket.MethodsPlus(ref _marcketData.CurrentValueRareGoods));
        BuyGood3Plus.onClick.AddListener(() =>  _marcket.MethodsPlus(ref _marcketData.CurrentValueEpicGoods));
        BuyGood4Plus.onClick.AddListener(() => _marcket.MethodsPlus(ref  _marcketData.CurrentValueLegendaryGoods));


        BuyGood1Minus.onClick.AddListener(() => _marcket.MethodsMinus(ref _marcketData.CurrentValueCommonGoods));
        BuyGood2Minus.onClick.AddListener(() => _marcket.MethodsMinus(ref _marcketData.CurrentValueRareGoods));
        BuyGood3Minus.onClick.AddListener(() => _marcket.MethodsMinus(ref _marcketData.CurrentValueEpicGoods));
        BuyGood4Minus.onClick.AddListener(() => _marcket.MethodsMinus(ref _marcketData.CurrentValueLegendaryGoods));
        #endregion
        #region Stuff
        BuyStuffMinus.onClick.AddListener(() => _marcket.MethodsMinus(ref _marcketData.CurrentValueStuff));
        BuyOneClockContractsMinus.onClick.AddListener(() => _marcket.MethodsMinus(ref _marcketData.CurrentValueOneClockContracts));
        BuyThreeClockContractsMinus.onClick.AddListener(() => _marcket.MethodsMinus(ref _marcketData.CurrentValueThreeClockContracts));
        BuySixClockContractsMinus.onClick.AddListener(() => _marcket.MethodsMinus(ref _marcketData.CurrentValueSixClockContracts));
        BuyNineClockContractsMinus.onClick.AddListener(() => _marcket.MethodsMinus(ref _marcketData.CurrentValueNineClockContracts));
        BuyTwelveClockContractsMinus.onClick.AddListener(() => _marcket.MethodsMinus(ref _marcketData.CurrentValueTwelveClockContracts));

        BuyStuffPlus.onClick.AddListener(() => _marcket.MethodsPlus(ref _marcketData.CurrentValueStuff));
        BuyOneClockContractsPlus.onClick.AddListener(() => _marcket.MethodsPlus(ref _marcketData.CurrentValueOneClockContracts));
        BuyThreeClockContractsPlus.onClick.AddListener(() => _marcket.MethodsPlus(ref _marcketData.CurrentValueThreeClockContracts));
        BuySixClockContractsPlus.onClick.AddListener(() => _marcket.MethodsPlus(ref _marcketData.CurrentValueSixClockContracts));
        BuyNineClockContractsPlus.onClick.AddListener(() => _marcket.MethodsPlus(ref _marcketData.CurrentValueNineClockContracts));
        BuyTwelveClockContractsPlus.onClick.AddListener(() => _marcket.MethodsPlus(ref _marcketData.CurrentValueTwelveClockContracts));
      #endregion
    }
    private void OnDestroy()
    {
        #region CardState

        BuyFoodPlus.onClick.RemoveAllListeners();
        BuyRestPlus.onClick.RemoveAllListeners();
        BuyPartsPlus.onClick.RemoveAllListeners();
        BuyFuelPlus.onClick.RemoveAllListeners();
        BuyStuffPlus.onClick.RemoveAllListeners();

        BuyFoodMinus.onClick.RemoveAllListeners();
        BuyRestMinus.onClick.RemoveAllListeners();
        BuyPartsMinus.onClick.RemoveAllListeners();
        BuyFuelMinus.onClick.RemoveAllListeners();
        BuyStuffMinus.onClick.RemoveAllListeners();
        #endregion

        #region Goods

        BuyGood1Plus.onClick.RemoveAllListeners();
        BuyGood2Plus.onClick.RemoveAllListeners();
        BuyGood3Plus.onClick.RemoveAllListeners();
        BuyGood4Plus.onClick.RemoveAllListeners();


        BuyGood1Minus.onClick.RemoveAllListeners();
        BuyGood2Minus.onClick.RemoveAllListeners();
        BuyGood3Minus.onClick.RemoveAllListeners();
        BuyGood4Minus.onClick.RemoveAllListeners();

        #endregion

        #region Stuff

        BuyOneClockContractsMinus.onClick.RemoveAllListeners();
        BuyThreeClockContractsMinus.onClick.RemoveAllListeners();
        BuySixClockContractsMinus.onClick.RemoveAllListeners();
        BuyNineClockContractsMinus.onClick.RemoveAllListeners();
        BuyTwelveClockContractsMinus.onClick.RemoveAllListeners();

        BuyStuffPlus.onClick.RemoveAllListeners();
        BuyOneClockContractsPlus.onClick.RemoveAllListeners();
        BuyThreeClockContractsPlus.onClick.RemoveAllListeners();
        BuySixClockContractsPlus.onClick.RemoveAllListeners();
        BuyNineClockContractsPlus.onClick.RemoveAllListeners();
        BuyTwelveClockContractsPlus.onClick.RemoveAllListeners();
        #endregion
    }
    public void OnDisable() 
    {
        _marcket.ResetData(_marcketData);
    }

}
public class MarcketData
{
    public int CurrentValueFood;
    public int CurrentValueRest;
    public int CurrentValueParts;
    public int CurrentValueFuel;
    public int CurrentValueCommonGoods;
    public int CurrentValueRareGoods;
    public int CurrentValueEpicGoods;
    public int CurrentValueLegendaryGoods;
    public int CurrentValueStuff;

    public int CurrentValueOneClockContracts;
    public int CurrentValueThreeClockContracts;
    public int CurrentValueSixClockContracts;
    public int CurrentValueNineClockContracts;
    public int CurrentValueTwelveClockContracts;


    public void ResetData()
    {
        CurrentValueFood = 0;
        CurrentValueRest = 0;
        CurrentValueParts = 0;
        CurrentValueFuel = 0;
        CurrentValueCommonGoods = 0;
        CurrentValueRareGoods = 0;
        CurrentValueEpicGoods = 0;
        CurrentValueLegendaryGoods = 0;
        CurrentValueStuff = 0;

        CurrentValueOneClockContracts = 0;
        CurrentValueThreeClockContracts = 0;
        CurrentValueSixClockContracts = 0;
        CurrentValueNineClockContracts = 0;
        CurrentValueTwelveClockContracts = 0;
    }
}