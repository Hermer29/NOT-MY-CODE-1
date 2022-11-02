using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
[System.Serializable]
class UiDataManagerPoint
{
    public TMP_Text CurrentContracts;
    [SerializeField] private TMP_Text CurrentCountPoint;
    public void UpdsateDisplayText(int CurrentPoint)
    {
        CurrentCountPoint.text = $"{CurrentPoint} / {(int) SelectContracts.CurrrentContract}";       
    }
}