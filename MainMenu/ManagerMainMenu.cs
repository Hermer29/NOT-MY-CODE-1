using Assets.Code.Map;
using Assets.Code.StaticClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ManagerMainMenu : MonoBehaviour 
{
    public static ManagerMainMenu instanse { get; set; }

    public static Action EventChekToTravel { get; set; }

    [field: SerializeField] public UIDataPanel UIDataPanelActiveDriver { get;  set; }
    [field: SerializeField] public UIDataPanel UIDataPanelActiveTruck { get;  set; }
    [field: SerializeField] public UIDataPanel UIDataPanelActiveTrailer { get;  set; }
    [field: SerializeField] public UIDataPanel UIDataPanelActiveWarhouseGoods { get; set; }
    [field: SerializeField] private CurrentManagerPoint _currentManagerPoint { get; set; }

    [SerializeField] private TimerControllerToTravel TimerMAinMenu;
    [SerializeField] private UpdateDataTimerMainMenu updateDataTimerMainMenu = new UpdateDataTimerMainMenu();

    private ActualToPerckForTravel _actualToPerckForTravel { get; set; }

    [field: SerializeField] private Text MassTruck { get; set; }

    [field: SerializeField]  private EventButtonController eventButtonController { get; set; }


    private PlayerData _playerData;
    private void Awake()
    {
        EventChekToTravel += StartToTravel;
        updateDataTimerMainMenu.LetsGo.onClick.AddListener(() => StartToTravel());
        TimerMAinMenu = FindObjectOfType<TimerControllerToTravel>();

        if (instanse != null) Destroy(instanse);
        instanse = this;

        //Active WarhousePanel
        eventButtonController.ActivePanel(EventPanel.EventOnEnableWarhouseGoodsPanel);
        
    }
    private void Start()
    {
        eventButtonController.ActivePanel("EventTruePanelMainMenu");
        _playerData = PlayerData.instanse;
    }
    public void StartToTravel()
    {

        ////Check Contract

        var instanseSavePlayerState = _playerData.instanseSavePlayerState;
        int CurrentContracts;
        if (instanseSavePlayerState.ContratsOneHour >= 1 && _currentManagerPoint.CurrentlistContact.Count == 1)
        {
            CurrentContracts = (int)Contract.ContratsOneHour;
        }
        else if (instanseSavePlayerState.ContratsThreeHour >= 1 && _currentManagerPoint.CurrentlistContact.Count == 3)
        {
            CurrentContracts = (int)Contract.ContratsThreeHour;
        }
        else if (instanseSavePlayerState.ContratsSixHour >= 1 && _currentManagerPoint.CurrentlistContact.Count == 6)
        {
            CurrentContracts = (int)Contract.ContratsSixHour;
        }
        else if (instanseSavePlayerState.ContratsNineHour >= 1 && _currentManagerPoint.CurrentlistContact.Count == 9)
        {
            CurrentContracts = (int)Contract.ContratsNineHour;
        }
        else if (instanseSavePlayerState.ContratsTwelveHour >= 1 && _currentManagerPoint.CurrentlistContact.Count == 12)
        {
            CurrentContracts = (int)Contract.ContratsTwelveHour;
        }
        else
        {
            StartCoroutine(DebugCoroutine("Current Point  " + _currentManagerPoint.CurrentlistContact.Count + "  Add or remove Point"));
            return;
        }

        var ListActiveCardDriver = _playerData.instanseSaveCard.ListActiveCardDriver;
        var ListActiveCardTruck = _playerData.instanseSaveCard.ListActiveCardTruck;
        var ListActiveCardTrailer = _playerData.instanseSaveCard.ListActiveCardTrailer;

        int CurrentMassTruck = 0;
        if (PlayerData.instanse.instanseSaveCard.ListActiveCardTruck.Count != 0)
        {
            CurrentMassTruck = PlayerData.instanse.instanseSaveCard.ListActiveCardTruck[0].СarryingСapacity;
        }
        if (CurrentMassTruck < TrailerPanelController.instanse.CountСargo)
        {
            StartCoroutine(DebugCoroutine("Reduce the load"));
            return;
        }
        if (ListActiveCardDriver.Count == 0)
        {
            StartCoroutine(DebugCoroutine("Add Driver"));
            return;
        }
        else if (ListActiveCardDriver[0].CurrentEnergy < CurrentContracts || ListActiveCardDriver[0].CurrentHunger < CurrentContracts)
        {
            StartCoroutine(DebugCoroutine("Add Hunger or Energy"));
            return;
        }
        if (ListActiveCardTruck.Count == 0)
        {
            StartCoroutine(DebugCoroutine("Add Truck"));
            return;
        }
        else if (ListActiveCardTruck[0].CurrentFuel < CurrentContracts || ListActiveCardTruck[0].CurrentParts < CurrentContracts)
        {
            StartCoroutine(DebugCoroutine("Add Fuel or Parts"));
            return;
        }
        if (_currentManagerPoint.CurrentlistContact.Count == 0)
        {
            StartCoroutine(DebugCoroutine("Add Point"));
            return;
        }
        if (!(ListActiveCardDriver[0].SkillLevel >= ListActiveCardTruck[0].SkillLevel))
        {
            StartCoroutine(DebugCoroutine("Change truck or driver level"));
            return;
        }
        if (ListActiveCardDriver[0].Travel != 0 || ListActiveCardTruck[0].Travel != 0)
        {
            StartCoroutine(DebugCoroutine("Card To Travel"));
            return;
        }
      
        var ActiveTrailer = UIDataPanelActiveTrailer.GetComponent<UIDataPanel>().CardInGamePanel.GetComponentsInChildren<LinkCurrentCard>();
        for (int i = 0; i < ActiveTrailer.Length; i++)
        {
            if (ActiveTrailer[i]._dataCurrentCardTrailer.CurrentDataCard.Travel != 0 &&
                ActiveTrailer[i]._dataCurrentCardTrailer.CurrentDataCard.CurrentSetApp == PlayerData.instanse.instanseSaveCard.CurrentSetupPlayer)
            {
                StartCoroutine(DebugCoroutine("Switching Setup"));
                return;
            }
        }
        List<Trailer> CurrentActiveTrailer = new List<Trailer>();

        for (int i = 0; i < ActiveTrailer.Length; i++)
        {
            if (ActiveTrailer[i]._dataCurrentCardTrailer.CurrentDataCard.IsActive == true &&
                ActiveTrailer[i]._dataCurrentCardTrailer.CurrentDataCard.CurrentSetApp == PlayerData.instanse.instanseSaveCard.CurrentSetupPlayer)
            {
                CurrentActiveTrailer.Add(ActiveTrailer[i]._dataCurrentCardTrailer.CurrentDataCard);
            }
        }
        if (CurrentActiveTrailer.Count == 0)
        {
            StartCoroutine(DebugCoroutine("Card To Trailer"));
            return;
        }
        RemoveResourseTrailer removeResourseTrailer = new RemoveResourseTrailer();
        removeResourseTrailer.CurrentActiveTrailer = UIDataPanelActiveTrailer.CardInGamePanel.GetComponent<GridLayoutGroup>();
        if (removeResourseTrailer.CheckResourse() == 0)
        {
            StartCoroutine(DebugCoroutine("Goods add To Trailer"));
            return;
        }

        if (TrailerPanelController.instanse.CountMass > PlayerData.instanse.instanseSaveCard.ListActiveCardTruck[0].СarryingСapacity)
        {
            StartCoroutine(DebugCoroutine("Weight exceeded"));
            return;
        }
        int CurrentIndexCard = TravelExtension.AddIndexTravelCard(ListActiveCardDriver, ListActiveCardTruck);
        TravelExtension.СancellationСontracts(CurrentContracts);

        _actualToPerckForTravel = new ActualToPerckForTravel();
        _actualToPerckForTravel.EventStartToTravel.Invoke((Contract)CurrentContracts, CurrentActiveTrailer);

        removeResourseTrailer.ResetGoodsInActiveSetupTrailer();

        StartCoroutine(DebugCoroutine("StartTravel"));

        _playerData.instanseSaveMoneyPlayer.Drive += _actualToPerckForTravel.ReturnReward();

        _currentManagerPoint.BlockPoint();

        TimerMAinMenu.EventTravelMinuts.Invoke(CurrentContracts, CurrentIndexCard, true); //TODO 999: TEST

        //TimerMAinMenu.EventTravel.Invoke(CurrentContracts); //Origin

        _currentManagerPoint.DateCheckPoint.calculationCurrentTime(CurrentContracts);

        DriverPanelController.instanse.UpdateUI();
        TruckPanelController.instanse.UpdateUI();
        SelectContracts.CurrrentContract = 0;
        TrailerPanelController.instanse.UpdateDisplayToStateTrailer(); //Обновление Trailer Сard
    }
    
    public IEnumerator DebugCoroutine(string text)
    {
        updateDataTimerMainMenu.PanelDebug.SetActive(true);
        updateDataTimerMainMenu.TextMenu.text = text;
        Debug.Log(text);

        yield return new WaitForSeconds(1f);
        updateDataTimerMainMenu.PanelDebug.SetActive(false);
    }
    public IEnumerator PrintDebugLogToDelay(string text, float Time = 1f)
    {
        yield return new WaitForSeconds(Time);
        StartCoroutine(DebugCoroutine(text));
    }
    [field: SerializeField] private const int CURRENTCLOCKTIME = 1;
    [ContextMenu("TetTimer")]
    private void TestTimer()
    {
        TimerMAinMenu.EventTravelMinuts.Invoke(CURRENTCLOCKTIME,0, true);
    }
}