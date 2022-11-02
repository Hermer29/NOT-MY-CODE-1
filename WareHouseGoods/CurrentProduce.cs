using Code.MainMenu.Timer;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.WareHouseGoods
{
    public class CurrentProduce : MonoBehaviour
    {
        [field: SerializeField] public Text CurrentPoduceTime { get; set; }
        [field: SerializeField] public Text CurrentEndTimeSubscription { get; set; } //Реализовать с помощью таймера.
        [field: SerializeField] public GameObject PanelDataProduct { get; set; }
        [field: SerializeField] public List<InstanseTimer> ListTimerUpdateDay { get; set; } = new List<InstanseTimer>();
        [field: SerializeField] public List<InstanseTimer> ListTimerUpdateHour { get; set; } = new List<InstanseTimer>();
        [field: SerializeField] public Text CurrentRarityPoduce { get; set; }

        private void FixedUpdate()
        {
            if (ListTimerUpdateDay.Count != 0)
            {
                for (int i = 0; i < ListTimerUpdateDay.Count; i++)
                {
                    ListTimerUpdateDay[i].UpdateUiTimerDay();
                }
            }

            if (ListTimerUpdateHour.Count != 0)
            {
                for (int i = 0; i < ListTimerUpdateHour.Count; i++)
                {
                    ListTimerUpdateHour[i].UpdateUiTimerHour();
                }
            }
        }
    }
}