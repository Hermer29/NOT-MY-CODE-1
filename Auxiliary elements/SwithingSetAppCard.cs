using Assets.Code.MainMenu;
using Assets.Code.Map;
using UnityEngine;
using UnityEngine.UI;

public class SwithingSetAppCard : MonoBehaviour //Main Headler Switching Card
{
    [SerializeField] private GameObject DriverPanel;
    [SerializeField] private GameObject TruckPanel;

    [SerializeField] private GridLayoutGroup ActiveDriverGridLayoutGroup;
    [SerializeField] private GridLayoutGroup GameDriverGridLayoutGroup;

    [SerializeField] private GridLayoutGroup ActiveTruckGridLayoutGroup;
    [SerializeField] private GridLayoutGroup GameTruckGridLayoutGroup;

    [SerializeField] private GridLayoutGroup ActiveTrailerGridLayoutGroup;
    [SerializeField] private GridLayoutGroup GameTrailerGridLayoutGroup;

    LinkCurrentCard[] ActiveDriver { get; set; }
    LinkCurrentCard[] GameDriver { get; set; }

    LinkCurrentCard[] ActiveTruck { get; set; }
    LinkCurrentCard[] GameTruck { get; set; }

    LinkCurrentCard[] ActiveTrailer { get; set; }
    LinkCurrentCard[] GameTrailer { get; set; }

    private PlayerData _playerData;
    private void Start()
    {
        _playerData = PlayerData.instanse;
    }

    [SerializeField] private CheckSetup _checkSetup = new CheckSetup();
    [SerializeField] private Creator _creator = new Creator();
    [SerializeField] private DataCheck _dataCheck = new DataCheck();
    [SerializeField] private int CountSetApp;

    [ContextMenu("StartToNewSetApp")]
    public void SwitchSetup(int CurrentSetup)
    {
        if (CurrentSetup == _playerData.instanseSaveCard.CurrentSetupPlayer) return;

        DeleteCard();     
        PlayerData.instanse.instanseSaveCard.CurrentSetupPlayer = CurrentSetup;
        ManagerSavePointToMap.instanse.EventSwitchingSetup?.Invoke();

        MainHeadlerSetapp.EventUpdateHeaderMainMenu?.Invoke();

        var currentImage = _checkSetup.CheckToElement(_dataCheck.SetApp);
        var currentActiveDriverToSwitchImage = _checkSetup.CheckDriver(currentImage);
        for (int i = 0; i < currentActiveDriverToSwitchImage.Count; i++)
        {
            _creator.Create(currentActiveDriverToSwitchImage[i].Icon, _dataCheck.SetApp[currentActiveDriverToSwitchImage[i].CurrentSetup - 1].gameObject);
        }

        var currentActiveDriver = _checkSetup.CheckDriver(_checkSetup.CheckToElement(_dataCheck.CardDriverToActiveSetApp));
        if (currentActiveDriver.Count != 0) _creator.Create(currentActiveDriver[0].CurrentSetup, currentActiveDriver[0].Icon, _dataCheck.CardDriverToActiveSetApp);


        var currentActiveTruck = _checkSetup.CheckTruck(_checkSetup.CheckToElement(_dataCheck.CardTruckToActiveSetApp));
        if (currentActiveTruck.Count != 0)  _creator.Create(currentActiveTruck[0].CurrentSetup, currentActiveTruck[0].Icon, _dataCheck.CardTruckToActiveSetApp);

        var currentActiveTrailer = _checkSetup.CheckTrailer(_checkSetup.CheckToElement(_dataCheck.CardTrailerToActiveSetApp));
      
        for (int i = 0; i < currentActiveTrailer.Count; i++)
        {
            _creator.Create(currentActiveTrailer[i].Icon, _dataCheck.CardTrailerToActiveSetApp);
        }
        ClearCard();
        StartSwitshing();

        //ResetPoint
        ControllerMap.EventResetPoint?.Invoke();
        CurrentManagerPoint.EventResetPoint.Invoke(null, null);
    }
    private void DeleteCard()
    {
        DeleteCardToMainMenu.Delete(_dataCheck.CardDriverToActiveSetApp.gameObject);
        DeleteCardToMainMenu.Delete(_dataCheck.CardTruckToActiveSetApp.gameObject);
        var ff = _dataCheck.CardTrailerToActiveSetApp.GetComponentsInChildren<Image>();
        for (int i = 0; i < _dataCheck.SetApp.Count; i++)
        {
            var a = _dataCheck.SetApp[i].GetComponentInChildren<Image>();
            if (a != null && a.sprite != PlayerData.instanse.IconcToMainMenu)
            {
                Destroy(a.gameObject);
            }
        }
        for (int i = 0; i < ff.Length; i++)
        {
            if (_dataCheck.CurrentImageTrailer != ff[i].sprite)
            {
                Destroy(ff[i].gameObject);
            }
        }
    }
    private void StartSwitshing() 
    {
        GetCurrentCard();
        for (int i = 0; i < GameDriver.Length; i++)
        {
            if (GameDriver[i]._dataCurrentCardDriver.CurrentDataCard.CurrentSetup == PlayerData.instanse.instanseSaveCard.CurrentSetupPlayer && PlayerData.instanse.instanseSaveCard.CurrentSetupPlayer != 0)
            {
                new SwitchingBetweenStates().OnAction(DriverPanel, GameDriver[i].gameObject, false);
            }
        }
        for (int i = 0; i < GameTruck.Length; i++)
        {
            if (GameTruck[i]._dataCurrentCardTruck.CurrentDataCard.CurrentSetup == PlayerData.instanse.instanseSaveCard.CurrentSetupPlayer && PlayerData.instanse.instanseSaveCard.CurrentSetupPlayer != 0)
            {
                new SwitchingBetweenStates().OnAction(TruckPanel, GameTruck[i].gameObject, false);
            }
        }

        TrailerPanelController.EventStartSetup?.Invoke();
    }

