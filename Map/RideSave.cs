using System;
using System.Collections.Generic;
namespace Assets.Code.Map
{
    public class RideSave
    {
        public List<SaveDataEntry> ListdateTimes { get; set; }
        public List<frozenTravelCards> ListfrozenTravelCards { get; set; }

    }
    public class SaveDataEntry
    {
        public int IndexTailer;
        public DateTime CurrentPointData;
        public DateTime EndDataTimeTrip;
    }
    public class frozenTravelCards
    {
        public int IndexTailer;
        public List<ContactPointToMap> ListPoints;
        public List<Trailer> CurrentTrailer;
        public Driver CurrentDriver;
        public Truck CurrentTruck;
        public DateTime EndDataTimeTrip;
    }
}