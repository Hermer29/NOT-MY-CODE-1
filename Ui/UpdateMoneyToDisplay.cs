using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Ui
{
    [System.Serializable]
    public class UpdateMoneyToDisplay
    {
        public static Action<float> EventUpdateMoney;
        [field: SerializeField] private TMP_Text CurrentMoneyToDisplay { get; set; }
        public void StartEvent()
        {
            EventUpdateMoney += UpdateUi;
        }
        public void OnDestroy()
        {
            EventUpdateMoney -= UpdateUi;
        }
        private void UpdateUi(float CurrentMoney)
        {
            CurrentMoneyToDisplay.text = CurrentMoney.ToString();
        }
    }
}
