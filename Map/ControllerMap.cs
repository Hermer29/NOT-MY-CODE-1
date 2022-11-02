using Assets.Code.Map;
using Assets.Code.StaticClass;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Code.Map
{
    public class ControllerMap: MonoBehaviour
    {
        public static Action<bool> EventActionPanel { get; set; }
        public static Action UpdateToWarhousePoint { get; set; }
        public Action OpenMapAndSowMyLand { get; set; }
        public static Action EventResetPoint { get; set; }
        [field: SerializeField] public Slider _slider { get; set; }
        [field: SerializeField] private GameObject _contentMap { get; set; }

        [field: SerializeField] public static ControllerMap instanse;

        [field: SerializeField] public float CurrentDecreaseToCard = 1f;
        [field: SerializeField] private CurrentDataMap currentDataMap { get; set; } = new CurrentDataMap();
        [field: SerializeField] PointCordinateData pointCordinateData { get; set; } = new PointCordinateData();
        [field: SerializeField] CurrentManagerPoint _currentManagerPoint { get; set; }

        private СashToTransferWarhouse WarhouseGoodsCache = new СashToTransferWarhouse();

        private void Awake()
        {
            if (instanse != null) instanse = null;

            instanse = this;
            EventActionPanel += StartPanel;
            EventResetPoint += ResetPointCordinateData;
            OpenMapAndSowMyLand += OpenMapAndShowMyWarhouse;
            UpdateToWarhousePoint += ResetCachToWarhouse;
            currentDataMap.Awake(_slider, pointCordinateData);
            currentDataMap.StartInizializationWarhouse(_contentMap);

        }
        private void Start()
        {
            _slider.onValueChanged.AddListener(ChangesSlider);
        }
        private void OnDestroy()
        {
            OpenMapAndSowMyLand -= OpenMapAndShowMyWarhouse;

            _slider.onValueChanged.RemoveListener(ChangesSlider);
            EventActionPanel -= StartPanel;
            EventResetPoint -= ResetPointCordinateData;
            UpdateToWarhousePoint -= ResetCachToWarhouse;

            currentDataMap.OnDestroy();

        }
        private void ChangesSlider(float CurrentValue)
        {
            //if (CountTime.Second - DateTime.Now.Second <= 0 || CountTime.TimeOfDay.Minutes - DateTime.Now.Minute <=-1)
            //{
            //    CountValue = 0;
            //    Debug.Log("Сброс счётчика");
            //}
            //if (CountValue == 0) TransferSlider(CurrentValue);

            //_contentMap.transform.localScale = new Vector3(CurrentValue, CurrentValue, 1);
            CurrentDecreaseToCard = CurrentValue;

            //Debug.Log("Count  Second " +( CountTime.Second - DateTime.Now.Second).ToString() + "Count Minute "+ (CountTime.TimeOfDay.Minutes - DateTime.Now.Minute).ToString());
            //var a = _contentMap.GetComponent<RectTransform>();
            //a.anchoredPosition = new Vector2(TargetPointToCentralMap.x * CurrentValue, TargetPointToCentralMap.y * CurrentValue);
        }
        //public void OnPointerExit(PointerEventData eventData)
        //{
        //    Debug.Log("Курсор вышел из доступного для выбора элемента пользовательского интерфейса.");
        //}
        //private void TransferSlider(float CurrentValue)
        //{
        //    var tt = _contentMap.GetComponent<RectTransform>();
        //    tt.anchoredPosition = new Vector2(tt.anchoredPosition.x * CurrentValue, tt.anchoredPosition.y * CurrentValue);
        //    TargetPointToCentralMap = tt.anchoredPosition;
        //    CountTime = DateTime.Now.AddSeconds(3);
        //    Debug.Log("Welcom To New Count");
        //    CountValue++;
        //}
        private void ResetPointCordinateData()
        {
            pointCordinateData.CurrentPointMapTruck.RemoveRange(0, pointCordinateData.CurrentPointMapTruck.Count);
        }

        private void StartPanel(bool isCurrentState)
        {
            var a = _contentMap.GetComponentsInChildren<Point>();
            for (int i = 0; i < a.Length; i++)
            {
                a[i].enabled = isCurrentState;
            }
        }
        private void OpenMapAndShowMyWarhouse()
        {
            FindObjectOfType<EventButtonController>().ActivePanel(EventPanel.EventOnEnableMapPanel);
            var a = PlayerData.instanse.instanseSaveCard.ListActiveCardWareHouseGoodS[0].Cordinats;
            _contentMap.GetComponent<RectTransform>().anchoredPosition = ConvertatorСoordinates.Convert(a[0], a[1]);
            _slider.value = 1f;
        }
        public void SelectContract(Contract contract)
        {
            currentDataMap.SelectContract(contract);
        }
        public void ShowWarhouseGoodsToMapPanel()
        {
            if (WarhouseGoodsCache.Item >= PlayerData.instanse.instanseSaveCard.ListGarageCardWareHouseGoodS.Count)
            {
                WarhouseGoodsCache.Item = 0;
                WarhouseGoodsCache.isActive = true;

            }
            AddCached(WarhouseGoodsCache, currentDataMap.TransferMapToPointWarhouse(WarhouseGoodsCache.Item, WarhouseGoodsCache.isActive));
        }
        private void AddCached(СashToTransferWarhouse сashToButtonTransfer, (int[] Coordinates, int ItemWarhouse, bool isActive) CurrentData)
        {
            if (CurrentData.Coordinates != null)
            {
                сashToButtonTransfer.Item = CurrentData.Item2;
                сashToButtonTransfer.isActive = CurrentData.Item3;

                _contentMap.GetComponent<RectTransform>().anchoredPosition = ConvertatorСoordinates.Convert(CurrentData.Coordinates[0], CurrentData.Coordinates[1]);
                _slider.value = 1f;
            }
        }
        private void ResetCachToWarhouse()
        {
            WarhouseGoodsCache.isActive = true;
            WarhouseGoodsCache.Item = 0;
        }
        public void ShowMyTruckToMapPanel()
        {
            var Coordinates = currentDataMap.TransferToPointTruck(_currentManagerPoint._recentPointToTravel);
            if (Coordinates != null)
            {
                _contentMap.GetComponent<RectTransform>().anchoredPosition = ConvertatorСoordinates.Convert(Coordinates[0], Coordinates[1]);
                _slider.value = 1f;
            }
        }
      
    }
}
[Serializable]
public class CurrentDataMap 
{
    [field: SerializeField] public GameObject PanelMap { get; set; }
    [field: SerializeField] public Button ButtonSelectRoute { get; set; }
    [field: SerializeField] public Button OpenMainPanelMap { get; set; }

