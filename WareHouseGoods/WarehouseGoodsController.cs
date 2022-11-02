using Assets.Code.Map;
using Assets.Code.WareHouseGoods;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WarehouseGoodsController : MonoBehaviour
{
    public static WarehouseGoodsController instanse;
    public static Action<GameObject, GameObject, GameObject> EventBroadcastCurrentActiivePlayer { get; set; }
    public static Action EventResetGoods { get; set; }
    [SerializeField] private UIDataPanel uIDataPanel;
    [SerializeField] private Text TextRare;
    [SerializeField] private Text Core;
    private WareHouseGoodS wareHouseGoodS;
    PlayerData playerData;
    private LinkCurrentCard LinkCurrentCard;
    GameObject CardOrigin;
    GameObject CurrentPanel;
    GameObject CardClone;

    [SerializeField] private GameObject[] GameObjects = new GameObject[4]; //CurrentImage Resourse And Button Warhouse
    private LinkCurrentCard linkCurrentCard;

    private CurrentProduce _currentProduce;
    
    [field: SerializeField] public Button ShowWarhouseGoodsToMap { get; set; }
    private void Awake()
    {
        if (instanse != null)
        {
            Destroy(instanse);
        }
        instanse = this;

        EventBroadcastCurrentActiivePlayer += UpdateCard;
        ShowWarhouseGoodsToMap.onClick.AddListener(() => ControllerMap.instanse.OpenMapAndSowMyLand?.Invoke());
        EventResetGoods += ResetGoods;
        _currentProduce = GetComponent<CurrentProduce>();

    }
    private void OnDestroy()
    {
        EventBroadcastCurrentActiivePlayer -= UpdateCard;
        EventResetGoods -= ResetGoods;
        ShowWarhouseGoodsToMap.onClick.RemoveListener(() => ControllerMap.instanse.OpenMapAndSowMyLand?.Invoke());

    }
    private void Start()
    {
        if (Core.text == "")
        {
            ShowWarhouseGoodsToMap.gameObject.SetActive(false);
        }
       
        StartCoroutine(StartInizialization());
    }
    private void OnEnable()
    {
        ControllerMap.UpdateToWarhousePoint?.Invoke();
        if (Core.text != "")
        {
            ShowWarhouseGoodsToMap.gameObject.SetActive(true);

        }
    }
    private void UpdateCard(GameObject PanelPlayer, GameObject CardOrigin, GameObject CardClone)
    {
        TextRare.text = " ";
        Core.text = "";
        this.CardOrigin = CardOrigin;
        CurrentPanel = PanelPlayer;
        this.CardClone = CardClone;
        LinkCurrentCard = CardOrigin.GetComponent<LinkCurrentCard>();
        PlayerData playerData = PlayerData.instanse;
        IninzializatoinUpdate();
        DisplayCurrentlyAllowedProducts();
        _currentProduce.ListTimerUpdateDay.RemoveRange(0, _currentProduce.ListTimerUpdateDay.Count);

        ShowWarhouseGoodsToMap.gameObject.SetActive(true);

    }
    private void DisplayCurrentlyAllowedProducts()
    {
        if (linkCurrentCard == null)
         return;
        
        switch (uIDataPanel.ActivePanel.GetComponent<GridLayoutGroup>().GetComponentInChildren<LinkCurrentCard>()._dataCurrentCardWareHouseGoodS.CurrentDataCard.Rarity)
        {
            case Rarity.Common:

                for (int i = 0; i < GameObjects.Length; i++)
                {
                    
                    if (GameObjects[i].GetComponent<RarityObjectInizializationWarhouseGoods>().CurrentRarity == Rarity.Common)
                    {
                        GameObjects[0].gameObject.SetActive(true);
                    }
                    else
                    {
                        GameObjects[i].gameObject.SetActive(false);
                    }
                }
                break;
            case Rarity.Rare:

                for (int i = 0; i < GameObjects.Length; i++)
                {
                    if (GameObjects[i].GetComponent<RarityObjectInizializationWarhouseGoods>().CurrentRarity > Rarity.Rare)
                    {
                        GameObjects[i].gameObject.SetActive(false);
                    }
                    else
                    {
                        GameObjects[i].gameObject.SetActive(true);
                        GameObjects[i].GetComponent<RarityObjectInizializationWarhouseGoods>().RarityWareHouse = Rarity.Rare;
                    }
                }

                break;
            case Rarity.Epic:

                for (int i = 0; i < GameObjects.Length; i++)
                {
                    if (GameObjects[i].GetComponent<RarityObjectInizializationWarhouseGoods>().CurrentRarity > Rarity.Epic)
                    {
                        GameObjects[i].gameObject.SetActive(false);
                    }
                    else
                    {
                        GameObjects[i].gameObject.SetActive(true);
                        GameObjects[i].GetComponent<RarityObjectInizializationWarhouseGoods>().RarityWareHouse = Rarity.Epic;
                    }
                }

                break;
            case Rarity.Legendary:
                for (int i = 0; i < GameObjects.Length; i++)
                {
                    GameObjects[i].gameObject.SetActive(true);
                    GameObjects[i].GetComponent<RarityObjectInizializationWarhouseGoods>().RarityWareHouse = Rarity.Legendary;
                }

                break;
        }
        Debug.Log(linkCurrentCard._dataCurrentCardWareHouseGoodS.CurrentDataCard.Rarity);


    }
    private void ResetGoods() //Добавить обнуление текста
    {
        for (int i = 0; i < GameObjects.Length; i++)
        {
            GameObjects[i].gameObject.SetActive(false);
        }

        ShowWarhouseGoodsToMap.gameObject.SetActive(false);

        TextRare.text = " ";
        Core.text = "";
    }
    private void IninzializatoinUpdate()
    {
        linkCurrentCard  = uIDataPanel.ActivePanel.GetComponent<GridLayoutGroup>().GetComponentInChildren<LinkCurrentCard>();

        TextRare.text = linkCurrentCard._dataCurrentCardWareHouseGoodS.CurrentDataCard.Rarity.ToString();
        Core.text = linkCurrentCard._dataCurrentCardWareHouseGoodS.CurrentDataCard.Core;
    }
    
    IEnumerator StartInizialization()
    {
        yield return new WaitForSeconds(0.2f);
        if (uIDataPanel.ActivePanel.GetComponent<GridLayoutGroup>().GetComponentInChildren<LinkCurrentCard>() != null)
        {
            IninzializatoinUpdate();
            DisplayCurrentlyAllowedProducts();
        }
    }
}
