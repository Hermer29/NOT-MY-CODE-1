using System;
using UnityEngine.UI;

namespace Code.MainMenu.Timer
{
    public class InstanseTimer : ParentTimer
    {
        public InstanseTimer(int Time, Text TextTime, int IdTravel)
        {
            StartTimer(Time, TextTime, IdTravel);
        }
        public InstanseTimer(DateTime dataTime, Text TextTime, int IdTravel)
        {
            StartTimer(dataTime, TextTime, IdTravel);
        }

        public InstanseTimer(int Time, Text TextTime, int IdTravel, bool isMinutes)
        {
            StartTimer(Time, TextTime, isMinutes, IdTravel);
        }
    }
}