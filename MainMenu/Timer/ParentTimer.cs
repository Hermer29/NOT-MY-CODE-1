
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.MainMenu.Timer
{
    public abstract class ParentTimer
    {
        private DateTime FinalDataTimer;
        private Text TextTimer;
        public int IdTimer { get; set;} 
        bool isFalse; //TODO 2: Добавить такую же конструкцию и для таймера выраженного в днях. 
        private int CurrentDay;
        protected void StartTimer(int Time,Text TextTime, int IdTravel)
        {
            FinalDataTimer = DateTime.Now.AddHours(Time);
            TextTimer = TextTime;
            IdTimer = IdTravel;
        }
        protected void StartTimer(DateTime dataTimer, Text TextTime, int IdTravel)
        {
            FinalDataTimer = dataTimer;
            TextTimer = TextTime;
            IdTimer = IdTravel;
        }
        protected void StartTimer(int Time, Text TextTime, bool isMinute, int IdTravel)
        {
            FinalDataTimer = DateTime.Now.AddSeconds(Time);
            TextTimer = TextTime;
            IdTimer = IdTravel;
        }
        public void UpdateUiTimerHour()
        {
            if (isFalse != true)
            {
                TimeSpan CurrentTimeLeft = FinalDataTimer - DateTime.Now;
                if (0 > CurrentTimeLeft.Seconds)
                {
                    RemoveTimer();
                    isFalse = true;
                    return;
                }
                TextTimer.text = $"{CurrentTimeLeft.Hours}:{CurrentTimeLeft.Minutes}:{CurrentTimeLeft.Seconds} ";
            }
        }
        public void UpdateUiTimerDay()
        {
            CurrentDay = 0;

            TimeSpan CurrentTimeLeft = FinalDataTimer - DateTime.Now;
            
            var a = FinalDataTimer.Month - DateTime.Now.Month;
            var b = FinalDataTimer.Day - DateTime.Now.Day;
            if (a > 0)
            {
                CurrentDay = a * 30;
            }
            CurrentDay += b - 1;

            TextTimer.text = $"{CurrentDay}Day:{CurrentTimeLeft.Hours}:{CurrentTimeLeft.Minutes}:{CurrentTimeLeft.Seconds} ";
        }
        private void RemoveTimer() //TODO 3: Вынести от сюда.
        {
           var a =  PlayerData.instanse.instanseSaveCard.SetAppCards;
            var b = TimerControllerToTravel.instanse.parentTimers;

            SetAppCard setAppCard = null;

            for (int i = 0; i < a.Count; i++)
            {
                if (a[i].IndexListSetApp == IdTimer)
                {
                    setAppCard = a[i]; 

                    a[i].ActiveCardDriver.Travel = 0;

                    a[i].ActiveCardTruck.Travel = 0;
                    for (int v = 0; v < a[i].ListActiveCardTrailer.Count; v++)
                    {
                        a[i].ListActiveCardTrailer[v].Travel = 0;
                    }
                    break;
                }
            }
            for (int i = 0; i < b.Count; i++)
            {
                if (b[i].IdTimer == IdTimer)
                {
                    b.Remove(b[i]);
                    break;
                }
            }
            a.Remove(setAppCard);
            TextTimer.text = "";
        } 
    }
}
