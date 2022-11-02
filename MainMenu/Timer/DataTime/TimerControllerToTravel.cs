using Code.MainMenu.Timer;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TimerControllerToTravel : MonoBehaviour
{
    public Action<int, int> EventTravel { get; set; }

    public Action<int,int, bool> EventTravelMinuts { get; set; }
    [field: SerializeField] private DataTimer _dataTimer { get; set; }
    [field: SerializeField] public List<ParentTimer> parentTimers { get; set; } = new List<ParentTimer>();

    public static TimerControllerToTravel instanse;

    private void Awake()
    {
        if (instanse != null)
        {
            Destroy(instanse);
        }
        instanse = this;

        EventTravel += NewStartTimer;
        EventTravelMinuts += NewStartTimer;
        //Брать сохранение таймера и воспроизводиить его.   
    }
    private void OnDestroy()
    {
        EventTravel -= NewStartTimer;
        EventTravelMinuts -= NewStartTimer;
    }
    [ContextMenu("NEW TEST")]
    private void NewStartTimer(int Time, int IndexCardTravel)
    {
        parentTimers.Add(new InstanseTimer(Time, _dataTimer.texts[PlayerData.instanse.instanseSaveCard.CurrentSetupPlayer -1], IndexCardTravel)); //Передавать текущие время поезки
    }
    private void NewStartTimer(int Time, int IndexCardTravel, bool isTime)
    {
        parentTimers.Add(new InstanseTimer(Time, _dataTimer.texts[PlayerData.instanse.instanseSaveCard.CurrentSetupPlayer - 1], IndexCardTravel, isTime)); //Передавать текущие время поезки
    }
    private void FixedUpdate()
    {
        if (parentTimers.Count > 0)
        {
            for (int i = 0; i < parentTimers.Count; i++)
            {
                parentTimers[i].UpdateUiTimerHour();
            }
        }
    }

}
