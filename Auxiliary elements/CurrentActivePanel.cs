using Assets.Code.StaticClass;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code
{
    public class CurrentActivePanel : MonoBehaviour //При открытт новой панели передавать сюда эту панель. Пполе перехода на главное меню обнулять панели.
    {
        public static Action<String> EventAddCurrentPanel { get; set; }
        public static Action<String, bool> EventRemoveCurrentPanel { get; set; }
        public static Action<bool> EventOnSwitchingButtonBack { get; set; }

        [field: SerializeField] private List<String> CurrentHistoryGamePanel { get; set; } = new List<String>();
        [field: SerializeField] private EventButtonController eventButtonController { get; set; } // Прокинуть руками.
        [field: SerializeField] public Button ButtonBackToPanel { get; private set; }

        private void Awake()
        {
            EventAddCurrentPanel += AddCurrentPanel;
            EventRemoveCurrentPanel += RemovePanel;
            ButtonBackToPanel.onClick.AddListener(() => OnBackToPanel());
            EventOnSwitchingButtonBack  += ButtonActivity;
        }
        private void OnDestroy()
        {
            EventAddCurrentPanel -= AddCurrentPanel;
            EventRemoveCurrentPanel -= RemovePanel;
            ButtonBackToPanel.onClick.RemoveListener(() => OnBackToPanel());
            EventOnSwitchingButtonBack -= ButtonActivity;
        }
        private void AddCurrentPanel(String NamePanel)
        {
            Debug.Log("Вошел и положил панель номер " + NamePanel);
            if (CurrentHistoryGamePanel.IndexOf(NamePanel) == -1 && EventPanel.EventOnEnableMainPanel != NamePanel)
            {
                CurrentHistoryGamePanel.Add(NamePanel);
                ButtonActivity(true);
            }
            else
                RemovePanel(NamePanel, false);
            
        }
        private void RemovePanel(String NamePanel, bool isAll)
        {
            if (isAll)
            {
                CurrentHistoryGamePanel.RemoveRange(0, CurrentHistoryGamePanel.Count);
            }
            else
            {
                CurrentHistoryGamePanel.Remove(NamePanel);
            }
        }
        private void OnBackToPanel()
        {
            if (CurrentHistoryGamePanel.Count > 1 )
            {
                eventButtonController.ActivePanel(CurrentHistoryGamePanel[CurrentHistoryGamePanel.Count - 2]);

                RemovePanel(CurrentHistoryGamePanel[CurrentHistoryGamePanel.Count - 1], false);

            }
            else
            {
                eventButtonController.ActivePanel(StaticClass.EventPanel.EventOnEnableMainPanel);
            }
        }
        private void ButtonActivity(bool State)
        {
            ButtonBackToPanel.gameObject.SetActive(State);
        }
    }
}