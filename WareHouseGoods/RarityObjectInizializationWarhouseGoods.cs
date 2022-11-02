using Assets.Code.WareHouseGoods;
using Code.MainMenu.Timer;
using System;
using UnityEngine;
using UnityEngine.UI;

public class RarityObjectInizializationWarhouseGoods : MonoBehaviour //Цепляется на каждый товар 
{
    public Rarity CurrentRarity;
    public Rarity RarityWareHouse;
    [field: SerializeField] private DataPanelPurchasesSubscrption dataPanelPurchasesSubscrption { get; set; } = new DataPanelPurchasesSubscrption();
    [field: SerializeField] private CurrentProduce curentProduce { get; set; } = new CurrentProduce();

    [field: SerializeField] private Image CurrentImageProduct { get; set; }

    [SerializeField] Button ButtonStakeToProdece;
    [SerializeField] Button ButtonSetPrice;
    [SerializeField] Button ButtonProduce;
    [SerializeField] Text CurrentCountGoods;
    public Text CurrentCreateProduct;
    public int CurrentCountGoodsP { get; set; }
    public int _count { get; set; }
    private PlayerData _playerData;
    [field: SerializeField] private GameObject CurrentPanelBuyGoods { get; set; }

    private void Awake()
    {
        dataPanelPurchasesSubscrption.BuySubscription.onClick.AddListener(() => BuySubscription(dataPanelPurchasesSubscrption.CurrentRarity));

        ButtonStakeToProdece.onClick.AddListener(() => ActiveSubcription(CurrentRarity, true)); //занесение суммы втекущий стейкинг
        ButtonProduce.onClick.AddListener(() => ActiveSubcription(CurrentRarity, true)); //Начать производство
        ButtonSetPrice.onClick.AddListener(() => CurrentPanelBuyGoods.SetActive(true)); // Открыть панельку в которой можно будет выставлять товары.
    }
    private void Start()
    {
        UpdateDisplayToPanel();
    }
    private void OnEnable()
    {
        UpdateDisplayToPanel();
    }

