using UnityEngine;

public class UIDataPanel : MonoBehaviour
{
    [SerializeField] private GameObject ActivePanelGameObj;
    [SerializeField] private GameObject CardInGamePanelGameObj;
    [SerializeField] private GameObject InWalletPanelGameObj;
    [SerializeField] private GameObject TransfersPanelGameObj;
    [SerializeField] private NameProduct NameProductE;


    public GameObject ActivePanel
    {
        get { return ActivePanelGameObj; }
        set { ActivePanelGameObj = value; }
    }

    public GameObject CardInGamePanel
    {
        get { return CardInGamePanelGameObj; }
        set { CardInGamePanelGameObj = value; }
    }
    public GameObject InWalletPanel
    {
        get { return InWalletPanelGameObj; }
        set { InWalletPanelGameObj = value; }
    }
    public GameObject TransfersPanel
    {
        get { return TransfersPanelGameObj; }
        set { TransfersPanelGameObj = value; }
    }
    public NameProduct NameProduct
    {
        get { return NameProductE; }
        set { NameProductE = value; }
    }
    private void OnEnable()
    {
        if (NameProductE == NameProduct.Trailer)
        {
            TrailerPanelController.EventUpdateMaxMass?.Invoke();
        }
    }
}
