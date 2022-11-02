using System;
using UnityEngine;

namespace Assets.Code.WareHouseGoods
{
    public class DataSubscription
    {
        [field: SerializeField] public DateTime CurrentDataSubsription { get; set; }
        [field: SerializeField] public DateTime FinalDataSubsription { get; set; }
        [field: SerializeField] public Rarity RaritySbsription { get; set; }
        [field: SerializeField] public int CurrentIssuedGoods { get; set; }

    }
}