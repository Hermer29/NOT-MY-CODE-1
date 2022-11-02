using System.Collections.Generic;
using UnityEngine;
public class AcrualRewardToTravel : IRewardTrawel
{
    PlayerData PlayerData = PlayerData.instanse;
    public float FinalReward { get; set; }

    public void Reward(Contract contract, List<Trailer> Tailers)
    {
        FinalReward = 0;
        SkillPlayer skillPlayer = PlayerData.instanseSaveCard.ListActiveCardDriver[0].SkillPlayer; //TODOD 5644444445
        var Perk =  RewardToPerck(skillPlayer, contract);
        var CurrentPriceToGoods = new ConstCountPricePurchases();

        CalcualationReward(
            CurrentPriceToGoods.Rests, CurrentPriceToGoods.Foods,
            CurrentPriceToGoods.Fuel, CurrentPriceToGoods.Parts,
            contract, PlayerData.instanseSaveCard.ListActiveCardDriver[0].Income, Perk,
            CurrentPriceToGoods.OneClockContracts, CurrentPriceToGoods.ThreeClockContracts,
            CurrentPriceToGoods.SixClockContracts, CurrentPriceToGoods.NineClockContracts, 
            CurrentPriceToGoods.TwelveClockContracts,
            CurrentPriceToGoods.CommonGoods, CurrentPriceToGoods.RareGoods, CurrentPriceToGoods.EpicGoods, CurrentPriceToGoods.LegendaryGoods,
            Tailers
            ); 
    }
    private int RewardToPerck(SkillPlayer skillPlayer, Contract contract) //Luke
    {
        int Luck = -1,
            Parts = (int)contract,
            Fuel = (int)contract,
            Hunger = (int)contract, 
            Energy = (int)contract; 

        if (PlayerData.instanseSaveCard.ListActiveCardDriver[0].PercentValueSkill > Random.Range(0, 100))
        {
            switch (skillPlayer)
            {
                case SkillPlayer.Luck:
                    Luck = PlayerData.instanseSaveCard.ListActiveCardDriver[0].PercentValueSkill;
                    break;

                case SkillPlayer.Energy:
                    Energy = 0;                
                    break;

                case SkillPlayer.Hunger:
                    Hunger = 0;
                    break;
            }
           ManagerMainMenu.instanse.StartCoroutine(ManagerMainMenu.instanse.PrintDebugLogToDelay($"Skill worked {skillPlayer}", 3f));
        }
        WriteOffResourceDriveAndCar(Parts, Fuel, Hunger, Energy);

        return Luck;
    }
    private void WriteOffResourceDriveAndCar(int Parts, int Fuel, int Hunger, int Energy)
    {
        PlayerData.instanseSaveCard.ListActiveCardDriver[0].CurrentEnergy -= Energy;
        PlayerData.instanseSaveCard.ListActiveCardDriver[0].CurrentHunger -= Hunger;
        PlayerData.instanseSaveCard.ListActiveCardTruck[0].CurrentFuel -= Fuel;
        PlayerData.instanseSaveCard.ListActiveCardTruck[0].CurrentParts -= Parts;
    }
    

    private void CalcualationReward(int CurrentPriceToEnergy, int CurrentPriceToHunger, int CurrentPriceToFuel, int CurrentPriceToParts, 
        Contract contract, float icome,float Perck, int OneContract, int ThreeContract, int SixContract, int NineContract, int TwelveContract,
        int CommonGoods, int RareGoods, int EpicGoods, int LegendaryGoods, List<Trailer> Tailers) 
    {
        float CurrentСonsumptionToContract = 0;
        float remunerationAdditionfactor = 0;
        float PriсeContract = 0; //Wax ConstContract.СurrentCoinRate;

        switch (contract) //Просчёт цены за за контракт  
        {
            case Contract.ContratsOneHour: //Contracts1 = 5 * 1
                PriсeContract = OneContract;
                break;
            case Contract.ContratsThreeHour:
                PriсeContract = ThreeContract;
                break;
            case Contract.ContratsSixHour:
                PriсeContract = SixContract;
                break;
            case Contract.ContratsNineHour:
                PriсeContract = NineContract;
                break;
            case Contract.ContratsTwelveHour:
                PriсeContract = TwelveContract;
                break;
        }

        //Расоход без учёта груза

        float CurrentExpenses = PriсeContract + CalculationMoneyToContract((int)contract, CurrentPriceToEnergy, CurrentPriceToHunger, CurrentPriceToFuel, CurrentPriceToParts);
        for (int i = 0; i < Tailers.Count; i++) //Цена за товар.
        {
            if (Tailers[i].CommonGoods > 0)
            {
                CurrentСonsumptionToContract += Tailers[i].CommonGoods * CommonGoods;
                remunerationAdditionfactor += Tailers[i].CommonGoods * 0.1f * ConstContract.СurrentCoinRate;

            }
            if (Tailers[i].RareGoods > 0)
            {
                CurrentСonsumptionToContract += Tailers[i].RareGoods * RareGoods;
                remunerationAdditionfactor += Tailers[i].RareGoods * 0.3f * ConstContract.СurrentCoinRate;
            }
            if (Tailers[i].EpicGoods > 0)
            {
                CurrentСonsumptionToContract += Tailers[i].EpicGoods * EpicGoods;
                remunerationAdditionfactor += Tailers[i].EpicGoods * 0.6f * ConstContract.СurrentCoinRate;
            }
            if (Tailers[i].LegendaryGoods > 0)
            {
                CurrentСonsumptionToContract += Tailers[i].LegendaryGoods * LegendaryGoods;
                remunerationAdditionfactor += Tailers[i].LegendaryGoods * 1.2f * ConstContract.СurrentCoinRate;
            }
        }
        var a = remunerationAdditionfactor * icome * (int)contract;
        Debug.Log($"CurrentExpenses {CurrentExpenses} remunerationAdditionfactor {remunerationAdditionfactor} Почти итоговая награда {a}");
        if (Perck > 0)
        {
          a += a * (Perck / 100);
        }

        FinalReward = a + CurrentExpenses + CurrentСonsumptionToContract;
        Debug.Log("FinalRevard " + FinalReward);
    }
    private int CalculationMoneyToContract(int Contract, int CurrentPriceToEnergy, int CurrentPriceToHunger, int CurrentPriceToFuel, int CurrentPriceToParts)
    {
        return Contract * (CurrentPriceToEnergy + CurrentPriceToHunger + CurrentPriceToFuel + CurrentPriceToParts);
    }
}