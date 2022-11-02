using UnityEngine;
using UnityEngine.EventSystems;

public enum SwitchingPanel
{
    Active, InWallet, InGarage
}
public class ClickCardToTrailePanel : MonoBehaviour, IPointerClickHandler 
{
    public bool isGoods { get; set; }
   [field: SerializeField] public GameObject PanelFromGoods { get; set; }

    public SwitchingPanel SwitchingPanel;

    private LinkCurrentCard dataTrailerToScene;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        dataTrailerToScene = GetComponentInParent<LinkCurrentCard>();
        Debug.Log("Current ID Delited Product  " + dataTrailerToScene.IndexCard);
        switch (SwitchingPanel)
        {
            case SwitchingPanel.Active:

                if (dataTrailerToScene._dataCurrentCardTrailer.CurrentDataCard.Travel > 0)
                {
                    ManagerMainMenu.instanse.StartCoroutine(ManagerMainMenu.instanse.DebugCoroutine("Unable to send, is on a trip"));
                    return;
                }
                if (TrailerPanelController.instanse.isLoabingGoods)
                {
                    PanelFromGoods.SetActive(true);
                    TrailerPanelController.OnActionPanelGoods?.Invoke(dataTrailerToScene.gameObject); 
                }
                else
                {
                   TrailerPanelController.EventUpdateCardTrailer?.Invoke(SwitchingPanel.Active, dataTrailerToScene);
                }
                break;

            case SwitchingPanel.InWallet:
                TrailerPanelController.EventUpdateCardTrailer?.Invoke(SwitchingPanel.InWallet, dataTrailerToScene);
                break;

            case SwitchingPanel.InGarage:
                TrailerPanelController.EventUpdateCardTrailer?.Invoke(SwitchingPanel.InGarage, dataTrailerToScene);
                break;
        }
    }  
}