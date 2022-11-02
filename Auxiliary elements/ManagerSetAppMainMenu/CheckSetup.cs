using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CheckSetup
{
    public List<Driver> CheckDriver(List<Image> setApp)
    {
        List<Driver> ListDriver = new List<Driver>(2);
        for (int i = 0; i < PlayerData.instanse.instanseSaveCard.ListLoungeCardDriver.Count; i++)
        {
            if (PlayerData.instanse.instanseSaveCard.ListLoungeCardDriver[i].CurrentSetup != 0)
            {
                ListDriver.Add(PlayerData.instanse.instanseSaveCard.ListLoungeCardDriver[i]);
            }
        }
        for (int i = 0; i < setApp.Count; i++)
        {
            for (int b = 0; b < ListDriver.Count; b++)
            {
                if (setApp[i] == ListDriver[b].Icon)
                {
                    ListDriver.RemoveAt(b);
                    break;
                }
            }
        }
        return ListDriver;
    }
    public List<Truck> CheckTruck(List<Image> setApp)
    {
        List<Truck> ListTruck = new List<Truck>(2);
        for (int i = 0; i < PlayerData.instanse.instanseSaveCard.ListGarageCardTruck.Count; i++)
        {
            if (PlayerData.instanse.instanseSaveCard.ListGarageCardTruck[i].CurrentSetup != 0)
            {
                ListTruck.Add(PlayerData.instanse.instanseSaveCard.ListGarageCardTruck[i]);
            }
        }
        for (int i = 0; i < setApp.Count; i++)
        {
            for (int b = 0; b < ListTruck.Count; b++)
            {
                if (setApp[i] == ListTruck[b].Icon)
                {
                    ListTruck.RemoveAt(b);
                    break;
                }
            }
        }
        return ListTruck;
    }
    public List<Trailer> CheckTrailer(List<Image> setApp)
    {
        List<Trailer> ListTrailer = new List<Trailer>(2);
        for (int i = 0; i < PlayerData.instanse.instanseSaveCard.ListActiveCardTrailer.Count; i++)
        {
            if (PlayerData.instanse.instanseSaveCard.ListActiveCardTrailer[i].CurrentSetApp != 0)
            {
                ListTrailer.Add(PlayerData.instanse.instanseSaveCard.ListActiveCardTrailer[i]);
            }
        }
        for (int i = 0; i < setApp.Count; i++)
        {
            for (int b = 0; b < ListTrailer.Count; b++)
            {
                if (setApp[i] == ListTrailer[b].Icon)
                {
                    ListTrailer.RemoveAt(b);
                    break;
                }
            }
        }
        return ListTrailer;
        
    }
    public List<Image> CheckToElement(List<GameObject> setApp)
    {
        List<Image> imagesToCard = new List<Image>();
        for (int i = 0; i < setApp.Count; i++)
        {
            var a = setApp[i].GetComponentInChildren<Image>();
            if (a != null)
            {
                imagesToCard.Add(a);
            }
        }
        return imagesToCard;
    }
    public List<Image> CheckToElement(GameObject setApp)
    {
        List<Image> imagesToCard = new List<Image>();

        var a = setApp.GetComponentInChildren<Image>();
        if (a != null)
        {
            imagesToCard.Add(a);
        }

        return imagesToCard;
    }
}