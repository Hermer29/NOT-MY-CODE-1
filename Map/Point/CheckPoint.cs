using System;
using System.Collections.Generic;

namespace Assets.Code.Map
{
    [System.Serializable]
    public class CheckPoint
    {
        public RideSave rideSave { get; set; }
        public List<ContactPointToMap>  ContactPoints {get;set;}
        public void calculationCurrentTime(int? TimeContrats)
        {
            var CurrentIndexTravel = UnityEngine.Random.Range(1, 1000000);
            var EndDataTimeTrip = DateTime.Now;
            EndDataTimeTrip.AddHours((double)TimeContrats);

            PlayerData playerData = PlayerData.instanse;

            rideSave.ListdateTimes = new List<SaveDataEntry>();
            rideSave.ListfrozenTravelCards = new List<frozenTravelCards>();

            //CopyList
            List<Trailer> trailers = new List<Trailer>();

            foreach (var item in playerData.instanseSaveCard.ListGarageCardTrailer)
            {
                if (item.IsActive == true && item.CurrentSetApp == playerData.instanseSaveCard.CurrentSetupPlayer)
                {
                    trailers.Add(item);

                }
            }
            rideSave.ListdateTimes.Add(new SaveDataEntry
            {
                CurrentPointData = DateTime.Now,
                EndDataTimeTrip = EndDataTimeTrip,
                IndexTailer = CurrentIndexTravel,

            }
            );
            rideSave.ListfrozenTravelCards.Add(new frozenTravelCards
            {
                EndDataTimeTrip = EndDataTimeTrip,
                IndexTailer = CurrentIndexTravel,
                CurrentDriver = playerData.instanseSaveCard.ListActiveCardDriver[0],
                CurrentTruck = playerData.instanseSaveCard.ListActiveCardTruck[0],
                CurrentTrailer = trailers,
                ListPoints = ContactPoints
            }
            );
        }
    }

}