using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.My_Stocks
{
    public class UiDataMyStoksPanel : MonoBehaviour
    {
        [field: SerializeField] public Text Foods { get; set; }
        [field: SerializeField] public Text Rests { get; set; }
        [field: SerializeField] public Text Parts { get; set; }
        [field: SerializeField] public Text Fuel { get; set; }
        [field: SerializeField] public Text CommonGoods { get; set; }
        [field: SerializeField] public Text RareGoods { get; set; }
        [field: SerializeField] public Text EpicGoods { get; set; }
        [field: SerializeField] public Text LegendaryGoods { get; set; }
        [field: SerializeField] public Text Stuff { get; set; }

        [field: SerializeField] public Text OneContract { get; set; }
        [field: SerializeField] public Text ThreeContract { get; set; }
        [field: SerializeField] public Text SixContract { get; set; }
        [field: SerializeField] public Text NineContract { get; set; }
        [field: SerializeField] public Text TwelveContract { get; set; }

        [field: SerializeField] public Button[] BuyOnMarket { get; set; } = new Button[3];
        [field: SerializeField] public Button BuyOnWarhouseGoods { get; set; }
    }
}