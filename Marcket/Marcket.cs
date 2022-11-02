using UnityEngine;

public enum ProductMarcet
{
    CurrentValueSlots1, CurrentValueSlots2, CurrentValueSlots3, CurrentValueSlots4
}
public class Marcket
{
    private const int _valueConst = 1;
    private MarcketData _marcketData;
    private MarcketUpdateUI _marcketUpdateUI;
    public  Marcket(MarcketData marcketData, MarcketUpdateUI marcketUpdateUI)
    {
        _marcketData = marcketData;
        _marcketUpdateUI = marcketUpdateUI;
    }
    public void MethodsPlus( ref int ValueChanges)
    {
        if (ValueChanges >= 0)
        {
            ValueChanges += _valueConst;
            UpdateCall(_marcketData);
        }     
    }
    public void MethodsMinus(ref int ValueChanges)
    {
        if (ValueChanges != 0)
        {
            ValueChanges += -_valueConst;
            UpdateCall(_marcketData);
        }
    }

    public void ResetData(MarcketData marcketData)
    {
        marcketData.ResetData();
        UpdateCall(_marcketData);
    }
    public void UpdateCall(MarcketData marcketData)
    {
        _marcketUpdateUI.EventUpdateDisplayUIMarcket?.Invoke(marcketData.CurrentValueFood, marcketData.CurrentValueRest, marcketData.CurrentValueParts,
            marcketData.CurrentValueFuel, marcketData.CurrentValueStuff, marcketData.CurrentValueCommonGoods, marcketData.CurrentValueRareGoods,
             marcketData.CurrentValueEpicGoods, marcketData.CurrentValueLegendaryGoods,
             marcketData.CurrentValueOneClockContracts, marcketData.CurrentValueThreeClockContracts, marcketData.CurrentValueSixClockContracts,
             marcketData.CurrentValueNineClockContracts, marcketData.CurrentValueTwelveClockContracts);
    }
}


