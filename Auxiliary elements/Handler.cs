using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum NameProduct
{
    Driver, Truck, Trailer, WareHouseGoodS, Defolt
}
public class Handler : MonoBehaviour 
{
    public CardSave _cardSave;
    private PlayerData _playerData;
    private StartFactory<Driver> _factoryDriver;
    private StartFactory<Truck> _factoryTruck;
    private StartFactory<WareHouseGoodS> _factoryWarhouseGoodS;
    private DataForHeader _dataForHeader;
    private ManagerMainMenu managerMainMenu;

    public static Handler handler { get; set; }
    public StartFactory<Driver> FactorDriver
    {
        get { return _factoryDriver; }
        set { _factoryDriver = value; }
    }
    public StartFactory<Truck> FactoryTruck
    {
        get { return _factoryTruck; }
        set { _factoryTruck = value; }
    }
    public StartFactory<WareHouseGoodS> FactoryWarhouseGoodS
    {
        get { return _factoryWarhouseGoodS; }
        set { _factoryWarhouseGoodS = value; }
    }

    private void Start()
    {
        _dataForHeader = GetComponent<DataForHeader>();
        _playerData = PlayerData.instanse;
        _cardSave = _playerData.instanseSaveCard;
        managerMainMenu = ManagerMainMenu.instanse;
        if (handler != null) Destroy(handler);

        handler = this;

        SpavnDriver();
        SpavnTruck();
        SpavnWarehouseGood();
    }
    private void SpavnDriver()
    {
        _factoryDriver = new StartFactory<Driver>();
        _factoryDriver.PanelFilling( _cardSave.ListLoungeCardDriver, _cardSave.ListActiveCardDriver, _cardSave.ListInWalletCardDriver, _dataForHeader, managerMainMenu.UIDataPanelActiveDriver.gameObject);
    }
    private void SpavnTruck()
    {
        FactoryTruck = new StartFactory<Truck>();
        FactoryTruck.PanelFilling(_cardSave.ListGarageCardTruck, _cardSave.ListActiveCardTruck, _cardSave.ListInWalletCardTruck, _dataForHeader, managerMainMenu.UIDataPanelActiveTruck.gameObject);

    }
    private void SpavnWarehouseGood()
    {
        FactoryWarhouseGoodS = new StartFactory<WareHouseGoodS>();
        FactoryWarhouseGoodS.PanelFilling( _cardSave.ListGarageCardWareHouseGoodS, _cardSave.ListActiveCardWareHouseGoodS,
            _cardSave.ListInWalletCardWareHouseGoodS, _dataForHeader, managerMainMenu.UIDataPanelActiveWarhouseGoods.gameObject);
    }
}
public class StartFactory<T>
{
    UIDataPanel UIData;
    GridLayoutGroup GridLayoutGroupCamePanel;
    GridLayoutGroup GridLayoutGroupActivePanel;
    GridLayoutGroup GridLayoutGroupWalletPanel;
    PlayerData playerData = PlayerData.instanse;
    Sprite GameImage = null;
    GameObject ButtonPrefab = null;
    GameObject ButtonPrefabUpload = null;
    NameProduct nameProduct = NameProduct.Defolt;

    GameObject CurrentPanel;
    bool isOneStart = true;
    int CurrentSetAppPlayer;

    DataForHeader _dataForHeader; 
    private void Start(GameObject CurrentPanelToStart)
    {
        UIData = CurrentPanelToStart.GetComponent<UIDataPanel>();
        CurrentPanel = CurrentPanelToStart;
        CurrentSetAppPlayer = playerData.instanseSaveCard.CurrentSetupPlayer;

        GridLayoutGroupCamePanel = UIData.CardInGamePanel.GetComponent<GridLayoutGroup>();
        GridLayoutGroupActivePanel = UIData.ActivePanel.GetComponent<GridLayoutGroup>();
        GridLayoutGroupWalletPanel = UIData.InWalletPanel.GetComponent<GridLayoutGroup>();
    }

