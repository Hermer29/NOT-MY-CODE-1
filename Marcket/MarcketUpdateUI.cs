using System;
using UnityEngine;
using UnityEngine.UI;

public class MarcketUpdateUI : MonoBehaviour
{
    [SerializeField] Text CountBuyGood1;
    [SerializeField] Text CountBuyGood2;
    [SerializeField] Text CountBuyGood3;
    [SerializeField] Text CountBuyGood4;

    [SerializeField] Text CountBuyFood;
    [SerializeField] Text CountBuyRest;
    [SerializeField] Text CountBuyParts;
    [SerializeField] Text CountBuyFuel;
    [SerializeField] Text CountBuyStuff;
    
    [SerializeField] Text CountBuyOneClock;
    [SerializeField] Text CountBuyThreeClock;
    [SerializeField] Text CountBuySixClock;
    [SerializeField] Text CountBuyNineClock;
    [SerializeField] Text CountBuyTwelveClock;

    [SerializeField] Text TotalCount;

    public Action<int, int, int, int, int, int, int, int, int, int, int, int,int, int> EventUpdateDisplayUIMarcket { get; set; }
    private void Start()
    {
        EventUpdateDisplayUIMarcket += UpdateDisplayUIMarcket;
    }
    private void OnDestroy()
    {
        EventUpdateDisplayUIMarcket -= UpdateDisplayUIMarcket;
    }
    private void UpdateDisplayUIMarcket(int slotsfood, int slotsRest, int slotsParts, int slotsFuel, int slotsStuff, int slots1, int slots2, int slots3, int slots4, 
        int OneContract, int ThreeContract, int SixContract, int NineContract, int TwelveContract )
    {
        CountBuyGood1.text =  slots1.ToString();
        CountBuyGood2.text =  slots2.ToString();
        CountBuyGood3.text = slots3.ToString();
        CountBuyGood4.text =  slots4.ToString();
        CountBuyFood.text = slotsfood.ToString();
        CountBuyRest.text = slotsRest.ToString();
        CountBuyParts.text = slotsParts.ToString();
        CountBuyFuel.text =slotsFuel.ToString();
        CountBuyStuff.text = slotsStuff.ToString();
        CountBuyOneClock.text = OneContract.ToString();
        CountBuyThreeClock.text = ThreeContract.ToString();
        CountBuySixClock.text = SixContract.ToString();
        CountBuyNineClock.text = NineContract.ToString();
        CountBuyTwelveClock.text = TwelveContract.ToString();
    }
}
