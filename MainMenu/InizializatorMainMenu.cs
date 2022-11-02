using Assets.Code.StaticClass;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InizializatorMainMenu : MonoBehaviour
{
    public static Action<GameObject, GameObject> EventUpdateActiveCardToMainMenu { get; set; }
    public static Action<PanelEnum> EventDestroyCardToMainMenu { get; set; }
    public static Action<GameObject> EventUpdateActiveTrailerCardToMainMenu { get; set; }
    public static Action<Image> EventDestriyTrailerCardToMainMenu { get; set; }

    private PlayerData _playerData { get; set; }
    [SerializeField] private GridLayoutGroup GridLayoutGroupCurrentDriver;
    [SerializeField] private GridLayoutGroup GridLayoutGroupCurrentTruck;
    [field: SerializeField] private LayoutGroup GridLayoutGroupCurrentTrailer { get; set; }
    private GameObject CurrentCardDriver;
    private GameObject CurrentCardTruck;

    [field: SerializeField] private Image[] CurrentSetAppDriver { get; set; }
    private void Start()
    {
        StartCoroutine(StartInizialization());

        EventUpdateActiveCardToMainMenu += UpdateUIActiveMainMenuPanel;
        EventDestroyCardToMainMenu += DestroyCardToMainMenu;
        EventUpdateActiveTrailerCardToMainMenu += UpdateCardToTravelMainMenu;
        EventDestriyTrailerCardToMainMenu += DestroyCardToMeinMenuTrailer;
    }
    private void OnDestroy()
    {
        EventDestroyCardToMainMenu -= DestroyCardToMainMenu;
        EventUpdateActiveCardToMainMenu -= UpdateUIActiveMainMenuPanel;
        EventUpdateActiveTrailerCardToMainMenu -= UpdateCardToTravelMainMenu;
        EventDestriyTrailerCardToMainMenu -= DestroyCardToMeinMenuTrailer;
    }

    private void UpdateUIActiveMainMenuPanel(GameObject CurrentPanel, GameObject CurrentCardUpdate)
    {
        NameProduct nameProduct = CurrentPanel.GetComponent<UIDataPanel>().NameProduct;
        switch (nameProduct)
        {
            case NameProduct.Driver:
                DestroyCardToMainMenu(PanelEnum.DriverPanel);

                  CurrentCardDriver = CreateCard(GridLayoutGroupCurrentDriver.gameObject, CurrentCardUpdate.GetComponent<Image>().sprite);
                CreateCard(CreateCard(CurrentSetAppDriver[PlayerData.instanse.instanseSaveCard.CurrentSetupPlayer-1].gameObject, CurrentCardUpdate.GetComponent<Image>().sprite));
                break;
            case NameProduct.Truck:
                DestroyCardToMainMenu(PanelEnum.TruckPanel);
                CurrentCardTruck = CreateCard(GridLayoutGroupCurrentTruck.gameObject, CurrentCardUpdate.GetComponent<Image>().sprite);
                break;
          
                
        }  
    }
    private void UpdateCardToTravelMainMenu(GameObject CurrentCardUpdate)
    {
        CurrentCardTruck = CreateCard(GridLayoutGroupCurrentTrailer.gameObject, CurrentCardUpdate.GetComponentInChildren<Image>().sprite);
    }
    private void DestroyCardToMainMenu(PanelEnum panelEnum) //Удаление карт драйвера и трека
    {
        switch (panelEnum)
        {
            case PanelEnum.DriverPanel:
                var CurrentCardToCellMainMenuDriver = GridLayoutGroupCurrentDriver.GetComponentsInChildren<Image>();
                var CurentDriverSetAppIcone = CurrentSetAppDriver[PlayerData.instanse.instanseSaveCard.CurrentSetupPlayer - 1].GetComponentsInChildren<Image>();
                
                for (int i = 0; i < CurrentCardToCellMainMenuDriver.Length; i++)
                {
                    if (CurrentCardToCellMainMenuDriver[i] != null && CurrentCardToCellMainMenuDriver[i].sprite != PlayerData.instanse.IconcToMainMenu)
                    {
                        Destroy(CurrentCardToCellMainMenuDriver[i].gameObject);

                    }
                }
               
                for (int i = 0; i < CurentDriverSetAppIcone.Length; i++)
                {
                    if (CurentDriverSetAppIcone[i] != null && CurentDriverSetAppIcone[i].sprite != PlayerData.instanse.IconcToMainMenu)
                    {
                        Destroy(CurentDriverSetAppIcone[i].gameObject);
                    }
                }

                break;
            case PanelEnum.TruckPanel:
                var CurrentCardToCellMainMenuTruck = GridLayoutGroupCurrentTruck.GetComponentsInChildren<Image>();
                for (int i = 0; i < CurrentCardToCellMainMenuTruck.Length; i++)
                {
                    if (CurrentCardToCellMainMenuTruck[i] != null && CurrentCardToCellMainMenuTruck[i].sprite != PlayerData.instanse.IconcToMainMenu)
                        Destroy(CurrentCardToCellMainMenuTruck[i].gameObject);
                }
               
                break;
        }
    }
    private void DestroyCardToMeinMenuTrailer(Image CurrentIconeCardTrailer)
    {
        var CurrentCardToCellMainMenuTrailer = GridLayoutGroupCurrentTrailer.GetComponentsInChildren<Image>();
        for (int i = 0; i < CurrentCardToCellMainMenuTrailer.Length; i++)
        {
            if (CurrentCardToCellMainMenuTrailer[i].sprite == CurrentIconeCardTrailer.sprite)
            {
                Destroy(CurrentCardToCellMainMenuTrailer[i].gameObject);
            } 
        }
    }
    private GameObject CreateCard(GameObject gridLayoutGroup, Sprite spritAcivePlayer = null)
    {
        GameObject CurrentACtiveCard = new GameObject(name: "CurrentACtiveDriver");
        CurrentACtiveCard.transform.SetParent(gridLayoutGroup.transform, true);
        CurrentACtiveCard.AddComponent<Image>().sprite = spritAcivePlayer;

        CurrentACtiveCard.transform.localScale = Vector3.one;
        TransferPos.PositionZeroCoordinste(CurrentACtiveCard);
        return CurrentACtiveCard;
    }
    private GameObject CreateCard(GameObject CurrentCard)
    {
        CurrentCard.transform.localPosition = Vector2.zero;
        RectTransform RectTransform = CurrentCard.GetComponent<RectTransform>();
        RectTransform.sizeDelta = new Vector2(200, 300);
        return CurrentCard;
    }
    private void Inizialization() //To start //Added ToDriverIcone
    {
        _playerData = PlayerData.instanse;

        if (_playerData.instanseSaveCard.ListActiveCardDriver.Count > 0)
            CreateCard(GridLayoutGroupCurrentDriver.gameObject, _playerData.instanseSaveCard.ListActiveCardDriver[0].Icon);

        if (_playerData.instanseSaveCard.ListActiveCardTruck.Count > 0)
            CreateCard(GridLayoutGroupCurrentTruck.gameObject, _playerData.instanseSaveCard.ListActiveCardTruck[0].Icon);

        if (_playerData.instanseSaveCard.ListGarageCardTrailer.Count > 0)
        {
            for (int i = 0; i < _playerData.instanseSaveCard.ListActiveCardTrailer.Count; i++)
            {
                if (_playerData.instanseSaveCard.ListActiveCardTrailer[i].IsActive == true && _playerData.instanseSaveCard.ListActiveCardTrailer[i].CurrentSetApp == _playerData.instanseSaveCard.CurrentSetupPlayer)
                {
                    CreateCard(GridLayoutGroupCurrentTruck.gameObject, _playerData.instanseSaveCard.ListActiveCardTrailer[i].Icon);
                }               
            }
        }

    }
    IEnumerator StartInizialization()
    {
        yield return new WaitForSeconds(0.2f);
        Inizialization();
    }
}