    public void PanelFilling<T>( List<T> DatasCardInGarage, List<T> DatasCardActive, List<T> DatasCardInWallet, DataForHeader dataForHeader, GameObject CurrentPanelToStart = null)
    {
        if (isOneStart)
        {
            Start(CurrentPanelToStart);
            isOneStart = false;
        }
        this._dataForHeader = dataForHeader;
        CreateActiveAndInGarageCard(DatasCardInGarage);//, DatasCardActive);
        CreateInWalletCard(DatasCardInWallet);
    }
    public void CreateActiveAndInGarageCard<T>(List<T> DatasCardInGarage)
    {
        for (int i = 0; i < DatasCardInGarage.Count; i++)
        {
            GameObject CreateCard = new GameObject(name: "GreateGameObject" + i + nameProduct);
            CreateCard.transform.position = Vector3.zero;
            CreateCard.AddComponent<LinkCurrentCard>();

            GameObject gameObjectWhichWaitingToDrawn = null;

            if (DatasCardInGarage is List<Driver> DriverData)
            {
                AddedComponent(DriverData[i].Icon, _dataForHeader.ButtonSetActive, NameProduct.Driver);

                var LinkCreateCard = CreateCard.GetComponent<LinkCurrentCard>();
                LinkCreateCard._dataCurrentCardDriver.CurrentDataCard = DriverData[i];
                LinkCreateCard.IndexCard = UnityEngine.Random.Range(0, 10000);
                //Add New Logic Spavn
                if (DriverData[i].IsActive == true && DriverData[i].CurrentSetup == playerData.instanseSaveCard.CurrentSetupPlayer)
                {
                    gameObjectWhichWaitingToDrawn = AddedCreatePlayer<Driver>(gameObjectWhichWaitingToDrawn, NameProduct.Driver, playerData.instanseSaveCard.ListActiveCardDriver);
                    gameObjectWhichWaitingToDrawn.GetComponent<LinkCurrentCard>()._dataCurrentCardDriver.CurrentDataCard = DriverData[i];
                    gameObjectWhichWaitingToDrawn.GetComponent<LinkCurrentCard>()._dataCurrentCardDriver.CurrentDataCard.indexCard = LinkCreateCard.IndexCard;

                    CreatePanelInGarage(CurrentPanel, gameObjectWhichWaitingToDrawn, DriverData[i].CurrentSetup);
                    DriverData[i].indexCard = LinkCreateCard.IndexCard;
                    SwitchingBetweenStates.EventOnAction?.Invoke(CurrentPanel, gameObjectWhichWaitingToDrawn, false);
                }
                else
                {
                    CreatePanelInGarage(CurrentPanel, CreateCard);
                }
            }
            else if (DatasCardInGarage is List<Truck> TruckData)
            {
                AddedComponent(TruckData[i].Icon, _dataForHeader.ButtonSetActive, NameProduct.Truck);

                var LinkCreateCard = CreateCard.GetComponent<LinkCurrentCard>();
                LinkCreateCard._dataCurrentCardTruck.CurrentDataCard = TruckData[i];
                LinkCreateCard.IndexCard = UnityEngine.Random.Range(0, 10000);
                //Add New Logic Spavn
                if (TruckData[i].IsActive == true && TruckData[i].CurrentSetup == playerData.instanseSaveCard.CurrentSetupPlayer)
                {
                    gameObjectWhichWaitingToDrawn = AddedCreatePlayer<Truck>(gameObjectWhichWaitingToDrawn, NameProduct.Truck, playerData.instanseSaveCard.ListActiveCardTruck);
                    gameObjectWhichWaitingToDrawn.GetComponent<LinkCurrentCard>()._dataCurrentCardTruck.CurrentDataCard = TruckData[i];
                    gameObjectWhichWaitingToDrawn.GetComponent<LinkCurrentCard>()._dataCurrentCardTruck.CurrentDataCard.indexCard = LinkCreateCard.IndexCard;

                    CreatePanelInGarage(CurrentPanel, gameObjectWhichWaitingToDrawn, TruckData[i].CurrentSetup);
                    TruckData[i].indexCard = LinkCreateCard.IndexCard;
                    SwitchingBetweenStates.EventOnAction?.Invoke(CurrentPanel, gameObjectWhichWaitingToDrawn, false);
                }
                else
                {
                    CreatePanelInGarage(CurrentPanel, CreateCard);
                }
            }
            else if (DatasCardInGarage is List<WareHouseGoodS> WareHouseGoodSData)
            {
                AddedComponent(WareHouseGoodSData[i].Icon,_dataForHeader.ButtonSetActive, NameProduct.WareHouseGoodS);

                var LinkCreateCard = CreateCard.GetComponent<LinkCurrentCard>();
                LinkCreateCard._dataCurrentCardWareHouseGoodS.CurrentDataCard = WareHouseGoodSData[i];
                LinkCreateCard.IndexCard = UnityEngine.Random.Range(0, 10000);
                //Add New Logic Spavn
                
                    gameObjectWhichWaitingToDrawn = AddedCreatePlayer<WareHouseGoodS>(gameObjectWhichWaitingToDrawn, NameProduct.WareHouseGoodS, playerData.instanseSaveCard.ListActiveCardWareHouseGoodS);
                    gameObjectWhichWaitingToDrawn.GetComponent<LinkCurrentCard>()._dataCurrentCardWareHouseGoodS.CurrentDataCard = WareHouseGoodSData[i];
                    gameObjectWhichWaitingToDrawn.GetComponent<LinkCurrentCard>()._dataCurrentCardWareHouseGoodS.CurrentDataCard.indexCard = LinkCreateCard.IndexCard;

                    CreatePanelInGarage(CurrentPanel, gameObjectWhichWaitingToDrawn, 0);
                    WareHouseGoodSData[i].indexCard = LinkCreateCard.IndexCard;
                    SwitchingBetweenStates.EventOnAction?.Invoke(CurrentPanel, gameObjectWhichWaitingToDrawn, false);
            }

        }
    }
    private void AddedComponent(Sprite GameImage, GameObject ButtonPrefab, NameProduct nameProduct)
    {
        this.GameImage = GameImage;
        this.ButtonPrefab = ButtonPrefab;
        this.nameProduct = nameProduct;
    }
    private GameObject AddedCreatePlayer<T>(GameObject gameObjectWhichWaitingToDrawn, NameProduct nameProduct, List<T> DataCard)
    {
        gameObjectWhichWaitingToDrawn = new GameObject(name: "GreateGameObject" + nameProduct);
        gameObjectWhichWaitingToDrawn.transform.position = new Vector3(0,0,100);
        gameObjectWhichWaitingToDrawn.AddComponent<LinkCurrentCard>();
        gameObjectWhichWaitingToDrawn.GetComponent<LinkCurrentCard>().NameProduct = nameProduct;
        if (DataCard.Count != 0)
        {
            DataCard.RemoveAt(0);
        }
        return gameObjectWhichWaitingToDrawn;
    }
    public void CreateInWalletCard<T>(List<T> DatasCardInWallet)
    {

        for (int i = 0; i < DatasCardInWallet.Count; i++)
        {
            GameObject CreateGameObjectCard = new GameObject(name: "GreateGameObject" + i + nameProduct);
            ButtonPrefab = _dataForHeader.ButonUpload;
            if (DatasCardInWallet is List<Driver> DriverData)
            {
                GameImage = DriverData[i].Icon;
 
                CreateGameObjectCard.AddComponent<LinkCurrentCard>();
                CreateGameObjectCard.GetComponent<LinkCurrentCard>()._dataCurrentCardDriver.CurrentDataCard = DriverData[i];
            }
            else if (DatasCardInWallet is List<Truck> TruckData)
            {
                GameImage = TruckData[i].Icon;

                CreateGameObjectCard.AddComponent<LinkCurrentCard>();
                CreateGameObjectCard.GetComponent<LinkCurrentCard>()._dataCurrentCardTruck.CurrentDataCard = TruckData[i];
            }

            else if (DatasCardInWallet is List<WareHouseGoodS> WareHouseGoodSData)
            {
                GameImage = WareHouseGoodSData[i].Icon;

                LinkCurrentCard linkCurrentCard = CreateGameObjectCard.AddComponent<LinkCurrentCard>();
                linkCurrentCard._dataCurrentCardWareHouseGoodS.CurrentDataCard = WareHouseGoodSData[i];
                linkCurrentCard.NameProduct = NameProduct.WareHouseGoodS;
            }

            CreatePanelInWallet(CurrentPanel, CreateGameObjectCard);
        }

    }