    private Slider _slider;
    private PointCordinateData _pointCordinateData;
    public  Contract _contract;
    public GameObject PanelContracts;
    public GameObject PanelWarhouseGoods;

    public void Awake(Slider _slider, PointCordinateData pointCordinateData)
    {
        ButtonSelectRoute.onClick.AddListener(() => SelectRoute());
        ButtonSelectRoute.onClick.AddListener(() => SelectRoute());


        OpenMainPanelMap.onClick.AddListener(() => PanelMap.gameObject.SetActive(true));
        OpenMainPanelMap.onClick.AddListener(() => OpenMainPanelMap.gameObject.SetActive(false));
        OpenMainPanelMap.onClick.AddListener(() => _slider.value = 0.3f);
        OpenMainPanelMap.onClick.AddListener(() => ControllerMap.EventActionPanel?.Invoke(false));
        this._slider = _slider;
        this._pointCordinateData = pointCordinateData;
    }
    public void StartInizializationWarhouse(GameObject CurrentMap)
    {
        var a = CurrentMap.GetComponentsInChildren<MapWarhouse>();
        for (int i = 0; i < a.Length; i++)
        {
            a[i].StartInizialization();
        }
    }
    public void OnDestroy()
    {
        ButtonSelectRoute.onClick.RemoveAllListeners();
        OpenMainPanelMap.onClick.RemoveAllListeners();;

        this._slider = null;
        this._pointCordinateData = null;
    }
    public void SelectContract(Contract contract) 
    {
        _contract = contract;
        SelectContracts.CurrrentContract = contract;
        Debug.Log("Текущий контракт: " + contract);
        CurrentManagerPoint.EventUpdateContracts?.Invoke();
        PanelContracts.SetActive(false);
    }
    private void SelectRoute()
    {
        if (SelectContracts.CurrrentContract != 0)
        {
            PanelMap.gameObject.SetActive(false);
            OpenMainPanelMap.gameObject.SetActive(true);
            ControllerMap.EventActionPanel?.Invoke(true);
        }
    }
    public (int[],int, bool) TransferMapToPointWarhouse(int ItemWarhouse, bool isActive)
    {
        var a =  PanelWarhouseGoods.GetComponent<UIDataPanel>().CardInGamePanel.GetComponentsInChildren<LinkCurrentCard>();
        int[] Coordinats = null;
        bool isActiveCurrentWarhouse = false;
        if (!isActiveCurrentWarhouse)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (a.Length > ItemWarhouse) 
                {
                    Coordinats = a[ItemWarhouse]._dataCurrentCardWareHouseGoodS.CurrentDataCard.Cordinats;
                    ItemWarhouse++;
                    break;
                }
            }
        }
        return (Coordinats, ItemWarhouse, false);
    }
    public int[] TransferToPointTruck(RecentPointToTravel recentPointToTravel)
    {
        RectTransform untransformedСoordinates = null;
        bool isActiveCurrentTruck = false;
        for (int i = 0; i < recentPointToTravel.LastPointsSetup.Count; i++)
        {
            if (recentPointToTravel.LastPointsSetup[i].Setup == PlayerData.instanse.instanseSaveCard.CurrentSetupPlayer)
            {
                untransformedСoordinates =  recentPointToTravel.LastPointsSetup[i].Point.GetComponent<RectTransform>();
                isActiveCurrentTruck = true;
                break;
            }
        }
        if (!isActiveCurrentTruck)
            return (null);
        else
            return (new int[2] { (int)untransformedСoordinates.anchoredPosition.x, (int)untransformedСoordinates.anchoredPosition.y });
    }
    
}
[Serializable]
public class PointCordinateData 
{
    [SerializeField] public List<List<int>> CurrentPointMapTruck { get; set; } = new List<List<int>>(); //Помещется первая точка пути
}