    private void ClearCard() 
    {
        GetCurrentCard();
      
        for (int i = 0; i < GameDriver.Length; i++)
        {
            if (ActiveDriver.Length == 0)
            {
                break;
            }
            if (ActiveDriver[0]._dataCurrentCardDriver.CurrentDataCard.indexCard == GameDriver[i]._dataCurrentCardDriver.CurrentDataCard.indexCard)
            {
                PlayerData.instanse.instanseSaveCard.ListActiveCardDriver.Remove(ActiveDriver[0]._dataCurrentCardDriver.CurrentDataCard); // Удаляем текущего игрока
                OnDisableToButton(ActiveDriver[0], GameDriver[i]);
                DriverPanelController.ResetEventValuePlayer?.Invoke();
                break;
            }
        }

        for (int i = 0; i < GameTruck.Length; i++)
        {
            if (ActiveTruck.Length == 0)
            {
                break;
            }
            if (ActiveTruck[0]._dataCurrentCardTruck.CurrentDataCard.indexCard == GameTruck[i]._dataCurrentCardTruck.CurrentDataCard.indexCard)
            {
                _playerData.instanseSaveCard.ListActiveCardTruck.Remove(ActiveTruck[0]._dataCurrentCardTruck.CurrentDataCard); // Удаляем текущего игрока
                OnDisableToButton(ActiveTruck[0], GameTruck[i]);
                TruckPanelController.ResetEventValuePlayer?.Invoke();
                break;
            }
        }
    }
    private void OnDisableToButton(LinkCurrentCard linkCurrentCardClon, LinkCurrentCard linkCurrentCardOrigin)
    {
        linkCurrentCardOrigin.SetActiveButton.enabled = false;
        Destroy(linkCurrentCardClon.gameObject);
    }
    public void GetCurrentCard()
    {
        ActiveDriver = ActiveDriverGridLayoutGroup.GetComponentsInChildren<LinkCurrentCard>();
        GameDriver = GameDriverGridLayoutGroup.GetComponentsInChildren<LinkCurrentCard>();

        ActiveTruck = ActiveTruckGridLayoutGroup.GetComponentsInChildren<LinkCurrentCard>();
        GameTruck = GameTruckGridLayoutGroup.GetComponentsInChildren<LinkCurrentCard>();

        ActiveTrailer = ActiveTrailerGridLayoutGroup.GetComponentsInChildren<LinkCurrentCard>();
        GameTrailer = GameTrailerGridLayoutGroup.GetComponentsInChildren<LinkCurrentCard>();
    }
}
