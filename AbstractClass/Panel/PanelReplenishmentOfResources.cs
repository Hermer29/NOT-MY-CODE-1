using System;
using UnityEngine;
using UnityEngine.UI;

namespace AbstractClass.Panel
{
    [Serializable]
    public class PanelReplenishmentOfResources //Числа используются для дебага потом возможно убрать их из видимости
    {
        [field: SerializeField] private Button SaveState { get; set; }
        [field: SerializeField] private Button ClosePanel { get; set; }
        [field: SerializeField] private int CurrentOneState { get; set; }
        [field: SerializeField] private int CurrentTwoState { get; set; }
        [field: SerializeField] private int MaxOneState { get; set; }
        [field: SerializeField] private int MaxTwoState { get; set; }

        [field: SerializeField] private int ConstOneState { get; set; }
        [field: SerializeField] private int ConstTwoState { get; set; }

        [field: SerializeField] private int CurrentOneStatePlayer { get; set; }
        [field: SerializeField] private int CurrentTwoStatePlayer { get; set; }


        [field: SerializeField] private ElementPanelResources _elementPanelOneResources { get; set; } = new ElementPanelResources();
        [field: SerializeField] private ElementPanelResources _elementPanelTwoResources { get; set; } = new ElementPanelResources();
        [field: SerializeField] private PanelEnum _panelEnum { get; set; }

        [field: SerializeField] private GameObject CurrentPanelAddstate { get; set; }

