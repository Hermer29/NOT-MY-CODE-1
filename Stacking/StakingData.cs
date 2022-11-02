using Code.MainMenu.Timer;
using UnityEngine;
using UnityEngine.UI;

public class StakingData : MonoBehaviour
{
    [field: SerializeField] public Image BackgrountIcone { get; set; }
    [field: SerializeField] public Text IndexDepositAndMoney { get; set; }
    [field: SerializeField] public Text EndTimeToDeposit { get; set; }
    [field: SerializeField] public AddedavePlayerStock CurrentCellData { get; set; }
    [field: SerializeField] public InstanseTimer instanseTimer { get; set; }
    [field: SerializeField] public Button CloseToStake { get; set; }
}