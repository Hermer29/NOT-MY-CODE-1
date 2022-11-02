using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Code.Map;
using UnityEngine.UI;

public class Point : MonoBehaviour, IPointerClickHandler
{
    public static Action<GameObject> EventClick;
    [field: SerializeField] public ContactPointToMap CurrentContactPoint { get; private set; }
    public Color CurrentColorPoint { get; private set; }
    private MapWarhouse _mapWarhouse { get; set; }
    [field: SerializeField] public Image CurrentImagePoint { get; set; }
    public int CountClick { get; set; }
    private void Start()
    {
        CurrentContactPoint = GetComponentInChildren<ContactPointToMap>();
        _mapWarhouse = GetComponent<MapWarhouse>();
        if (CurrentImagePoint != null)  CurrentColorPoint = CurrentImagePoint.color;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");

        CurrentManagerPoint.EventSelectionPoint.Invoke(this, CurrentContactPoint);
    }
}

