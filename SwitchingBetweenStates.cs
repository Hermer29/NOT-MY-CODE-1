using System;
using UnityEngine;
using UnityEngine.UI;

public class SwitchingBetweenStates : MonoBehaviour //TODO NEW event CheckOnActive;
{
    public static Action<GameObject, GameObject, bool> EventOnAction { get; set; }
    public static Action<GameObject, GameObject, GameObject> EventInWallet { get; set; }
    public static Action<GameObject, GameObject> EventInlounge { get; set; }
    public DataForHeader _dataForHeader;
    private void Start()
    {
        EventOnAction += OnAction;
        EventInWallet += MovmentInWallet;
        EventInlounge += MovmentInLounge;
        _dataForHeader = GetComponent<DataForHeader>();
    }
    private void OnDestroy()
    {
        EventOnAction -= OnAction;
        EventInWallet -= MovmentInWallet;
        EventInlounge -= MovmentInLounge;
    }
    private bool CheckToActiveCard(LinkCurrentCard linkCurrentCard, bool isClickCardToPlayer)
    {
        PlayerData playerData = PlayerData.instanse;
        int SwitchPlayerActive = 0;


        switch (linkCurrentCard.NameProduct) 
        {
            case NameProduct.Driver:
                SwitchPlayerActive = playerData.instanseSaveCard.ListActiveCardDriver.Count;
                break;
            case NameProduct.Truck:
                SwitchPlayerActive = playerData.instanseSaveCard.ListActiveCardTruck.Count;
                break;
        }
        if (isClickCardToPlayer == true && SwitchPlayerActive > 0)
        {
            int Travel = 0;
            int ActiveSetApp = 0;
            switch (linkCurrentCard.NameProduct)
            {
                case NameProduct.Driver:
                    Travel = linkCurrentCard._dataCurrentCardDriver.CurrentDataCard.Travel;
                    ActiveSetApp = linkCurrentCard._dataCurrentCardDriver.CurrentDataCard.CurrentSetup;
                    break;
                case NameProduct.Truck:

                    Travel = linkCurrentCard._dataCurrentCardTruck.CurrentDataCard.Travel;
                    ActiveSetApp = linkCurrentCard._dataCurrentCardTruck.CurrentDataCard.CurrentSetup;
                    break;
            }

            if (Travel > 0 || ActiveSetApp != 0)
            {
                Debug.Log("Currrent Card To Active Setup!!!");
                return true; 
            }
        }
        if (SwitchPlayerActive > 0)
        {
            Debug.Log("Уже есть активная карта!!!");
            return true;
        }
        if (linkCurrentCard.NameProduct == NameProduct.WareHouseGoodS)
        {
            if (PlayerData.instanse.instanseSaveCard.ListActiveCardWareHouseGoodS.Count != 0)
            {
                return true;
            }
        }
        return false;
    }
    public void OnAction(GameObject GamePanel, GameObject CurentCameObj, bool isClickCardToPlayer)
    {
        PlayerData playerData = PlayerData.instanse;


        LinkCurrentCard linkCurrentCard = CurentCameObj.GetComponent<LinkCurrentCard>();

        bool isActiveSetApp = CheckToActiveCard(linkCurrentCard, isClickCardToPlayer);

        if (isActiveSetApp == true)
        {
            return;
        }

        Sprite iconcCard = null;

        GridLayoutGroup ActionPanel = GamePanel.GetComponent<UIDataPanel>().ActivePanel.GetComponent<GridLayoutGroup>();

        GameObject CreateInstanseCardGameObj = new GameObject(linkCurrentCard.gameObject.name);
        Debug.Log(CreateInstanseCardGameObj.transform.position.z);
        CreateInstanseCardGameObj.AddComponent<ClickCard>();
        var ClickCard = CreateInstanseCardGameObj.GetComponent<ClickCard>();
        ClickCard.ClonCard = CurentCameObj;
        ClickCard.ClonCard = CurentCameObj;
        ClickCard.OriginCard = CurentCameObj;

        Debug.Log(CreateInstanseCardGameObj.transform.position.z);
        CreateInstanseCardGameObj.AddComponent<LinkCurrentCard>().ActiveSetApp = PlayerData.instanse.instanseSaveCard.CurrentSetupPlayer;

        CreateInstanseCardGameObj.transform.SetParent(ActionPanel.transform, true);

        Button CurrentButtonSetActive = CurentCameObj.GetComponentInChildren<Button>();
        CurrentButtonSetActive.onClick.RemoveListener(() => SwitchingBetweenStates.EventOnAction(GamePanel, CurentCameObj, true));

        CreateInstanseCardGameObj.AddComponent<BoxCollider2D>().size = ActionPanel.cellSize;

        CreateInstanseCardGameObj.transform.localScale =Vector3.one;

        LinkCurrentCard linkCreateCard = CreateInstanseCardGameObj.GetComponent<LinkCurrentCard>();

        switch (linkCurrentCard.NameProduct)
        {
            case NameProduct.Driver:
                iconcCard = linkCurrentCard._dataCurrentCardDriver.CurrentDataCard.Icon;

                linkCreateCard._dataCurrentCardDriver = linkCurrentCard._dataCurrentCardDriver;
                linkCreateCard.NameProduct = linkCurrentCard.NameProduct;
                linkCreateCard.IndexCard = linkCurrentCard._dataCurrentCardDriver.CurrentDataCard.indexCard;
                linkCreateCard._dataCurrentCardDriver.CurrentDataCard.indexCard = CurentCameObj.GetComponent<LinkCurrentCard>()._dataCurrentCardDriver.CurrentDataCard.indexCard;

                linkCreateCard._dataCurrentCardDriver.CurrentDataCard.CurrentSetup = playerData.instanseSaveCard.CurrentSetupPlayer; //add
                CurentCameObj.GetComponent<LinkCurrentCard>().ActiveSetApp = playerData.instanseSaveCard.CurrentSetupPlayer; //add
                CurentCameObj.GetComponent<LinkCurrentCard>().CurrentSetupText.GetComponent<Text>().text = "Setup " + playerData.instanseSaveCard.CurrentSetupPlayer; //add

                linkCreateCard.CurrentActiveSetupGameObjectText = CurentCameObj.GetComponent<LinkCurrentCard>().CurrentActiveSetupGameObjectText;

                
                playerData.instanseSaveCard.ListActiveCardDriver.Add(CreateInstanseCardGameObj.GetComponent<LinkCurrentCard>()._dataCurrentCardDriver.CurrentDataCard);

                DriverPanelController.EventBroadcastCurrentActiivePlayer?.Invoke(GamePanel, CurentCameObj, CreateInstanseCardGameObj);

                //Added 
                linkCreateCard._dataCurrentCardDriver.CurrentDataCard.IsActive = true;

                

                break;
            case NameProduct.Truck:
                iconcCard = linkCurrentCard._dataCurrentCardTruck.CurrentDataCard.Icon;
                linkCreateCard._dataCurrentCardTruck = linkCurrentCard._dataCurrentCardTruck;
                linkCreateCard.NameProduct = linkCurrentCard.NameProduct;
                linkCreateCard.IndexCard = linkCurrentCard._dataCurrentCardTruck.CurrentDataCard.indexCard;

                linkCreateCard._dataCurrentCardTruck.CurrentDataCard.indexCard = CurentCameObj.GetComponent<LinkCurrentCard>()._dataCurrentCardTruck.CurrentDataCard.indexCard;
                linkCreateCard.CurrentActiveSetupGameObjectText = CurentCameObj.GetComponent<LinkCurrentCard>().CurrentActiveSetupGameObjectText;


                linkCurrentCard._dataCurrentCardTruck.CurrentDataCard.CurrentSetup = playerData.instanseSaveCard.CurrentSetupPlayer; //add
                linkCurrentCard.CurrentSetupText.GetComponent<Text>().text = "Setup " + playerData.instanseSaveCard.CurrentSetupPlayer; //add
                linkCurrentCard._dataCurrentCardTruck.CurrentDataCard.IsActive = true;//Add

                playerData.instanseSaveCard.ListActiveCardTruck.Add(CreateInstanseCardGameObj.GetComponent<LinkCurrentCard>()._dataCurrentCardTruck.CurrentDataCard);

                TruckPanelController.EventBroadcastCurrentActiivePlayer?.Invoke(GamePanel, CurentCameObj, CreateInstanseCardGameObj);

                TruckPanelController.instanse.StartSwitchingInfoCard(true);

                break;
            case NameProduct.WareHouseGoodS:
                iconcCard = linkCurrentCard._dataCurrentCardWareHouseGoodS.CurrentDataCard.Icon;

                linkCreateCard._dataCurrentCardWareHouseGoodS = linkCurrentCard._dataCurrentCardWareHouseGoodS;
                linkCreateCard.NameProduct = linkCurrentCard.NameProduct;
                linkCreateCard.IndexCard = linkCurrentCard._dataCurrentCardWareHouseGoodS.CurrentDataCard.indexCard;

                linkCreateCard._dataCurrentCardWareHouseGoodS.CurrentDataCard.indexCard = CurentCameObj.GetComponent<LinkCurrentCard>()._dataCurrentCardWareHouseGoodS.CurrentDataCard.indexCard;
                linkCreateCard.CurrentActiveSetupGameObjectText = CurentCameObj.GetComponent<LinkCurrentCard>().CurrentActiveSetupGameObjectText;

                playerData.instanseSaveCard.ListActiveCardWareHouseGoodS.Add(CreateInstanseCardGameObj.GetComponent<LinkCurrentCard>()._dataCurrentCardWareHouseGoodS.CurrentDataCard);
                WarehouseGoodsController.EventBroadcastCurrentActiivePlayer?.Invoke(GamePanel, CurentCameObj, CreateInstanseCardGameObj);

                CurentCameObj.GetComponent<LinkCurrentCard>().CurrentSetupText.GetComponent<Text>().text = "Setup " + playerData.instanseSaveCard.CurrentSetupPlayer; //add
                linkCurrentCard._dataCurrentCardWareHouseGoodS.CurrentDataCard.IsActive = true;
                linkCurrentCard.isActive = true;



                break;
            case NameProduct.Defolt:
                break;
        }
        CurentCameObj.GetComponent<LinkCurrentCard>().CurrentSetupText.gameObject.SetActive(true);
        CreateInstanseCardGameObj.AddComponent<Image>().sprite = iconcCard;
        InizializatorMainMenu.EventUpdateActiveCardToMainMenu?.Invoke(GamePanel, CreateInstanseCardGameObj); // Пока отправляю клон объекта

        Assets.Code.StaticClass.TransferPos.PositionZeroCoordinste(CreateInstanseCardGameObj);

    }