        public Action EventSaveState { get; set;}
        public void Awake()
        {          
            SaveState.onClick.AddListener(() => EventSaveState?.Invoke());
            SaveState.onClick.AddListener(() => ResetState());
            ClosePanel.onClick.AddListener(() => ResetState());
        }
        public void OnDestroy()
        {
            SaveState.onClick.RemoveAllListeners(); //=> EventSaveState?.Invoke());
            ClosePanel.onClick.RemoveAllListeners(); // => ResetState());
        }
        public void AcivePanel(bool isOneResourse, PanelEnum panelEnum)
        {
            _elementPanelOneResources.ButtonMinus.onClick.RemoveAllListeners();
            _elementPanelOneResources.ButtonPlus.onClick.RemoveAllListeners();
            CurrentPanelAddstate.SetActive(true);
            var ResourcesPlayer = PlayerData.instanse.instanseSavePlayerState;
            _panelEnum = panelEnum;

            if (_panelEnum == PanelEnum.DriverPanel)
            {
                CurrentOneStatePlayer = ResourcesPlayer.Food;
                CurrentTwoStatePlayer = ResourcesPlayer.Rest;
            }
            else if (_panelEnum == PanelEnum.TruckPanel)
            {
                CurrentOneStatePlayer = ResourcesPlayer.Fuel;
                CurrentTwoStatePlayer = ResourcesPlayer.Parts;
            }
            if (isOneResourse)
            {
                _elementPanelOneResources.ResourcesToPlayer.text = CurrentOneStatePlayer.ToString();
                _elementPanelOneResources.CurrentResources.text = "0";
                _elementPanelOneResources.ButtonMinus.onClick.AddListener(() => StartUpdateStateOneResources(false));
                _elementPanelOneResources.ButtonPlus.onClick.AddListener(() => StartUpdateStateOneResources(true));
                _elementPanelOneResources.CurrentResourse.gameObject.SetActive(true);
                _elementPanelTwoResources.CurrentResourse.gameObject.SetActive(false);
            }
            else
            {
                _elementPanelTwoResources.ResourcesToPlayer.text = CurrentTwoStatePlayer.ToString();
                _elementPanelTwoResources.CurrentResources.text = "0";
                _elementPanelTwoResources.ButtonMinus.onClick.AddListener(() => StartUpdateStateTwoResources(false));
                _elementPanelTwoResources.ButtonPlus.onClick.AddListener(() => StartUpdateStateTwoResources(true));

                _elementPanelTwoResources.CurrentResourse.gameObject.SetActive(true);
                _elementPanelOneResources.CurrentResourse.gameObject.SetActive(false);
            }
        }
        public void UpdatePlayerState(int OneState, int MaxOneState, int TwoState, int MaxTwoState)
        {
            ResetState();
            ConstTwoState = TwoState;
            ConstOneState = OneState;

            this.MaxOneState = MaxOneState;
            this.MaxTwoState = MaxTwoState;
        }
        public (int?, int?) CurrentSetResources()
        {
            return (CurrentOneState + ConstOneState, CurrentTwoState + ConstTwoState);
        }
        private void StartUpdateStateOneResources(bool isPlus)
        {
            int CurrrentrResources1 = 0;
            int CurrentResources2 = 0;

            if (_panelEnum == PanelEnum.DriverPanel)
                CurrrentrResources1 = PlayerData.instanse.instanseSavePlayerState.Food;

            else
                CurrrentrResources1 = PlayerData.instanse.instanseSavePlayerState.Fuel;


            if (isPlus)
            {
                if (CurrentOneState + ConstOneState < MaxOneState && PlayerData.instanse.instanseSaveMoneyPlayer.Drive >= CurrentOneState)
                {
                    CurrentOneState++;
                    CurrentOneStatePlayer--;
                    _elementPanelOneResources.CurrentResources.text = CurrentOneState.ToString();
                    _elementPanelOneResources.ResourcesToPlayer.text = (CurrrentrResources1 - CurrentOneState).ToString();
                }
            }
            else
            {
                if (CurrentOneState > 0 && ConstOneState != MaxOneState)
                {
                    CurrentOneState--;
                    CurrentOneStatePlayer++;
                    _elementPanelOneResources.CurrentResources.text = CurrentOneState.ToString();
                    _elementPanelOneResources.ResourcesToPlayer.text = (CurrrentrResources1 - CurrentOneState).ToString();
                }
            }
        }
        private void StartUpdateStateTwoResources(bool isPlus)
        {
            int CurrrentResources1 = 0;
            int CurrentResources2 = 0;

            if (_panelEnum == PanelEnum.DriverPanel)
                CurrentResources2 = PlayerData.instanse.instanseSavePlayerState.Rest;
            else
              
                CurrentResources2 = PlayerData.instanse.instanseSavePlayerState.Parts;

            if (isPlus)
            {
                if (CurrentTwoState + ConstTwoState < MaxTwoState && PlayerData.instanse.instanseSaveMoneyPlayer.Drive >= CurrentTwoState)
                {
                    CurrentTwoState++;
                    CurrentTwoStatePlayer--;
                    _elementPanelTwoResources.CurrentResources.text = CurrentTwoState.ToString();
                    _elementPanelTwoResources.ResourcesToPlayer.text = (CurrentResources2 - CurrentTwoState).ToString();
                }
            }
            else
            {
                if (CurrentTwoState > 0 && ConstTwoState != MaxTwoState)
                {
                    CurrentTwoState--;
                    CurrentTwoStatePlayer++;
                    _elementPanelTwoResources.CurrentResources.text = CurrentTwoState.ToString();
                    _elementPanelTwoResources.ResourcesToPlayer.text = (CurrentResources2 - CurrentTwoState).ToString();
                } 
            }
        }
        public void ResetState()
        {
            CurrentOneState = 0;
            CurrentTwoState = 0;
            MaxOneState = 0;
            MaxTwoState = 0;
            CurrentOneStatePlayer = 0;
            CurrentTwoStatePlayer = 0;
            _elementPanelTwoResources.ButtonMinus.onClick.RemoveAllListeners();
            _elementPanelTwoResources.ButtonPlus.onClick.RemoveAllListeners();

        }
    }
}
[Serializable]
public class ElementPanelResources
{
    [field: SerializeField] public Text ResourcesToPlayer { get; set; }
    [field: SerializeField] public Text CurrentResources { get; set; }
    [field: SerializeField] public Button ButtonMinus { get; set; }
    [field: SerializeField] public Button ButtonPlus { get; set; }
    [field: SerializeField] public GameObject CurrentResourse { get; set; }
}