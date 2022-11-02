using AbstractClass.Panel;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DriverPanelController : MonoBehaviour, IPanelController
{
    public static Action<GameObject, GameObject, GameObject> EventBroadcastCurrentActiivePlayer;
    public static Action ResetEventValuePlayer;

    public static DriverPanelController instanse;

    [SerializeField] private Text Energy;
    [SerializeField] private Text Hunger;
    [SerializeField] private Text LevelCard;
    [SerializeField] private Text PerckCard;
    [SerializeField] private Button buttonRest;
    [SerializeField] private Button Eat;
    [SerializeField] private Button _switchButtonInWallet;

    [SerializeField] private GameObject[] _openCLoseGameObj;
    [SerializeField] private BarController _barControllerEnergy;
    [SerializeField] private BarController _barControllerHunger;
    private Driver driver;
    PlayerData playerData;
    private LinkCurrentCard LinkCurrentCard;
    GameObject CardPlayer;
    GameObject CurrentPanel;
    GameObject CardClone;

    [field: SerializeField]public PanelReplenishmentOfResources PanelReplenishmentOfResources { get; set; } = new PanelReplenishmentOfResources();

    public void StartSwitchingInfoCard(bool isCurrentSwitch)
    {
        StartBar();
        OpenClosePanel(isCurrentSwitch);
    }
    private void Awake()
    {
        EventBroadcastCurrentActiivePlayer += UpdateCard;
        ResetEventValuePlayer += ResetValuePlayer;

        if (instanse != null) Destroy(instanse.gameObject);
        instanse = this;

        PanelReplenishmentOfResources.Awake();
        PanelReplenishmentOfResources.EventSaveState += SetState;

        buttonRest.onClick.AddListener(() => AddResourseToCurrentActiveCard(false));

        Eat.onClick.AddListener(() => AddResourseToCurrentActiveCard(true));
    }
    private void OnDestroy()
    {
        EventBroadcastCurrentActiivePlayer -= UpdateCard;
        ResetEventValuePlayer -= ResetValuePlayer;

        PanelReplenishmentOfResources.OnDestroy();
        PanelReplenishmentOfResources.EventSaveState -= SetState;


        buttonRest.onClick.RemoveAllListeners();
        Eat.onClick.RemoveAllListeners();

    }
    private void Start()
    {
        playerData = PlayerData.instanse;

        _switchButtonInWallet.onClick.AddListener(() => SwitchingBetweenStates.EventInWallet?.Invoke(CurrentPanel, CardPlayer, CardClone));

    }
    public void UpdateUI()
    {
        Energy.text = " Energy " + driver.CurrentEnergy.ToString() + " / " + driver.MaxEnergy;
        Hunger.text = " Hunger " + driver.CurrentHunger.ToString() + " / " + driver.MaxHunger.ToString();
        LevelCard.text = $"LEVEL {  driver.SkillLevel} ";
        PerckCard.text = $"PERK {driver.SkillPlayer} {driver.PercentValueSkill} %";
        StartBar();
    }
    public void SetState()
    {

        var CurrentStatePlayer = PanelReplenishmentOfResources.CurrentSetResources();
        int DifferenceBetweenParametersOne = (int)CurrentStatePlayer.Item1 - driver.CurrentHunger; 
        playerData.instanseSavePlayerState.Food -= DifferenceBetweenParametersOne;
        driver.CurrentHunger = (int)CurrentStatePlayer.Item1;

        int DifferenceBetweenParametersTwo = (int)CurrentStatePlayer.Item2 - driver.CurrentEnergy;
        playerData.instanseSavePlayerState.Rest -= DifferenceBetweenParametersTwo;
        driver.CurrentEnergy = (int)CurrentStatePlayer.Item2;

        UpdateUI();
        //PanelReplenishmentOfResources.UpdatePlayerState(driver.CurrentHunger, driver.MaxHunger, driver.CurrentEnergy, driver.MaxEnergy);
    }
    private void AddResourseToCurrentActiveCard(bool isResourse)
    {
        if (driver != null && driver.Travel == 0)
        {
            PanelReplenishmentOfResources.UpdatePlayerState(driver.CurrentHunger, driver.MaxHunger, driver.CurrentEnergy, driver.MaxEnergy);
            PanelReplenishmentOfResources.AcivePanel(isResourse, PanelEnum.DriverPanel);
        }
        
    }
    public void ResetValuePlayer()
    {
        CardClone = null;
        CardPlayer = null;
        CurrentPanel = null;
        LinkCurrentCard = null;

        Energy.text = "";
        Hunger.text = "";
        LevelCard.text = "";
        OpenClosePanel(false);
    }
    private void UpdateCard(GameObject PanelPlayer, GameObject CurdPlayer, GameObject CardClone)
    {
        CardPlayer = CurdPlayer;
        CurrentPanel = PanelPlayer;
        this.CardClone = CardClone;
        LinkCurrentCard = CurdPlayer.GetComponent<LinkCurrentCard>();
        PlayerData playerData = PlayerData.instanse;

        driver = LinkCurrentCard._dataCurrentCardDriver.CurrentDataCard;

        UpdateUI();

        OpenClosePanel(true);
        StartBar();

        PanelReplenishmentOfResources.UpdatePlayerState(driver.CurrentHunger, driver.MaxHunger, driver.CurrentEnergy, driver.MaxEnergy);
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
        Bar.StartBar(_barControllerEnergy, LinkCurrentCard._dataCurrentCardDriver.CurrentDataCard.MaxEnergy, LinkCurrentCard._dataCurrentCardDriver.CurrentDataCard.CurrentEnergy);
        Bar.StartBar(_barControllerHunger, LinkCurrentCard._dataCurrentCardDriver.CurrentDataCard.MaxHunger, LinkCurrentCard._dataCurrentCardDriver.CurrentDataCard.CurrentHunger);
    }
}

public static class Bar 
{
    public static void StartBar(BarController barController,int MaxCountOne, int MinCountOne)
    {
        barController.EventUpdateDisplayDar?.Invoke(MaxCountOne, MinCountOne);
    }
}