    private void CreatePanelInWallet(GameObject CurrentPanel, GameObject CreateGameObjectCard, int CurrentSetApp = 0) // передавать в этот метод активный сетапп игрока.
    {
        GameObject ButtonS = AbstractCreate(CurrentPanel, CreateGameObjectCard, GridLayoutGroupWalletPanel);
        ButtonS.GetComponent<Button>().onClick.AddListener(() => SwitchingBetweenStates.EventInlounge?.Invoke(CurrentPanel, CreateGameObjectCard));
        //Add Test

        AddComponentToCard.AddComponentText(CreateGameObjectCard, CurrentSetApp);

    }
    private void CreatePanelInGarage(GameObject CurrentPanel, GameObject CreateGameObjectCard, int CurrentSetApp = 0)
    {
        GameObject ButtonS = AbstractCreate(CurrentPanel, CreateGameObjectCard, GridLayoutGroupCamePanel);
        ButtonS.GetComponent<Button>()?.onClick.AddListener(() => SwitchingBetweenStates.EventOnAction(CurrentPanel, CreateGameObjectCard, true));
        //Add Test
        AddComponentToCard.AddComponentText(CreateGameObjectCard, CurrentSetApp);

    }
    private GameObject AbstractCreate(GameObject CurrentPanel, GameObject CreateGameObjectCard, GridLayoutGroup GridLayoutGroupPanel)
    {
        GameObject CreatePrefabButon = GameObject.Instantiate(ButtonPrefab, Vector3.zero, Quaternion.identity, CreateGameObjectCard.transform);
        //CreatePrefabButon.transform.SetParent(CreateGameObjectCard.transform);
        Assets.Code.StaticClass.TransferPos.PositionZeroCoordinste(CreatePrefabButon);

        LinkCurrentCard linkCurrentCard = CreateGameObjectCard.GetComponent<LinkCurrentCard>();

        CreateGameObjectCard.GetComponent<LinkCurrentCard>().NameProduct = nameProduct;
        RectTransform RectTransform = CreatePrefabButon.GetComponent<RectTransform>();
        RectTransform.sizeDelta = new Vector2(200, 60);
        RectTransform.anchoredPosition = new Vector2(0, -179.24f);
        Button Button = CreatePrefabButon.GetComponent<Button>();

        CreateGameObjectCard.GetComponent<LinkCurrentCard>().SetActiveButton = CreatePrefabButon.GetComponent<Button>();
        CreateGameObjectCard.AddComponent<BoxCollider2D>().size = GridLayoutGroupPanel.cellSize;
        CreateGameObjectCard.transform.SetParent(GridLayoutGroupPanel.transform, true);
        CreateGameObjectCard.AddComponent<Image>().sprite = GameImage;
        return CreatePrefabButon;
    }
}