    private void OnDestroy()
    {
        dataPanelPurchasesSubscrption.BuySubscription.onClick.RemoveListener(() => BuySubscription(dataPanelPurchasesSubscrption.CurrentRarity));

        ButtonStakeToProdece.onClick.RemoveListener(() => ActiveSubcription(CurrentRarity, true));
        ButtonProduce.onClick.RemoveListener(() => ActiveSubcription(CurrentRarity,true));
        ButtonSetPrice.onClick.RemoveListener(() => CurrentPanelBuyGoods.SetActive(true));

    }
    private void UpdateDisplayToPanel()
    {
        _playerData = PlayerData.instanse;
        WareHouseGoodS CurrentActiveWarhouse = _playerData.instanseSaveCard.ListActiveCardWareHouseGoodS[0];
        StartUpdateResourse(CurrentActiveWarhouse, CurrentRarity);
        ActiveSubcription(CurrentRarity, false);
    }
    private void StartUpdateResourse(WareHouseGoodS CurrentActiveWarhouse, Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common:
                _playerData.instanseSavePlayerState.CommonGoods = CurrentActiveWarhouse.CommonGoods;
                CurrentCountGoods.text = _playerData.instanseSavePlayerState.CommonGoods.ToString();

                break;
            case Rarity.Rare:

                _playerData.instanseSavePlayerState.RareGoods = CurrentActiveWarhouse.RareGoods;
                CurrentCountGoods.text = _playerData.instanseSavePlayerState.RareGoods.ToString();

                break;
            case Rarity.Epic:

                _playerData.instanseSavePlayerState.EpicGoods = CurrentActiveWarhouse.EpicGoods;
                CurrentCountGoods.text = _playerData.instanseSavePlayerState.EpicGoods.ToString();

                break;
            case Rarity.Legendary:
                _playerData.instanseSavePlayerState.legendaryGoods = CurrentActiveWarhouse.LegendaryGoods;
                CurrentCountGoods.text = _playerData.instanseSavePlayerState.legendaryGoods.ToString();

                break;
        }
    }
    
    private void OpenToPanelBuySubscriptions(bool isOpenPanel) // При нажатие кнопки открывать панель и предавать параметры. //Делать проверку по текущим выплаченным ресурсам. Сделать int переменную для этого
    {
        dataPanelPurchasesSubscrption.PanelSubscription.SetActive(isOpenPanel);
        dataPanelPurchasesSubscrption.PriceSubscription.text = "Price: 100" + " Drive";
        dataPanelPurchasesSubscrption.CountDaySubscription.text = "Time 10" + " Day";
        CurrentImageProduct.enabled = false;
    }

    private void OpenDataPanelProduction(DataSubscription dataSubscription, bool isOpenPanel) //Add Rarity
    {
        var a = DateTime.Now - dataSubscription.CurrentDataSubsription; //кол-во прошедшего времени
        var b = dataSubscription.FinalDataSubsription - DateTime.Now; //Получаем не правильное оставшеся время.
        var v = a.Hours;
        
        UpdateDisplayPanelProduct(dataSubscription, isOpenPanel);
    }
    private void BuySubscription(Rarity rarity) 
    {
        //Hard Code
        if (rarity != CurrentRarity)
        {
            return;
        }
        if (PlayerData.instanse.instanseSaveMoneyPlayer.Drive > 100)
        {
            var a = new DataSubscription() { CurrentDataSubsription = DateTime.Now, FinalDataSubsription = DateTime.Now.AddDays(10), RaritySbsription = CurrentRarity, CurrentIssuedGoods = 0 };

            PlayerData.instanse.instanseSaveCard.ListActiveCardWareHouseGoodS[0].dataSubscriptions.Add(a);
            var Timer1 = new InstanseTimer(DateTime.Now.AddDays(10), curentProduce.CurrentEndTimeSubscription, 0); //TODO 2: ПОКА прокидываю 0, но в далльнейшем для сохранения сделать так чтобы можно было определить.
            var Timer2 =  new InstanseTimer(DateTime.Now.AddHours(1), curentProduce.CurrentPoduceTime, 0);
            curentProduce.ListTimerUpdateDay.Add(Timer1);
            curentProduce.ListTimerUpdateDay.Add(Timer2);
            CurrentImageProduct.enabled = true;
            PlayerData.instanse.instanseSaveMoneyPlayer.Drive -= 100;
        }
    }
    private void ActiveSubcription(Rarity rarity, bool isOpenPanel)
    {
        var a = PlayerData.instanse.instanseSaveCard.ListActiveCardWareHouseGoodS[0];
        bool isCurentSubscription = false;
        dataPanelPurchasesSubscrption.CurrentRarity = rarity;

        foreach (var item in a.dataSubscriptions)
        {
            if (rarity == item.RaritySbsription)
            {
                OpenDataPanelProduction(item, isOpenPanel);
                isCurentSubscription = true;
                break;
            }
        }
        if (!isCurentSubscription)
        {
            OpenToPanelBuySubscriptions(isOpenPanel);
        }
    }
    private void UpdateDisplayPanelProduct(DataSubscription dataSubscription, bool isOpenPanel)
    {
        curentProduce.ListTimerUpdateDay.RemoveRange(0, curentProduce.ListTimerUpdateDay.Count); //Обнуление

        curentProduce.CurrentPoduceTime.text = "000000";
        curentProduce.CurrentEndTimeSubscription.text = "000000";
        curentProduce.PanelDataProduct.SetActive(isOpenPanel);


        var Timer1 = new InstanseTimer(dataSubscription.FinalDataSubsription, curentProduce.CurrentEndTimeSubscription, 0);
        var Timer2 = new InstanseTimer(dataSubscription.CurrentDataSubsription.AddHours(1), curentProduce.CurrentPoduceTime, 0); //Todo 2: Заглушка
        curentProduce.ListTimerUpdateDay.Add(Timer1);
        curentProduce.ListTimerUpdateHour.Add(Timer2);
        CurrentImageProduct.enabled = true;

        curentProduce.CurrentRarityPoduce.text = "Rarity Product: " + CurrentRarity; 
    }
}
