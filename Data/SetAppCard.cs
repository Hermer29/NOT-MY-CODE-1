using System;
using System.Collections.Generic;
[System.Serializable]
public class SetAppCard
{
    public int IndexListSetApp;
    public Driver ActiveCardDriver;
    public Truck ActiveCardTruck;
    public List<Trailer> ListActiveCardTrailer = new List<Trailer>();
    public DateTime EndTravel;
}

