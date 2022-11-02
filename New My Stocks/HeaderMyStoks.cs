using UnityEngine;

namespace Assets.Code.My_Stocks
{
    //HeaderResoursePlayer
    public class HeaderMyStoks : MonoBehaviour 
    {
        public void UpdateDisplay(UiDataMyStoksPanel uiDataMyStoksPanel, SavePlayerState savePlayerState)
        {
            uiDataMyStoksPanel.Foods.text = savePlayerState.Food.ToString();
            uiDataMyStoksPanel.Rests.text = savePlayerState.Rest.ToString();
            uiDataMyStoksPanel.Parts.text = savePlayerState.Parts.ToString();
            uiDataMyStoksPanel.Fuel.text = savePlayerState.Fuel.ToString();
            uiDataMyStoksPanel.CommonGoods.text = savePlayerState.CommonGoods.ToString();
            uiDataMyStoksPanel.RareGoods.text = savePlayerState.RareGoods.ToString();
            uiDataMyStoksPanel.EpicGoods.text = savePlayerState.EpicGoods.ToString();
            uiDataMyStoksPanel.LegendaryGoods.text = savePlayerState.legendaryGoods.ToString();
            uiDataMyStoksPanel.Stuff.text = savePlayerState.StuffMoney.ToString();
            uiDataMyStoksPanel.OneContract.text = savePlayerState.ContratsOneHour.ToString();
            uiDataMyStoksPanel.ThreeContract.text = savePlayerState.ContratsThreeHour.ToString();
            uiDataMyStoksPanel.SixContract.text = savePlayerState.ContratsSixHour.ToString();
            uiDataMyStoksPanel.NineContract.text = savePlayerState.ContratsNineHour.ToString();
            uiDataMyStoksPanel.TwelveContract.text = savePlayerState.ContratsTwelveHour.ToString();
        }
        public void UpdateContracts(UiDataMyStoksPanel uiDataMyStoksPanel, SavePlayerState savePlayerState)
        {
            uiDataMyStoksPanel.OneContract.text = $"{savePlayerState.ContratsOneHour} pieces";
            uiDataMyStoksPanel.ThreeContract.text = $"{savePlayerState.ContratsThreeHour} pieces";
            uiDataMyStoksPanel.SixContract.text = $"{savePlayerState.ContratsSixHour} pieces"; 
            uiDataMyStoksPanel.NineContract.text = $"{savePlayerState.ContratsNineHour} pieces";
            uiDataMyStoksPanel.TwelveContract.text = $"{savePlayerState.ContratsTwelveHour} pieces";
        }
    }
}