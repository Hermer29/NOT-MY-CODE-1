using Code.MainMenu.Timer;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelUnstake : MonoBehaviour
{
    [field: SerializeField] private GameObject UnstakePanel { get; set; }
    [field: SerializeField] private GridLayoutGroup _gridLayoutGroup { get; set; }
    [field: SerializeField] private List<InstanseTimer> currentTimer { get; set; } = new List<InstanseTimer>();
    [field: SerializeField] private GameObject Panel—onfirmationCancel { get; set; }

    public Action<AddedavePlayerStock> EventRestCellToStake { get; set; }
    public Action<AddedavePlayerStock, int> EventAddToSell { get; set; }
    private AddedavePlayerStock _addedavePlayerStock { get; set; }
    private StakingData _cellStaking { get; set; }
    public void DeletionConfirmation()
    {
        ResetTime(this._addedavePlayerStock);
    }
    public void Unsubscribe()
    {
        if (_cellStaking != null)
        {
            _cellStaking.CloseToStake.onClick.AddListener(() => TemporaryTransfer(_cellStaking.CurrentCellData, _cellStaking));
            _cellStaking.CloseToStake.onClick.RemoveListener(() => TemporaryTransfer(_cellStaking.CurrentCellData, _cellStaking));
            _cellStaking = null;
            _addedavePlayerStock = null;
        }
    }
    private void Awake()
    {
        EventRestCellToStake += ResetTime;
        EventAddToSell += AddCellStaking;
    }
    private void OnDestroy()
    {
        EventRestCellToStake -= ResetTime;
        EventAddToSell -= AddCellStaking;
    }
    private void Start()
    {
        StartStake();
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < currentTimer.Count; i++)
        {
            currentTimer[i].UpdateUiTimerDay();
        }
    }
    private void StartStake()
    {
        var CurrentSavePayment = PlayerData.instanse.instanseSaveAddedPlayerPayment;
        for (int i = 0; i < CurrentSavePayment.AddedavePlayertock.Count; i++)
        {
            AddCellStaking(CurrentSavePayment.AddedavePlayertock[i], i);
        }
    }
    private void AddCellStaking(AddedavePlayerStock addedavePlayerStock, int IndexCell)
    {
        GameObject CurrentSell = Instantiate(PlayerData.instanse.CellToStaking, Vector3.zero, Quaternion.identity);
        CurrentSell.transform.SetParent(_gridLayoutGroup.transform, true);
        CurrentSell.transform.localScale = Vector3.one;

        var StakingData = CurrentSell.GetComponent<StakingData>();
        StakingData.CurrentCellData = addedavePlayerStock;
        StakingData.IndexDepositAndMoney.text = $"Stake {IndexCell+ 1}: {addedavePlayerStock.CurrentPresent}";


        var a = addedavePlayerStock.DataPresent - addedavePlayerStock.CurrentDateTime;
        Debug.Log(a);

        var CurrentTimer = new InstanseTimer(addedavePlayerStock.DataPresent, StakingData.EndTimeToDeposit, 0); //TODO 2: «‡„ÎÛ¯Í‡
        StakingData.instanseTimer = CurrentTimer;
        currentTimer.Add(CurrentTimer);
        StakingData.CloseToStake.onClick.AddListener(() => TemporaryTransfer(StakingData.CurrentCellData, StakingData));
        StakingData.CloseToStake.onClick.RemoveListener(() => TemporaryTransfer(StakingData.CurrentCellData, StakingData));
    }
    private void TemporaryTransfer(AddedavePlayerStock addedavePlayerStock, StakingData CellData)
    {
        this._addedavePlayerStock = null;
        this._addedavePlayerStock = addedavePlayerStock;
        this._cellStaking = CellData;
        Panel—onfirmationCancel.SetActive(true);
       
    }
    private void ResetTime(AddedavePlayerStock addedavePlayerStock)
    {
        var CurrentSlots =  _gridLayoutGroup.GetComponentsInChildren<StakingData>();

        for (int i = 0; i < CurrentSlots.Length; i++)
        {
            if (CurrentSlots[i].CurrentCellData == addedavePlayerStock)
            {
                currentTimer.Remove(CurrentSlots[i].instanseTimer);
                PlayerData.instanse.instanseSaveAddedPlayerPayment.AddedavePlayertock.Remove(addedavePlayerStock);
                PlayerData.instanse.instanseSaveMoneyPlayer.Drive += addedavePlayerStock.InvestedMoney;
                Destroy(CurrentSlots[i].gameObject);      
            }
        }
    }
}
