using UnityEngine;
using UnityEngine.UI;

public class HeadlerCheckCountGoods : MonoBehaviour //Панель загрузки товара
{
    public GameObject Common;
    public GameObject Rare;
    public GameObject Epic;
    public GameObject Legendary;
    public Button SaveCurrentSettingsGoods; 
    public Image iconProduct;
    public Text CountGoodsCurrentText;

    //Fields
    WrapperPanel WrapperPanelCommon;
    WrapperPanel WrapperPanelRare;
    WrapperPanel WrapperPanelEpic;
    WrapperPanel WrapperPanelLegendary;

    private int CurrentCountGoodsToActivePlayer;
    public int CurrentCountGoodsToActivePlayerProp
    {
        get { return CurrentCountGoodsToActivePlayer; }
        set { CurrentCountGoodsToActivePlayer = value; }
    }

    public GameObject CurrentActivePlayer { get; set; }
    public int CountGoodsCurrent { get; set; }
    private PlayerData playerData { get; set; }
    private byte Oneload;

    Trailer _trailer;

    private void Awake()
    {
        TrailerPanelController.OnActionPanelGoods += CheckGoods;

        WrapperPanelCommon = Common.GetComponent<WrapperPanel>();
        WrapperPanelRare = Rare.GetComponent<WrapperPanel>();
        WrapperPanelEpic = Epic.GetComponent<WrapperPanel>();
        WrapperPanelLegendary = Legendary.GetComponent<WrapperPanel>();
    }
    private void InizializatoinPlayerData()
    {
        playerData = PlayerData.instanse;
        Oneload++;
    }

