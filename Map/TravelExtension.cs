using System;
using System.Collections.Generic;

namespace Assets.Code.Map
{
    public static class TravelExtension
    {
        public static int AddIndexTravelCard(List<Driver> ListActiveCardDriver, List<Truck> ListActiveCardTruck)
        {

            SetAppCard CurrentActiveTravel = new SetAppCard();

            var RandomTravel = UnityEngine.Random.Range(200, 900);

            CurrentActiveTravel.IndexListSetApp = RandomTravel;

            var ListActivCardDriver = PlayerData.instanse.instanseSaveCard.ListActiveCardDriver;
            var ListActivCardTruck = PlayerData.instanse.instanseSaveCard.ListActiveCardTruck;
            var ListActivCardTrailer = PlayerData.instanse.instanseSaveCard.ListGarageCardTrailer;

            for (int i = 0; i < ListActivCardDriver.Count; i++)
            {
                ListActivCardDriver[i].Travel = RandomTravel;
                CurrentActiveTravel.ActiveCardDriver = ListActivCardDriver[i];
            }
            for (int i = 0; i < ListActivCardTruck.Count; i++)
            {
                ListActivCardTruck[i].Travel = RandomTravel;
                CurrentActiveTravel.ActiveCardTruck = ListActivCardTruck[i];
            }
            foreach (var item in ListActivCardTrailer)
            {
                if (item.IsActive == true && item.CurrentSetApp == PlayerData.instanse.instanseSaveCard.CurrentSetupPlayer)
                {
                    item.Travel = RandomTravel;
                    CurrentActiveTravel.ListActiveCardTrailer.Add(item);
                }
            }
            PlayerData.instanse.instanseSaveCard.SetAppCards.Add(CurrentActiveTravel);
           // RemoveStateCurrentTravel(ListActivCardDriver[0], ListActivCardTruck[0]);
            return RandomTravel;
        }
        public static void RemoveStateCurrentTravel(Driver driver, Truck truck)
        {
            driver.CurrentEnergy -= 1;
            driver.CurrentHunger -= 1;
            truck.CurrentFuel -= 1;
            truck.CurrentParts -= 1;
        }
        public static void СancellationСontracts( int CurrentContracts)
        {
            switch (CurrentContracts) // списание контракта
            {
                case 1:
                    PlayerData.instanse.instanseSavePlayerState.ContratsOneHour -= 1;
                    break;
                case 3:
                    PlayerData.instanse.instanseSavePlayerState.ContratsThreeHour -= 1;
                    break;
                case 6:
                    PlayerData.instanse.instanseSavePlayerState.ContratsSixHour -= 1;
                    break;
                case 9:
                    PlayerData.instanse.instanseSavePlayerState.ContratsNineHour -= 1;
                    break;
                case 12:
                    PlayerData.instanse.instanseSavePlayerState.ContratsTwelveHour -= 1;
                    break;
            }
        }
    }
}
