using AbstractClass.Panel;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickCard : MonoBehaviour, IPointerClickHandler
{
    public LinkCurrentCard linkCurrentCard;
    public GameObject ClonCard;
    public GameObject OriginCard;
    private ManagerMainMenu _managerMainMenu { get; set; }
    private GameObject CurrentPanelCard;
    public void OnPointerClick(PointerEventData eventData) //Добавить обновления отображения на главном меню. 
    {
        PlayerData playerData = PlayerData.instanse;

        _managerMainMenu = ManagerMainMenu.instanse;

        linkCurrentCard = ClonCard.GetComponent<LinkCurrentCard>();
        
        var LinkOriginGameObj = OriginCard.GetComponent<LinkCurrentCard>();

        

        if (ClonCard.GetComponent<LinkCurrentCard>().IndexCard != LinkOriginGameObj.IndexCard)
            return;

        switch (LinkOriginGameObj.NameProduct)
        {
            case NameProduct.Driver:
               var CurrenPanelDriver =  DriverPanelController.instanse;
                CurrentPanelCard = CurrenPanelDriver.gameObject;

                LinkOriginGameObj._dataCurrentCardDriver.CurrentDataCard.IsActive = false;
                var a = DopAddedComponent(CurrenPanelDriver, LinkOriginGameObj, 
                   LinkOriginGameObj._dataCurrentCardDriver.CurrentDataCard.Travel,
                   LinkOriginGameObj._dataCurrentCardDriver.CurrentDataCard.IsActive,
                   playerData.instanseSaveCard.ListActiveCardDriver,
                   linkCurrentCard._dataCurrentCardDriver.CurrentDataCard 
                   );
                if (a == false)
                {
                    return;
                }
                LinkOriginGameObj._dataCurrentCardDriver.CurrentDataCard.CurrentSetup = 0;
                InizializatorMainMenu.EventDestroyCardToMainMenu.Invoke(PanelEnum.DriverPanel);

                CurrenPanelDriver.OpenClosePanel(false);
               
                break;

            case NameProduct.Truck:
                var CurrenPanelTruck = TruckPanelController.instanse;
                CurrentPanelCard = CurrenPanelTruck.gameObject;

                LinkOriginGameObj._dataCurrentCardTruck.CurrentDataCard.IsActive = false;
               
               var b = DopAddedComponent(CurrenPanelTruck, LinkOriginGameObj,
                LinkOriginGameObj._dataCurrentCardTruck.CurrentDataCard.Travel,
                LinkOriginGameObj._dataCurrentCardTruck.CurrentDataCard.IsActive,
                playerData.instanseSaveCard.ListActiveCardTruck,
                linkCurrentCard._dataCurrentCardTruck.CurrentDataCard
                );
                if (b ==false)
                {
                    return;
                }
                LinkOriginGameObj._dataCurrentCardTruck.CurrentDataCard.CurrentSetup = 0;
                InizializatorMainMenu.EventDestroyCardToMainMenu.Invoke(PanelEnum.TruckPanel);

                //FindObjectOfType<TruckPanelController>().OpenClosePanel(false);
                CurrenPanelTruck.OpenClosePanel(false);
                break;
            case NameProduct.WareHouseGoodS:
                LinkOriginGameObj._dataCurrentCardWareHouseGoodS.CurrentDataCard.IsActive = false;
                var v = DopAddedComponent(null, LinkOriginGameObj,
               0,
               LinkOriginGameObj._dataCurrentCardWareHouseGoodS.CurrentDataCard.IsActive,
               playerData.instanseSaveCard.ListActiveCardWareHouseGoodS,
               linkCurrentCard._dataCurrentCardWareHouseGoodS.CurrentDataCard
               );
                if (v == false)
                {
                    return;
                }
                WarehouseGoodsController.EventResetGoods.Invoke();

                break;
        }
        gameObject.SetActive(false);
        GameObject.Destroy(gameObject);

    }
    public bool DopAddedComponent<T>(IPanelController panelController, LinkCurrentCard LinkOriginGameObj,
        int ValueTravel, bool State, List<T> ListPlayerActive, T Card)
    {
        if (ValueTravel > 0)
        {
            _managerMainMenu.StartCoroutine(_managerMainMenu.DebugCoroutine("Unable to send, is on a trip"));
            return false;
        }

        State = false;
        if (panelController != null)
        {
            panelController.ResetValuePlayer();

        }
        ListPlayerActive.Remove(Card);
        LinkOriginGameObj.CurrentActiveSetupGameObjectText.SetActive(false);
        LinkOriginGameObj.CurrentSetupText.GetComponent<Text>().text = "";

        LinkOriginGameObj.SetActiveButton.enabled = true;
        Button buttonSetActive = ClonCard.GetComponentInChildren<Button>();
        RectTransform RectTransform = buttonSetActive.GetComponent<RectTransform>();
        RectTransform.sizeDelta = new Vector2(200, 60);
        buttonSetActive?.onClick.AddListener(() => SwitchingBetweenStates.EventOnAction(CurrentPanelCard, ClonCard, true));
        return true;
    }

}
