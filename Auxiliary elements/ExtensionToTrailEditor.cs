using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtensionToTrailEditor : MonoBehaviour
{
    [field: SerializeField] private List<Driver> _drivers { get; set; }
    [field: SerializeField] private List<Truck> _trucks { get; set; }
    [field: SerializeField] private List<Trailer> _trailers { get; set; }

    [ContextMenu("ResetToSetApp")]
    private void ResetToSetApp()
    {
        for (int i = 0; i < _drivers.Count; i++)
        {
            _drivers[i].CurrentSetup = 0;
            _drivers[i].IsActive = false;
        }
        for (int i = 0; i < _trucks.Count; i++)
        {
            _trucks[i].CurrentSetup = 0;
            _trucks[i].IsActive = false;
        }
        for (int i = 0; i < _trailers.Count; i++)
        {
            _trailers[i].CurrentSetApp = 0;
            _trailers[i].IsActive = false;
        }
    }

    //[ContextMenu("ResetToTrevel")]
    private void ResetCardTravel()
    {
        for (int i = 0; i < _drivers.Count; i++)
        {
            _drivers[i].Travel = 0;
        }
        for (int i = 0; i < _trucks.Count; i++)
        {
            _trucks[i].Travel = 0;
        }
        for (int i = 0; i < _trailers.Count; i++)
        {

            _trailers[i].Travel = 0;
        }
    }
    //[ContextMenu("ReturnToDefoltState")]
    private void ReturnToDefoltState()
    {
        for (int i = 0; i < _drivers.Count; i++)
        {
            _drivers[i].CurrentEnergy = _drivers[i].MaxEnergy;
            _drivers[i].CurrentHunger = _drivers[i].MaxHunger;
        }
        for (int i = 0; i < _trucks.Count; i++)
        {
            _trucks[i].CurrentFuel = 1000; // _trucks[i].MaxFuel;
            _trucks[i].CurrentParts = 1000; //_trucks[i].MaxParts;
        }
    }

    //Сравнение по картинки
    [field: SerializeField] private Sprite InamesOne { get; set; }
    [field: SerializeField] private Sprite ImageTwo { get; set; }

}
