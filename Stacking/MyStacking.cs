using System;
using UnityEngine;
using UnityEngine.UI;
public enum MyStakingE
{
    TenPercent = 240, FiftyPercent = 720, HundredPercent = 1200, Defolt
}
public class MyStacking : MonoBehaviour
{
    [SerializeField] InputField InputField10Percent;
    [SerializeField] InputField InputField50Percent;
    [SerializeField] InputField InputField100Percent;

    [SerializeField] private Text _commonGoods;
    [SerializeField] private Text _rareGoods;
    [SerializeField] private Text _epicGoods;
    [SerializeField] private Text _legendaryGoods;
    
    [SerializeField] private HeandlerPayment _heandlerPayment = new HeandlerPayment();
    [field: SerializeField] Text BalancePlayer { get; set; }
    public string InputField10PercentP
    {
        get { return InputField10Percent.text; }
        set { InputField10Percent.text = value; }
    }
    public string InputField50PercentP
    {
        get { return InputField50Percent.text; }
        set { InputField50Percent.text = value; }
    }

    public string InputField100PercentP
    {
        get { return InputField100Percent.text; }
        set { InputField100Percent.text = value; }
    }

    public static MyStacking myStacking;
    private void Awake()
    {
         myStacking = this;
        _heandlerPayment.UiDataMyStocks.ButtonHeaderStacking.onClick.AddListener(() => _heandlerPayment.StartHeaderToMyStacting());
        _heandlerPayment.UiDataMyStocks.StartStacking.onClick.AddListener(() => _heandlerPayment.StartStaking());
        _heandlerPayment.UiDataMyStocks.StartStacking.onClick.AddListener(() => ResetStacking());
        //_heandlerPayment.UiDataMyStocks.UnstakeButton.onClick.AddListener(() => ResetStacking()); //TODO
    }

    private void OnEnable()
    {
        InizializationGoods.StartInizializationGoods(PlayerData.instanse, _commonGoods, _rareGoods, _epicGoods, _legendaryGoods);
    }
    public void ResetStacking()
    {
        InputField10Percent.text = "";
        InputField50Percent.text = "";
        InputField100Percent.text = "";
        _heandlerPayment.UiDataMyStocks.CurrentPresent.text = "0";
        _heandlerPayment.UiDataMyStocks.ReceivedPresent.text = "0";
    }
    private void FixedUpdate()
    {
        BalancePlayer.text = PlayerData.instanse.instanseSaveMoneyPlayer.Drive.ToString();
    }
    //Открывать панель с текущими стейками и показывать оставшееся время
}

[Serializable]
public class HeandlerPayment
{
    public UiDataMyStocks UiDataMyStocks = new UiDataMyStocks();
    public MyStakingE MyStackingE;
    private PlayerData _playerData;
    private int CurrentPayment;
    private int CurrentPresent;
    private MyStacking _myStacking;
   [field: SerializeField] private PanelUnstake _panelUnstake { get; set; } 

    public void StartHeaderToMyStacting()
    {
        int _tenPercentValue =0;
        int _fiftyPercentValue = 0;
        int _hundredPercentValue = 0;
        _myStacking = MyStacking.myStacking;
        _playerData = PlayerData.instanse;
        try
        {
            if (_myStacking.InputField10PercentP != "")
            {
                _tenPercentValue = int.Parse(_myStacking.InputField10PercentP);
            }
        }
        catch (Exception)
        {
            Debug.Log("Введено не верное значение");
            return;
        }
        try
        {
            if (_myStacking.InputField50PercentP != "")
            {
                _fiftyPercentValue = int.Parse(_myStacking.InputField50PercentP);
            }
        }
        catch (Exception)
        {
            Debug.Log("Введено не верное значение");
            return;
        }
        try
        {
            if (_myStacking.InputField100PercentP!= "")
            {
                _hundredPercentValue = int.Parse(_myStacking.InputField100PercentP);
            }
        }
        catch (Exception)
        {
            Debug.Log("Введено не верное значение");
            return;
        }
        //Настроить инпут филды под текст.

        if (_tenPercentValue != 0)
        {
            CurrentPayment = _tenPercentValue;
            CurrentPresent = CurrentPayment + ((_tenPercentValue * 10)/ 100);

            MyStackingE = MyStakingE.TenPercent;

            OpenPanelPresent();
            return;
        }
        if (_fiftyPercentValue != 0 && _tenPercentValue == 0 && _hundredPercentValue == 0)
        {
            CurrentPayment = _fiftyPercentValue;
            CurrentPresent = CurrentPayment + ((_fiftyPercentValue * 50) / 100);

            MyStackingE = MyStakingE.FiftyPercent;

            OpenPanelPresent();
            return;
        }
        if (_fiftyPercentValue == 0 && _tenPercentValue == 0 && _hundredPercentValue != 0)
        {
            CurrentPayment = _hundredPercentValue;

            CurrentPresent = CurrentPayment + ((_hundredPercentValue * 100) / 100);

            MyStackingE = MyStakingE.HundredPercent;

            OpenPanelPresent();
            return;
        }
    }
    public void OpenPanelPresent()
    {
        UiDataMyStocks.PanelPresent.SetActive(true);
        UiDataMyStocks.CurrentPresent.text = CurrentPayment.ToString();
        UiDataMyStocks.ReceivedPresent.text = CurrentPresent.ToString();
    }
    public void StartStaking()
    {
        if (_playerData.instanseSaveMoneyPlayer.Drive < CurrentPayment)
        {
            Debug.LogError("НЕ хватает денег");
            return;
        }
        int Iteration = 0;
        int TimeStartStacking = 0;
        switch (MyStackingE)
        {
            case MyStakingE.TenPercent:
                Iteration++;
                TimeStartStacking = 1;
                break;
            case MyStakingE.FiftyPercent:
                Iteration++;
                TimeStartStacking = 5;
                break;
            case MyStakingE.HundredPercent:
                Iteration++;
                TimeStartStacking = 10;
                break;
            default:
                break;
        }
        if (Iteration!= 0 )
        {
            _playerData.instanseSaveMoneyPlayer.Drive -= CurrentPayment;
            /* var CurrentData = DateTime.Now;
            var a = (int)MyStackingE;
            var CurrentDay =  a / 24;
            var CurrentClock = a % 24;
            var CurrentDataDays = CurrentData.AddDays(CurrentDay);
            var CurrentDataClock  = CurrentData.AddHours(CurrentClock); */
            var addedavePlayerStock = new AddedavePlayerStock() { CurrentPresent = CurrentPresent, CurrentDateTime = DateTime.Now, DataPresent = DateTime.Now.AddHours((int)MyStackingE) };
            _playerData.instanseSaveAddedPlayerPayment.AddedavePlayertock.Add(addedavePlayerStock);
            var Index = _playerData.instanseSaveAddedPlayerPayment.AddedavePlayertock.IndexOf(addedavePlayerStock);

            _panelUnstake.EventAddToSell?.Invoke(addedavePlayerStock, Index);
        }
    }
  
}
[Serializable]
public class UiDataMyStocks 
{
    public Text CurrentPresent;
    public Text ReceivedPresent;
    public GameObject PanelPresent;
    public Button ButtonHeaderStacking;
    public Button StartStacking;
    //public Button UnstakeButton { get; }
}