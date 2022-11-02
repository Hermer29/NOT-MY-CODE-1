using AbstractClass.Panel;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TruckPanelController : MonoBehaviour, IPanelController
{
    public static Action<GameObject, GameObject, GameObject> EventBroadcastCurrentActiivePlayer { get; set; }
    public static Action ResetEventValuePlayer { get; set; }
    [field: SerializeField] public PanelReplenishmentOfResources PanelReplenishmentOfResources { get; set; } = new PanelReplenishmentOfResources();

    public static TruckPanelController instanse;

    [SerializeField] private Text Parts;
    [SerializeField] private Text Fuel;
    [SerializeField] private Text Level;

    [SerializeField] private GameObject[] _openCLoseGameObj;
    [SerializeField] private BarController _barControllerParts;
    [SerializeField] private BarController _barControllerFuel;

    [SerializeField] private Button FullTankButtton;
    [SerializeField] private Button RepairButton;
    [SerializeField] private Button _switchButtonInWallet;
    private Truck Truck;
    private PlayerData _playerData;
    private GameObject _cardPlayer;
    private GameObject _currentPanel;
    private GameObject _cardClone;

    public void StartSwitchingInfoCard(bool isCurrentSwitch)
    {
        StartBar();
        OpenClosePanel(isCurrentSwitch);
    }
    private void Awake()
    {
        EventBroadcastCurrentActiivePlayer += UpdateCard;
        ResetEventValuePlayer += ResetValuePlayer;
        if (instanse !=null) Destroy(instanse.gameObject);
        instanse = this;
        PanelReplenishmentOfResources.Awake();
        PanelReplenishmentOfResources.EventSaveState += SetState;
    }
    private void OnDestroy()
    {
        EventBroadcastCurrentActiivePlayer -= UpdateCard;
        ResetEventValuePlayer -= ResetValuePlayer;
        PanelReplenishmentOfResources.OnDestroy();
        PanelReplenishmentOfResources.EventSaveState -= SetState;


        FullTankButtton.onClick.RemoveAllListeners();
        RepairButton.onClick.RemoveAllListeners(); 
    }

    private void Start()
    {
        _playerData = PlayerData.instanse;

        _switchButtonInWallet.onClick.AddListener(() => SwitchingBetweenStates.EventInWallet?.Invoke(_currentPanel, _cardPlayer, _cardClone));

        // FullTankButtton.onClick.AddListener(() => FullTank());
        //RepairButton.onClick.AddListener(() => Repair());

        RepairButton.onClick.AddListener(() => AddResourseToCurrentActiveCard(false));
        FullTankButtton.onClick.AddListener(() => AddResourseToCurrentActiveCard(true));
    }
    
    public void UpdateUI()
    {
        Fuel.text = " Fuel " + Truck.CurrentFuel.ToString() + " / " + Truck.MaxFuel;
        Parts.text = " Parts " + Truck.CurrentParts.ToString() + " / " + Truck.MaxParts.ToString();
        Level.text = $"LEVEL { Truck.SkillLevel} ";
        StartBar();
    }

    private void AddResourseToCurrentActiveCard(bool isResourse)
    {
        if (Truck != null && Truck.Travel == 0)
        {
            PanelReplenishmentOfResources.UpdatePlayerState(Truck.CurrentFuel, Truck.MaxFuel, Truck.CurrentParts, Truck.MaxParts);
            PanelReplenishmentOfResources.AcivePanel(isResourse, PanelEnum.TruckPanel);
        }

    }
    public  void ResetValuePlayer()
    {
        _cardClone = null;
        _cardPlayer = null;
        _currentPanel = null;

        Parts.text = "";
        Fuel.text = "";
        Level.text = "";
        OpenClosePanel(false);
    }
    private void UpdateCard(GameObject PanelPlayer, GameObject CurdPlayer, GameObject CardClone)
    {
        _playerData = PlayerData.instanse;
        this._cardClone = CardClone;
        _cardPlayer = CurdPlayer;
        _currentPanel = PanelPlayer;

        Truck = CardClone.GetComponent<LinkCurrentCard>()._dataCurrentCardTruck.CurrentDataCard;
        UpdateUI();
        OpenClosePanel(true);
        StartBar();
        PanelReplenishmentOfResources.UpdatePlayerState(Truck.CurrentFuel, Truck.MaxFuel, Truck.CurrentParts, Truck.MaxParts);
    }

    public void OpenClosePanel(bool isActive)
    {
        for (int i = 0; i < _openCLoseGameObj.Length; i++)
        {
            _openCLoseGameObj[i].SetActive(isActive);
        }
    }
    private void StartBar()
    {
        Bar.StartBar(_barControllerParts, Truck.MaxParts, Truck.CurrentParts);
        Bar.StartBar(_barControllerFuel, Truck.MaxFuel, Truck.CurrentFuel);
    }

    public void SetState()
    {
        PlayerData playerData = PlayerData.instanse;
        var CurrentStatePlayer = PanelReplenishmentOfResources.CurrentSetResources();
        int DifferenceBetweenParametersOne = (int)CurrentStatePlayer.Item1 - Truck.CurrentFuel;
        int DifferenceBetweenParametersTwo = (int)CurrentStatePlayer.Item2 - Truck.CurrentParts;
        playerData.instanseSavePlayerState.Fuel -= DifferenceBetweenParametersOne;
        playerData.instanseSavePlayerState.Parts -= DifferenceBetweenParametersTwo;
        Truck.CurrentFuel = (int)CurrentStatePlayer.Item1;
        Truck.CurrentParts = (int)CurrentStatePlayer.Item2;
        UpdateUI();
        StartBar();
        PanelReplenishmentOfResources.UpdatePlayerState(Truck.CurrentFuel, Truck.MaxFuel, Truck.CurrentParts, Truck.MaxParts);
    }
}