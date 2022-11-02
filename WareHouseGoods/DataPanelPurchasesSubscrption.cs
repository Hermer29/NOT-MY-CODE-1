using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.WareHouseGoods
{
    public class DataPanelPurchasesSubscrption : MonoBehaviour
    {
        [field: SerializeField] public GameObject PanelSubscription { get; set; } //Написано: желаете приобрести производство товаров?
        [field: SerializeField] public Button BuySubscription { get; set; }
        [field: SerializeField] public Text PriceSubscription { get; set; } //Пока будет дефолтная цена в виде 100 монет Drive
        [field: SerializeField] public Text CountDaySubscription { get; set; }
        [field: SerializeField] public Rarity CurrentRarity { get; set; }
    }
}