    public void PlusClick(GameObject RareGameObj)
    {
        WrapperPanel CurrentPanelData = RareGameObj.GetComponent<WrapperPanel>(); //Нужно чтобы определить тип раритетности панели
        var CurrentCountGoodsPlayer = CurrentActivePlayer.GetComponent<LinkCurrentCard>()._dataCurrentCardTrailer.CurrentDataCard;
        CurrentPanelData.TotalCountGoods = CurrentCountGoodsPlayer.TotalCountResource;
        

        CurrentPanelData.PlayerStateCountGoods.text = CurrentCountGoodsPlayer.CurrentCountResource.ToString();
        switch (CurrentPanelData.Rarity)
        {
            case Rarity.Common:
                if (playerData.instanseSavePlayerState.CommonGoods > 0 && WrapperPanelCommon.TotalCountGoods > CurrentCountGoodsToActivePlayerProp)
                {
                    WrapperPanelCommon.DataCountPlayerGoods += 1;
                    CurrentCountGoodsToActivePlayerProp += 1;
                    playerData.instanseSavePlayerState.CommonGoods -= 1;
                }

                break;
            case Rarity.Rare:
                if (playerData.instanseSavePlayerState.RareGoods > 0 && WrapperPanelRare.TotalCountGoods > CurrentCountGoodsToActivePlayerProp)
                {
                    WrapperPanelRare.DataCountPlayerGoods += 1;
                    CurrentCountGoodsToActivePlayerProp += 1;
                    playerData.instanseSavePlayerState.RareGoods -= 1;
                }

                break;
            case Rarity.Epic:
                if (playerData.instanseSavePlayerState.EpicGoods > 0 && WrapperPanelEpic.TotalCountGoods > CurrentCountGoodsToActivePlayerProp)
                {
                    WrapperPanelEpic.DataCountPlayerGoods += 1;
                    CurrentCountGoodsToActivePlayerProp += 1;
                    playerData.instanseSavePlayerState.EpicGoods -= 1;
                }

                break;
            case Rarity.Legendary:
                if (playerData.instanseSavePlayerState.legendaryGoods > 0 && WrapperPanelLegendary.TotalCountGoods > CurrentCountGoodsToActivePlayerProp)
                {
                    WrapperPanelLegendary.DataCountPlayerGoods += 1;
                    CurrentCountGoodsToActivePlayerProp += 1;
                    playerData.instanseSavePlayerState.legendaryGoods -= 1;
                }
                break;
        }

        //OnInizializatoinGoodsPanel(_trailer.CommonGoods, _trailer.RareGoods, _trailer.EpicGoods, _trailer.LegendaryGoods);
        CurrentPanelData.PlayerStateCountGoods.text = CurrentPanelData.DataCountPlayerGoods.ToString();
    }
    public void MinusClick(GameObject CurrentPanelToActive)
    {
        WrapperPanel CurrentPanelData = CurrentPanelToActive.GetComponent<WrapperPanel>(); //Нужно чтобы определить тип раритетности панели
        var CurrentCountGoodsPlayer = CurrentActivePlayer.GetComponent<LinkCurrentCard>()._dataCurrentCardTrailer.CurrentDataCard;
        CurrentPanelData.TotalCountGoods = CurrentCountGoodsPlayer.TotalCountResource;

        CurrentPanelData.PlayerStateCountGoods.text = CurrentCountGoodsPlayer.CurrentCountResource.ToString();

        switch (CurrentPanelData.Rarity)
        {
            case Rarity.Common:
                if (WrapperPanelCommon.DataCountPlayerGoods > 0 )
                {
                    WrapperPanelCommon.DataCountPlayerGoods -= 1;
                    CurrentCountGoodsToActivePlayerProp -= 1;
                    playerData.instanseSavePlayerState.CommonGoods += 1;
                }
                break;
            case Rarity.Rare:
                if (WrapperPanelRare.DataCountPlayerGoods > 0)
                {
                    WrapperPanelRare.DataCountPlayerGoods -= 1;
                    CurrentCountGoodsToActivePlayerProp -= 1;
                    playerData.instanseSavePlayerState.RareGoods += 1;
                }
                break;
            case Rarity.Epic:
                if (WrapperPanelEpic.DataCountPlayerGoods > 0)
                {
                    WrapperPanelEpic.DataCountPlayerGoods -= 1;
                    CurrentCountGoodsToActivePlayerProp -= 1;
                    playerData.instanseSavePlayerState.EpicGoods += 1;
                }
                break;
            case Rarity.Legendary:
                if (WrapperPanelLegendary.DataCountPlayerGoods > 0)
                {
                    WrapperPanelLegendary.DataCountPlayerGoods -= 1;
                    CurrentCountGoodsToActivePlayerProp -= 1;
                    playerData.instanseSavePlayerState.legendaryGoods -= 1;
                }
                break;
        }


        //OnInizializatoinGoodsPanel(_trailer.CommonGoods, _trailer.RareGoods, _trailer.EpicGoods, _trailer.LegendaryGoods);
        CurrentPanelData.PlayerStateCountGoods.text = CurrentPanelData.DataCountPlayerGoods.ToString();
    }
    private void CheckGoods(GameObject CurrentGameobj)  //Сюда мы передаем текущего игрока.
    {
        gameObject.SetActive(true); // Открытие панели.
        var linkCurrentCard = CurrentGameobj.GetComponent<LinkCurrentCard>();
         _trailer = linkCurrentCard._dataCurrentCardTrailer.CurrentDataCard;
        CurrentActivePlayer = CurrentGameobj; //Используется в Plus and Minus Methods.
        if (Oneload == 0)
        {
            InizializatoinPlayerData();
        }
  
        CountGoodsCurrentText.text = "Count Load: " + _trailer.TotalCountResource.ToString();
        CurrentCountGoodsToActivePlayerProp = _trailer.CurrentCountResource;

        switch (linkCurrentCard._dataCurrentCardTrailer.CurrentDataCard.Rarity)
        {
            case Rarity.Common:
                SetActivePanel(true, false, false, false);
                Test(CurrentGameobj);
                break;
            case Rarity.Rare:
                SetActivePanel(true, true, false, false);
                Test(CurrentGameobj);
                break;
            case Rarity.Epic:
                SetActivePanel(true, true, true, false);
                Test(CurrentGameobj);
                break;
            case Rarity.Legendary:
                SetActivePanel(true, true, true, true);
                Test(CurrentGameobj);
                break;
            default:
                break;
        }

        OnInizializatoinGoodsPanel(_trailer.CommonGoods, _trailer.RareGoods, _trailer.EpicGoods, _trailer.LegendaryGoods);
    }
    private void SetActivePanel(bool CommonSetSctive, bool RareSetSctive, bool EpicSetSctive, bool LegendarySetSctive)
    {
        Common.SetActive(CommonSetSctive);
        WrapperPanelCommon.ButtonPlus.gameObject.SetActive(CommonSetSctive);
        WrapperPanelCommon.ButtonMinus.gameObject.SetActive(CommonSetSctive);

        WrapperPanelCommon.CurrentCountGoods.text = playerData.instanseSavePlayerState.CommonGoods.ToString();

        Rare.SetActive(RareSetSctive);
        WrapperPanelRare.ButtonPlus.gameObject.SetActive(RareSetSctive);
        WrapperPanelRare.ButtonMinus.gameObject.SetActive(RareSetSctive);

        WrapperPanelRare.CurrentCountGoods.text = playerData.instanseSavePlayerState.RareGoods.ToString();

        Epic.SetActive(EpicSetSctive);
        WrapperPanelEpic.ButtonPlus.gameObject.SetActive(EpicSetSctive);
        WrapperPanelEpic.ButtonMinus.gameObject.SetActive(EpicSetSctive);

        WrapperPanelEpic.CurrentCountGoods.text = playerData.instanseSavePlayerState.EpicGoods.ToString();

        Legendary.SetActive(LegendarySetSctive);
        WrapperPanelLegendary.ButtonPlus.gameObject.SetActive(LegendarySetSctive);
        WrapperPanelLegendary.ButtonMinus.gameObject.SetActive(LegendarySetSctive);

        WrapperPanelLegendary.CurrentCountGoods.text = playerData.instanseSavePlayerState.legendaryGoods.ToString();

    }
    private void Test(GameObject CurrentGameobj)
    {
        SaveCurrentSettingsGoods.onClick.AddListener(() => AddedDataGoods(CurrentGameobj));
    }
    private void AddedDataGoods(GameObject CurrentGameobj)
    {
        var DataGoodsCurrentPlayer =  CurrentGameobj.GetComponent<LinkCurrentCard>()._dataCurrentCardTrailer.CurrentDataCard; // DataGoodsCurrentPlayer

        DataGoodsCurrentPlayer.CommonGoods = WrapperPanelCommon.DataCountPlayerGoods;
        DataGoodsCurrentPlayer.RareGoods = WrapperPanelRare.DataCountPlayerGoods;
        DataGoodsCurrentPlayer.EpicGoods = WrapperPanelEpic.DataCountPlayerGoods;
        DataGoodsCurrentPlayer.LegendaryGoods = WrapperPanelLegendary.DataCountPlayerGoods;

        DataGoodsCurrentPlayer.CurrentCountResource = WrapperPanelCommon.DataCountPlayerGoods + WrapperPanelRare.DataCountPlayerGoods + WrapperPanelEpic.DataCountPlayerGoods + WrapperPanelLegendary.DataCountPlayerGoods;
        CurrentGameobj.GetComponent<linkToElements>().CurrentLoadGoods.text = DataGoodsCurrentPlayer.CurrentCountResource.ToString() + " / " + DataGoodsCurrentPlayer.TotalCountResource.ToString();

        TrailerPanelController.EventStatseGoods?.Invoke(DefineStat.AllGeneratorResourse(DataGoodsCurrentPlayer));

        SaveCurrentSettingsGoods.onClick.RemoveAllListeners(); //Отписка.
        OnInizializatoinGoodsPanel();
    }
    private void OnInizializatoinGoodsPanel(int CommonCountGoods = 0, int RareCountGoods = 0, int EpicCountGoods = 0, int LegendaryCountGoods = 0)
    {

        WrapperPanelCommon.DataCountPlayerGoods = CommonCountGoods;
        WrapperPanelCommon.PlayerStateCountGoods.text = CommonCountGoods.ToString();

        WrapperPanelRare.DataCountPlayerGoods = RareCountGoods;
        WrapperPanelRare.PlayerStateCountGoods.text = RareCountGoods.ToString();

        WrapperPanelEpic.DataCountPlayerGoods = EpicCountGoods;
        WrapperPanelEpic.PlayerStateCountGoods.text = EpicCountGoods.ToString();

        WrapperPanelLegendary.DataCountPlayerGoods = LegendaryCountGoods;
        WrapperPanelLegendary.PlayerStateCountGoods.text = LegendaryCountGoods.ToString();
    }
}