    private void MovmentInWallet(GameObject GamePanel, GameObject CurentCameObj, GameObject CardCloneOnActive)
    {
        if (CardCloneOnActive == null )
        {
            return;
        }

        GridLayoutGroup InWalletPanel = GamePanel.GetComponent<UIDataPanel>().InWalletPanel.GetComponent<GridLayoutGroup>();
        GridLayoutGroup InActivePanel = GamePanel.GetComponent<UIDataPanel>().ActivePanel.GetComponent<GridLayoutGroup>();
        LinkCurrentCard linkCurrentCard = CurentCameObj.GetComponent<LinkCurrentCard>();

        if (linkCurrentCard._dataCurrentCardDriver?.CurrentDataCard?.Icon != null)
        {
            if (linkCurrentCard._dataCurrentCardDriver.CurrentDataCard.CurrentEnergy == linkCurrentCard._dataCurrentCardDriver.CurrentDataCard.MaxEnergy 
                && linkCurrentCard._dataCurrentCardDriver.CurrentDataCard.CurrentHunger == linkCurrentCard._dataCurrentCardDriver.CurrentDataCard.MaxHunger
                && linkCurrentCard._dataCurrentCardDriver.CurrentDataCard.Travel ==0 )
            {
                CurentCameObj.GetComponent<LinkCurrentCard>().CurrentSetupText.GetComponent<Text>().text = ""; 
                UIDataPanel uIDataPanel = GamePanel.GetComponent<UIDataPanel>();
                LinkCurrentCard[] allLinkCurrentCard  = uIDataPanel.GetComponentsInChildren<LinkCurrentCard>();

                PlayerData playerData = PlayerData.instanse;

                for (int i = 1; i < allLinkCurrentCard.Length; i++)
                {
                    if(allLinkCurrentCard[i].gameObject.name == linkCurrentCard.gameObject.name)
                    {
                        allLinkCurrentCard[i].gameObject.transform.SetParent(InWalletPanel.transform, true);

                        allLinkCurrentCard[i].CurrentActiveSetupGameObjectText.gameObject.GetComponent<Text>().text = "";
                        playerData.instanseSaveCard.ListInWalletCardDriver.Add(allLinkCurrentCard[i]._dataCurrentCardDriver.CurrentDataCard);
                        playerData.instanseSaveCard.ListLoungeCardDriver.Remove(allLinkCurrentCard[i]._dataCurrentCardDriver.CurrentDataCard);

                        playerData.instanseSaveCard.ListActiveCardDriver.Remove(linkCurrentCard._dataCurrentCardDriver.CurrentDataCard);
                        CardCloneOnActive.SetActive(false);

                        GameObject CreateButtonSetActive = Instantiate(_dataForHeader.ButonUpload, Vector3.zero, Quaternion.identity, linkCurrentCard.transform);

                        Assets.Code.StaticClass.TransferPos.PositionZeroCoordinste(CreateButtonSetActive);

                        //CreateButtonSetActive.transform.SetParent(linkCurrentCard.transform);
                        RectTransform ButonSetActiveRect = CreateButtonSetActive.GetComponent<RectTransform>();
                        Assets.Code.StaticClass.TransferPos.PositionZeroCoordinste(CreateButtonSetActive);
                        ButonSetActiveRect.anchoredPosition = new Vector2(0, -179.24f);
                        ButonSetActiveRect.transform.localScale = Vector3.one;
                        ButonSetActiveRect.sizeDelta = new Vector2(200, 60);

                        DriverPanelController.ResetEventValuePlayer();

                       
                        CreateButtonSetActive.GetComponent<Button>().onClick.AddListener(() => SwitchingBetweenStates.EventInlounge?.Invoke(GamePanel, CurentCameObj));


                        //Added 
                        allLinkCurrentCard[i]._dataCurrentCardDriver.CurrentDataCard.IsActive = false; 

                        break;

                    }

                }
            }
        }
        if (linkCurrentCard._dataCurrentCardTruck?.CurrentDataCard?.Icon != null)
        {
            if (linkCurrentCard._dataCurrentCardTruck.CurrentDataCard.CurrentFuel == linkCurrentCard._dataCurrentCardTruck.CurrentDataCard.MaxFuel
                && linkCurrentCard._dataCurrentCardTruck.CurrentDataCard.CurrentParts == linkCurrentCard._dataCurrentCardTruck.CurrentDataCard.MaxParts
               &&  linkCurrentCard._dataCurrentCardTruck.CurrentDataCard.Travel == 0 )
            {
                UIDataPanel uIDataPanel = GamePanel.GetComponent<UIDataPanel>();
                LinkCurrentCard[] allLinkCurrentCard = uIDataPanel.GetComponentsInChildren<LinkCurrentCard>();

                var a = Array.IndexOf(allLinkCurrentCard, linkCurrentCard);
                Debug.Log("Current; " + a);
                PlayerData playerData = PlayerData.instanse;

                for (int i = 0; i < allLinkCurrentCard.Length; i++)
                {
                    if (allLinkCurrentCard[i].gameObject.name == linkCurrentCard.gameObject.name)
                    {

                        linkCurrentCard.gameObject.transform.SetParent(InWalletPanel.transform, true);

                        allLinkCurrentCard[i].CurrentActiveSetupGameObjectText.gameObject.GetComponent<Text>().text = "";

                        playerData.instanseSaveCard.ListInWalletCardTruck.Add(allLinkCurrentCard[i]._dataCurrentCardTruck.CurrentDataCard);
                        playerData.instanseSaveCard.ListGarageCardTruck.Remove(allLinkCurrentCard[i]._dataCurrentCardTruck.CurrentDataCard);

                        playerData.instanseSaveCard.ListActiveCardTruck.Remove(linkCurrentCard._dataCurrentCardTruck.CurrentDataCard);
                        CardCloneOnActive.SetActive(false);
                        allLinkCurrentCard[i].gameObject.SetActive(false);

                        GameObject CreateButtonSetActive = Instantiate(_dataForHeader.ButonUpload, Vector3.zero, Quaternion.identity, linkCurrentCard.transform);

                        Assets.Code.StaticClass.TransferPos.PositionZeroCoordinste(CreateButtonSetActive);

                        //CreateButtonSetActive.transform.SetParent(linkCurrentCard.transform);
                        RectTransform ButonSetActiveRect = CreateButtonSetActive.GetComponent<RectTransform>();
                        ButonSetActiveRect.anchoredPosition = new Vector2(0, -179.24f);
                        ButonSetActiveRect.transform.localScale = Vector3.one;
                        ButonSetActiveRect.sizeDelta = new Vector2(200, 60);

                        TruckPanelController.ResetEventValuePlayer();
                        CreateButtonSetActive.GetComponent<Button>().onClick.AddListener(() => SwitchingBetweenStates.EventInlounge?.Invoke(GamePanel, CurentCameObj));
                        Debug.Log("Current после захода; " + i);
                        break;

                    }

                }
            }
        }
    }
    private void MovmentInLounge(GameObject GamePanel, GameObject CurentCameObj)
    {
        GameObject CreateButtonSetActive = null;
        PlayerData playerData = PlayerData.instanse;
        GridLayoutGroup inLoungePanel = GamePanel.GetComponent<UIDataPanel>().CardInGamePanel.GetComponent<GridLayoutGroup>();
        LinkCurrentCard linkCurrentCard = CurentCameObj.GetComponent<LinkCurrentCard>();

        linkCurrentCard.gameObject.transform.SetParent(inLoungePanel.transform, true);

        Button ButtonCard = CurentCameObj.GetComponentInChildren<Button>();
        ButtonCard.onClick.RemoveAllListeners();
        ButtonCard.gameObject.SetActive(false);


        switch (linkCurrentCard.NameProduct)
        {
            case NameProduct.Driver:

                playerData.instanseSaveCard.ListInWalletCardDriver.Remove(linkCurrentCard._dataCurrentCardDriver.CurrentDataCard);
                playerData.instanseSaveCard.ListLoungeCardDriver.Add(linkCurrentCard._dataCurrentCardDriver.CurrentDataCard);

                CreateButtonSetActive = Instantiate(_dataForHeader.ButtonSetActive, Vector3.zero, Quaternion.identity);

               
                break;
            case NameProduct.Truck:

                playerData.instanseSaveCard.ListInWalletCardTruck.Remove(linkCurrentCard._dataCurrentCardTruck.CurrentDataCard);
                playerData.instanseSaveCard.ListGarageCardTruck.Add(linkCurrentCard._dataCurrentCardTruck.CurrentDataCard);

                CreateButtonSetActive = Instantiate(_dataForHeader.ButtonSetActive, Vector3.zero, Quaternion.identity);

             
                break;
            case NameProduct.WareHouseGoodS:

                playerData.instanseSaveCard.ListInWalletCardWareHouseGoodS.Remove(linkCurrentCard._dataCurrentCardWareHouseGoodS.CurrentDataCard);
                playerData.instanseSaveCard.ListGarageCardWareHouseGoodS.Add(linkCurrentCard._dataCurrentCardWareHouseGoodS.CurrentDataCard);

                CreateButtonSetActive = Instantiate(_dataForHeader.ButtonSetActive, Vector3.zero, Quaternion.identity);

                break;
        
        }

        CreateButtonSetActive.transform.SetParent(linkCurrentCard.transform);
        RectTransform ButonSetActiveRect = CreateButtonSetActive.GetComponent<RectTransform>();
        ButonSetActiveRect.anchoredPosition = new Vector2(0, -179.24f);
        ButonSetActiveRect.transform.localScale = Vector3.one;
        ButonSetActiveRect.sizeDelta = new Vector2(200, 60);

        CreateButtonSetActive.GetComponent<Button>()?.onClick.AddListener(() => SwitchingBetweenStates.EventOnAction(GamePanel, CurentCameObj, true));

        linkCurrentCard.SetActiveButton.onClick.RemoveAllListeners();
        Destroy(linkCurrentCard.SetActiveButton.gameObject);
        linkCurrentCard.SetActiveButton = CreateButtonSetActive.GetComponent<Button>();

        Assets.Code.StaticClass.TransferPos.PositionZeroCoordinste(CreateButtonSetActive);

    }
  
}

