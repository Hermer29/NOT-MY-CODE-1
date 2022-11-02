using Assets.Code.StaticClass;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class TrailerPanelController : MonoBehaviour
{
    [field: SerializeField] public static TrailerPanelController instanse;

    public static Action<SwitchingPanel, LinkCurrentCard> EventUpdateCardTrailer { get; set; }
    public static Action<GameObject> OnActionPanelGoods { get; set; }
    public static Action<int> EventStatseGoods { get; set; } //пока инт, потом возможно прокидывать bool
    public static Action EventUpdateMaxMass { get; set; }
    public static Action EventStartSetup { get; set; }
    [SerializeField] private Text CountText;
    [SerializeField] private Text CountTextmainMenu;
    [field: SerializeField] public int CountMass { get; set; }
    [field: SerializeField] public int CountСargo { get; private set; }
    [SerializeField] private int TimeCountMass;
    [SerializeField] private int TimeCountСargo;
    private PlayerData _playerData;
    [SerializeField] private UIDataPanel UIDataPanel;
    [SerializeField] private Button LoodsGoods;
    [SerializeField] private GameObject _panelToLoadingGoods;

    public bool isLoabingGoods { get; set; }
    private GridLayoutGroup _inActivePanel;
    private GridLayoutGroup _inGamePanel;
    private GridLayoutGroup _inWalletPanel;
    [field: SerializeField] private Text MassTruck { get; set; }
    [field: SerializeField] private GameObject ActiveTruck { get; set; }
    [field: SerializeField] private Sprite _spriteButtonSetActive { get; set; }
    [field: SerializeField] private Sprite _spriteButtonSetUpload { get; set; }



    private void Start()
    {
        StartCoroutine(StartSetActiveTrailerPanel());
        LoodsGoods.onClick.AddListener(() => StartToLoadingGoods());
        EventStatseGoods += CalculationGoods;
        EventStartSetup += ToSwitchingSetup;
        EventUpdateMaxMass += UpdateMaxMass;

        if (instanse  != null)
        {
            Destroy(instanse);
        }
        instanse = this;
    }
    private void OnDestroy()
    {
        LoodsGoods.onClick.RemoveListener(() => StartToLoadingGoods());
        EventStatseGoods -= CalculationGoods;
        EventStartSetup -= ToSwitchingSetup;
        EventUpdateMaxMass -= UpdateMaxMass;

    }
    private void  UpdateMaxMass()
    {
        if (PlayerData.instanse.instanseSaveCard.ListActiveCardTruck.Count != 0)
        {
            MassTruck.text = PlayerData.instanse.instanseSaveCard.ListActiveCardTruck[0].СarryingСapacity.ToString();
        }
        else
        {
            MassTruck.text = "0";
        }
        
    }
    
    IEnumerator StartSetActiveTrailerPanel()
    {
        yield return new WaitForSeconds(0.2f);
        EventUpdateCardTrailer += LogicUpdateCard;

         _playerData = PlayerData.instanse;

        _inActivePanel = UIDataPanel.ActivePanel.GetComponent<GridLayoutGroup>();
        _inGamePanel = UIDataPanel.CardInGamePanel.GetComponent<GridLayoutGroup>();
        _inWalletPanel = UIDataPanel.InWalletPanel.GetComponent<GridLayoutGroup>();

        inizializationsCreateCard();

    }
    public void UpdateDisplayToStateTrailer()
    {
       var a  = UIDataPanel.ActivePanel.GetComponentsInChildren<LinkCurrentCard>();
        var b = UIDataPanel.CardInGamePanel.GetComponentsInChildren<LinkCurrentCard>();
        for (int i = 0; i < a.Length; i++)
        {
            UpdateCurrentTravelResourse(a[i]);
            if (a[i]._dataCurrentCardTrailer.CurrentDataCard.IsActive == true)
            {
           //     (TimeCountMass, TimeCountСargo) =
           //DefineStat.GeneratorState(a[i]._dataCurrentCardTrailer.CurrentDataCard.Rarity, a[i]._dataCurrentCardTrailer.CurrentDataCard.level);
            }
        }
        for (int i = 0; i < b.Length; i++)
        {
            UpdateCurrentTravelResourse(b[i]);
        }
        CountText.text = CountСargo.ToString();
    }
    private void UpdateCurrentTravelResourse(LinkCurrentCard linkCurrentCard)
    {
        linkToElements linkToElementsNewGameObj = linkCurrentCard.GetComponentInChildren<linkToElements>();
        linkToElementsNewGameObj.CurrentLoadGoods.text = $"{linkCurrentCard._dataCurrentCardTrailer.CurrentDataCard.CurrentCountResource} / {linkCurrentCard._dataCurrentCardTrailer.CurrentDataCard.TotalCountResource}";
        
    }
    private void LogicUpdateCard(SwitchingPanel switchingPanel, LinkCurrentCard linkCurrentCard)
    {
        switch (switchingPanel) //Здесь принимаются текущие панели и обработка карт с перемещением на другие панели.
        {
            case SwitchingPanel.Active:
                LinkCurrentCard[] CurrentCountInGameCard = _inGamePanel.GetComponentsInChildren<LinkCurrentCard>();
                for (int i = 0; i < CurrentCountInGameCard.Length; i++)
                {
                    if (linkCurrentCard._dataCurrentCardTrailer.CurrentDataCard.Travel == 0 && linkCurrentCard._dataCurrentCardTrailer.CurrentDataCard == CurrentCountInGameCard[i]._dataCurrentCardTrailer.CurrentDataCard)
                    {
                        CalculationGoodsToCard(linkCurrentCard);
                        CurrentCountInGameCard[i].GetComponentInChildren<linkToElements>().CurrentLoadGoods.text = CurrentCountInGameCard[i]._dataCurrentCardTrailer.CurrentDataCard.CurrentCountResource + " / " + CurrentCountInGameCard[i]._dataCurrentCardTrailer.CurrentDataCard.TotalCountResource.ToString(); 
                        Destroy(linkCurrentCard.gameObject);

                        CurrentCountInGameCard[i]._dataCurrentCardTrailer.CurrentDataCard.IsActive = false;
                        CurrentCountInGameCard[i]._dataCurrentCardTrailer.CurrentDataCard.CurrentSetApp = 0;
                        CurrentCountInGameCard[i].UniversalButtonForTrailer.gameObject.GetComponent<ClickCardToTrailePanel>().enabled = true;
                        CalculationResurse(CurrentCountInGameCard[i], false);
                        InizializatorMainMenu.EventDestriyTrailerCardToMainMenu(CurrentCountInGameCard[i].GetComponentInChildren<Image>());

                        CurrentCountInGameCard[i].CurrentSetupText.GetComponent<Text>().text = "";
                        linkCurrentCard.CurrentSetupText.GetComponent<Text>().text = "";
                        break;
                    }
                }
                break;
            case SwitchingPanel.InWallet:
                LinkCurrentCard[] CurrentCountInWalletCard = _inWalletPanel.GetComponentsInChildren<LinkCurrentCard>();
                for (int i = 0; i < CurrentCountInWalletCard.Length; i++)
                {
                    if (linkCurrentCard._dataCurrentCardTrailer.CurrentDataCard == CurrentCountInWalletCard[i]._dataCurrentCardTrailer.CurrentDataCard)
                    {
                        _playerData.instanseSaveCard.ListInWalletCardTrailer.Remove(linkCurrentCard._dataCurrentCardTrailer.CurrentDataCard);
                        _playerData.instanseSaveCard.ListGarageCardTrailer.Add(linkCurrentCard._dataCurrentCardTrailer.CurrentDataCard);
                    
                        UpdateDisplayToPanel(SwitchingPanel.InGarage, linkCurrentCard.UniversalButtonForTrailer.gameObject.GetComponent<ClickCardToTrailePanel>(), _inGamePanel.gameObject, linkCurrentCard);
                        linkCurrentCard.UniversalButtonForTrailer.GetComponent<Image>().sprite = _spriteButtonSetActive;
                        break;
                    }
                }
                break;
            case SwitchingPanel.InGarage:
                foreach (var item in PlayerData.instanse.instanseSaveCard.CurrentTravelCard)
                {
                    if (PlayerData.instanse.instanseSaveCard.CurrentSetupPlayer == item) 
                    {
                        return;
                    }
                }

                linkCurrentCard._dataCurrentCardTrailer.CurrentDataCard.IsActive = true;
                linkCurrentCard._dataCurrentCardTrailer.CurrentDataCard.CurrentSetApp = PlayerData.instanse.instanseSaveCard.CurrentSetupPlayer;
                CloneCode(linkCurrentCard.gameObject, linkCurrentCard, SwitchingPanel.InGarage, false);
                CalculationResurse(linkCurrentCard, true);
                linkCurrentCard.UniversalButtonForTrailer.GetComponent<Image>().sprite = _spriteButtonSetActive;
                break;
            default:
                Debug.LogError("false");
                break;
        }
    }

    private void AddSwitchingButton(SwitchingPanel switchingPanel, LinkCurrentCard linkCurrentCard) 
    {
        switch (switchingPanel)
        {
            case SwitchingPanel.Active:
                linkCurrentCard.UniversalButtonForTrailer.GetComponent<Image>().sprite = _spriteButtonSetActive;
                break;
            case SwitchingPanel.InWallet:
                linkCurrentCard.UniversalButtonForTrailer.GetComponent<Image>().sprite = _spriteButtonSetUpload;
                break;
            case SwitchingPanel.InGarage:
                linkCurrentCard.UniversalButtonForTrailer.GetComponent<Image>().sprite = _spriteButtonSetActive;
                break;
        }
    } 
    private void CreateButton(LinkCurrentCard linkCurrentCard)
    {
        var currentCreateButton = Instantiate(PlayerData.instanse.UniversalButtonForTrailer, Vector3.one, Quaternion.identity, linkCurrentCard.gameObject.transform);
        currentCreateButton.transform.localScale = Vector3.one;
        RectTransform RectTransform = currentCreateButton.GetComponent<RectTransform>();
        RectTransform.sizeDelta = new Vector2(200, 70);
        RectTransform.anchoredPosition = new Vector2(0, -170.24f);

        linkCurrentCard.UniversalButtonForTrailer = currentCreateButton;
        TransferPos.PositionZeroCoordinste(currentCreateButton);
    }
    private void ToSwitchingSetup()
    {
        LinkCurrentCard[] CurrentCountInGameCard = _inGamePanel.GetComponentsInChildren<LinkCurrentCard>();
        LinkCurrentCard[] CurrentCountinActive = _inActivePanel.GetComponentsInChildren<LinkCurrentCard>();
        for (int i = 0; i < CurrentCountInGameCard.Length; i++)
        {
            for (int b = 0; b < CurrentCountinActive.Length; b++)
            {
                if (CurrentCountinActive[b]._dataCurrentCardTrailer.CurrentDataCard == CurrentCountInGameCard[i]._dataCurrentCardTrailer.CurrentDataCard)
                {
                    CurrentCountInGameCard[i].GetComponentInChildren<linkToElements>().CurrentLoadGoods.text = CurrentCountInGameCard[i]._dataCurrentCardTrailer.CurrentDataCard.CurrentCountResource + " / " + CurrentCountInGameCard[i]._dataCurrentCardTrailer.CurrentDataCard.TotalCountResource.ToString();
                    Destroy(CurrentCountinActive[b].gameObject);

                    CurrentCountInGameCard[i].UniversalButtonForTrailer.gameObject.GetComponent<ClickCardToTrailePanel>().enabled = false;
                    CalculationResurse(CurrentCountInGameCard[i], false);
                    break;
                }
            }
            
        }
        for (int i = 0; i < CurrentCountInGameCard.Length; i++)
        {
            if (CurrentCountInGameCard[i]._dataCurrentCardTrailer.CurrentDataCard.CurrentSetApp == PlayerData.instanse.instanseSaveCard.CurrentSetupPlayer)
            {
                LogicUpdateCard(SwitchingPanel.InGarage, CurrentCountInGameCard[i]);           
            }
        }
        LinkCurrentCard[] CurrentCountinActiveToNew = _inActivePanel.GetComponentsInChildren<LinkCurrentCard>();
        for (int i = 0; i < CurrentCountinActiveToNew.Length; i++)
        {
            var CurrentClickCard = CurrentCountinActiveToNew[i].UniversalButtonForTrailer;
            if (CurrentClickCard != null)
            {
                CurrentClickCard.gameObject.GetComponent<ClickCardToTrailePanel>().enabled = true;
            }
        }

    } 
    private void UpdateDisplayToPanel(SwitchingPanel switchingPanel, ClickCardToTrailePanel clickCardToTrailePanel, GameObject MovmentPanel = null, LinkCurrentCard linkCurrentCard = null)
    {
        if (linkCurrentCard != null && MovmentPanel != null)  linkCurrentCard.gameObject.transform.SetParent(MovmentPanel.transform, true);

        clickCardToTrailePanel.SwitchingPanel = switchingPanel;
    }
    private void CalculationResurse(LinkCurrentCard linkCurrentCard, bool isCalculationState)
    {
        (TimeCountMass, TimeCountСargo) = 
            DefineStat.GeneratorState (linkCurrentCard._dataCurrentCardTrailer.CurrentDataCard.Rarity, linkCurrentCard._dataCurrentCardTrailer.CurrentDataCard.level);

        if (isCalculationState)
        {
            CountMass += TimeCountMass;
            CountСargo += TimeCountСargo;
        }
        else
        {
            CountMass -= TimeCountMass;
            CountСargo -= TimeCountСargo;
        }
        CountText.text = CountСargo.ToString();
        
    }
    private void CalculationGoodsToCard(LinkCurrentCard linkCurrentCard)
    {
        var a = DefineStat.GeneratorState(linkCurrentCard._dataCurrentCardTrailer.CurrentDataCard.Rarity, linkCurrentCard._dataCurrentCardTrailer.CurrentDataCard.level);
        linkToElements linkToElementsNewGameObj = linkCurrentCard.GetComponentInChildren<linkToElements>();
        linkCurrentCard._dataCurrentCardTrailer.CurrentDataCard.TotalCountResource = a.Item1;
        linkToElementsNewGameObj.CurrentLoadGoods.text = linkCurrentCard._dataCurrentCardTrailer.CurrentDataCard.CurrentCountResource + " / " + a.Item1.ToString();
    }
    private void CalculationGoods(int CurrentResourse)
    {
        CountText.text = (CountСargo +  CurrentResourse).ToString();
    }
    private void inizializationsCreateCard()
    {
        CreateinWalletTrailer();
        CreateinGarageTrailer();
    }
    private void CloneCode(GameObject CreateCard, LinkCurrentCard LinkCreateCard, SwitchingPanel switchingPanel, bool isCreateText = true)
    {
        ClickCardToTrailePanel clickCardOrigin= null;
        if (LinkCreateCard.UniversalButtonForTrailer.GetComponent<ClickCardToTrailePanel>() == null)
        {
            LinkCreateCard.UniversalButtonForTrailer.gameObject.AddComponent<ClickCardToTrailePanel>().SwitchingPanel = switchingPanel;
            clickCardOrigin = LinkCreateCard.UniversalButtonForTrailer.gameObject.GetComponent<ClickCardToTrailePanel>();
            clickCardOrigin.PanelFromGoods = _panelToLoadingGoods;
        }
        else
            clickCardOrigin = LinkCreateCard.UniversalButtonForTrailer.gameObject.GetComponent<ClickCardToTrailePanel>();

        LinkCreateCard.NameProduct = NameProduct.Trailer;

        CalculationGoodsToCard(LinkCreateCard);
        if (isCreateText)
        {
            AddComponentToCard.AddComponentText(CreateCard, 0, 60); //TODO 1: Добавить активный сетапп у карты
        }
        
        if (switchingPanel == SwitchingPanel.InGarage)
        {
            if (LinkCreateCard._dataCurrentCardTrailer.CurrentDataCard.IsActive)
            {
                var ClonActiveCard = Instantiate(CreateCard, Vector3.zero, Quaternion.identity, _inActivePanel.transform);
                //ClonActiveCard.transform.SetParent(_inActivePanel.transform, true);
                ClonActiveCard.transform.localScale = new Vector3(1, 1, 1);

                TransferPos.PositionZeroCoordinste(ClonActiveCard);

                LinkCurrentCard LinkCardClone = ClonActiveCard.GetComponent<LinkCurrentCard>();

                LinkCreateCard.CurrentSetupText.GetComponent<Text>().text = $"Setup {PlayerData.instanse.instanseSaveCard.CurrentSetupPlayer}";
                LinkCardClone.CurrentSetupText.GetComponent<Text>().text = $"";
                Destroy(LinkCardClone.UniversalButtonForTrailer.gameObject);

                var ClonClickCard = ClonActiveCard.AddComponent<ClickCardToTrailePanel>();
                
                ClonClickCard.SwitchingPanel = SwitchingPanel.Active;
                ClonClickCard.PanelFromGoods = _panelToLoadingGoods;
                clickCardOrigin.enabled = false; // Скрываем скрипт с клик кардом


                InizializatorMainMenu.EventUpdateActiveTrailerCardToMainMenu?.Invoke(ClonActiveCard);
            }
        }
        TransferPos.PositionZeroCoordinste(CreateCard);
    }
    private void CreateinGarageTrailer() 
    {
        var ListCurrentActiveCard = _playerData.instanseSaveCard.ListGarageCardTrailer;
        for (int i = 0; i < ListCurrentActiveCard.Count; i++)
        {
            GameObject CreateCard = Instantiate(_playerData.CurrentInstanseGameObject, Vector3.zero, Quaternion.identity);
            CreateCard.name = "GreateGameObject" + ListCurrentActiveCard[i].name;

            CreateCard.AddComponent<LinkCurrentCard>()._dataCurrentCardTrailer.CurrentDataCard = ListCurrentActiveCard[i];
            var LinkCreateCard = CreateCard.GetComponent<LinkCurrentCard>();

            LinkCreateCard.IndexCard= UnityEngine.Random.Range(0, 10000);
            LinkCreateCard._dataCurrentCardTrailer.CurrentDataCard.indexCard = LinkCreateCard.IndexCard;

            CreateCard.GetComponentInChildren<Image>().sprite = ListCurrentActiveCard[i].Icon;
            CreateCard.transform.SetParent(_inGamePanel.transform);
            CreateCard.transform.localScale = new Vector3(1, 1, 1);

            CreateButton(LinkCreateCard);
            LinkCreateCard.UniversalButtonForTrailer.GetComponent<Image>().sprite = _spriteButtonSetActive;

            CloneCode(CreateCard, LinkCreateCard, SwitchingPanel.InGarage);
            UpdateDisplayToPanel(SwitchingPanel.InGarage, LinkCreateCard.UniversalButtonForTrailer.gameObject.GetComponent<ClickCardToTrailePanel>(), _inGamePanel.gameObject, LinkCreateCard);      
        }
    }
    private void CreateinWalletTrailer()
    {
        var ListCurrentActiveCard = _playerData.instanseSaveCard.ListInWalletCardTrailer;
        for (int i = 0; i < ListCurrentActiveCard.Count; i++)
        {
            GameObject CreateCard = Instantiate(_playerData.CurrentInstanseGameObject, Vector3.zero, Quaternion.identity);
            CreateCard.name = "GreateGameObject" + ListCurrentActiveCard[i].name;

            CreateCard.AddComponent<LinkCurrentCard>()._dataCurrentCardTrailer.CurrentDataCard = ListCurrentActiveCard[i];
            var LinkCreateCard = CreateCard.GetComponent<LinkCurrentCard>();

            LinkCreateCard.IndexCard = UnityEngine.Random.Range(10001, 20000);
            LinkCreateCard._dataCurrentCardTrailer.CurrentDataCard.indexCard = LinkCreateCard.IndexCard;

            CreateCard.GetComponentInChildren<Image>().sprite = ListCurrentActiveCard[i].Icon;
            CreateCard.transform.SetParent(_inWalletPanel.transform);
            CreateCard.transform.localScale = new Vector3(1, 1, 1);

            CreateButton(LinkCreateCard);

            LinkCreateCard.UniversalButtonForTrailer.GetComponent<Image>().sprite = _spriteButtonSetUpload;

            CloneCode(CreateCard, LinkCreateCard, SwitchingPanel.InWallet);
            UpdateDisplayToPanel(SwitchingPanel.InWallet, LinkCreateCard.UniversalButtonForTrailer.gameObject.GetComponent<ClickCardToTrailePanel>(), _inWalletPanel.gameObject, LinkCreateCard);
        }
    }
    public void StartToLoadingGoods()
    {
        if (isLoabingGoods)
            isLoabingGoods = false;
        else
            isLoabingGoods = true;
    }
  }