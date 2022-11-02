using Assets.Code.Ui;
using UnityEngine;
using Assets.Code.Map;
using Assets.Code.WareHouseGoods;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private SaveMoneyPlayer _saveMoneyPlayer = new SaveMoneyPlayer();
    [SerializeField] private SavePlayerState _savePlayerStats = new SavePlayerState();
    [SerializeField] private CardSave _cardSave = new CardSave();
    [SerializeField] private RideSave _rideSave = new RideSave();
    [SerializeField] private SaveAddedPlayerPayment _saveAddedPlayerPayment = new SaveAddedPlayerPayment();
    [SerializeField] private UpdateMoneyToDisplay _updateMoneyToDisplay = new UpdateMoneyToDisplay();
    [field: SerializeField] public DataMap DataMap { get; set; } = new DataMap();
    public SaveMoneyPlayer instanseSaveMoneyPlayer { get => _saveMoneyPlayer; private set => _saveMoneyPlayer = value; }
    public SavePlayerState instanseSavePlayerState { get => _savePlayerStats; private set => _savePlayerStats = value; }
    public CardSave instanseSaveCard { get => _cardSave; private set => _cardSave = value; }
    public RideSave instanseRideSave { get => _rideSave; private set => _rideSave = value; }
    public SaveAddedPlayerPayment instanseSaveAddedPlayerPayment { get => _saveAddedPlayerPayment; private set => _saveAddedPlayerPayment = value; }
    public static PlayerData instanse { get; set; }

    public GameObject CurrentInstanseGameObject;
    public GameObject CurrentActiveCard;
    public GameObject CurrentActiveSetAppText;
    public Sprite IconcToMainMenu;
    public GameObject MapLine;
    public GameObject UniversalButtonForTrailer;
    public IconSpriteToWarhouseGoods iconSpriteToWarhouseGoods;

    [field: SerializeField] public GameObject CellToStaking { get; set; }
    private void Awake()
    {
        if (instanse != null) Destroy(instanse);

        instanse = this;
        _updateMoneyToDisplay.StartEvent();
        instanseSaveMoneyPlayer.Drive +=0;
    }
    private void OnDestroy()
    {
        _updateMoneyToDisplay.OnDestroy();
    }
   /* [ContextMenu("SavePlayerState")]
    public void SavePlayerState()
    {
        ButtonClassSave.SaveToPlayerPrefs<SaveMoneyPlayer>(instanseSaveMoneyPlayer, "instanseSaveMoneyPlayer");
        ButtonClassSave.SaveToPlayerPrefs<SavePlayerState>(instanseSavePlayerState, "instanseSavePlayerState");
        ButtonClassSave.SaveToPlayerPrefs<CardSave>(instanseSaveCard, "instanseSaveCard");
    }
    [ContextMenu("LoadPlayerStats")]
    public void loagingPlayerState()
    {
        instanseSaveMoneyPlayer = ButtonClassSave.LoadFromPlayerPrefs<SaveMoneyPlayer>(instanseSaveMoneyPlayer, "instanseSaveMoneyPlayer");
        instanseSavePlayerState = ButtonClassSave.LoadFromPlayerPrefs<SavePlayerState>(instanseSavePlayerState, "instanseSavePlayerState");
        instanseSaveCard = ButtonClassSave.LoadFromPlayerPrefs<CardSave>(instanseSaveCard, "instanseSaveCard");
    }*/
}