public static class AddComponentToCard 
{
    public static void AddComponentText(GameObject CreateCard, int CurrentSetApp, float Offset = 70f)
    {
        var TextActiveCard = GameObject.Instantiate(PlayerData.instanse.CurrentActiveCard, Vector3.zero, Quaternion.identity, CreateCard.transform);

        // TextActiveCard.transform.SetParent(CreateCard.transform, true);
        CreateCard.GetComponent<LinkCurrentCard>().CurrentActiveSetupGameObjectText = TextActiveCard;

        CreateCard.transform.localScale = Vector3.one;
        TextActiveCard.transform.localScale = Vector3.one;

        var TextCurrentSetApp = GameObject.Instantiate(PlayerData.instanse.CurrentActiveSetAppText, Vector3.zero, Quaternion.identity, CreateCard.transform);

        //TextCurrentSetApp.transform.SetParent(CreateCard.transform, true);
        CreateCard.GetComponent<LinkCurrentCard>().CurrentSetupText = TextCurrentSetApp;

        TextCurrentSetApp.transform.localScale = Vector3.one;
        var a = TextCurrentSetApp.GetComponent<RectTransform>();
        a.anchoredPosition = new Vector2(0, Offset);

        if (CurrentSetApp > 0)
        {
            TextCurrentSetApp.GetComponent<Text>().text = $"Setup {CurrentSetApp}";
        }
        else
        {
            TextCurrentSetApp.GetComponent<Text>().text = "";
        }
        Assets.Code.StaticClass.TransferPos.PositionZeroCoordinste(TextActiveCard);
    }

}
