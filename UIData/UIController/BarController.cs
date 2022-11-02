using System;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour //Сделать активацию когда карта оказалась в активном слоте
{
    [SerializeField] private Image[] BackImageBar;
    [SerializeField] private Image[] TopImageBar;
    public Action<int, int> EventUpdateDisplayDar { get; set; }
    private void Awake()
    {
        EventUpdateDisplayDar += UpdateDisplayCurrentBar;
    }
    private void OnDestroy()
    {
        EventUpdateDisplayDar -= UpdateDisplayCurrentBar;
    }

    private void UpdateDisplayCurrentBar(int constValue, int CountCurrentValue)
    {
        for (int i = 0; i < constValue; i++)
        {
            BackImageBar[i].gameObject.SetActive(true);
        }

        for (int i = constValue; i < BackImageBar.Length; i++)
        {
            BackImageBar[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < CountCurrentValue; i++)
        {
            TopImageBar[i].gameObject.SetActive(true);
        }

        for (int i = CountCurrentValue; i < TopImageBar.Length; i++)
        {

            TopImageBar[i].gameObject.SetActive(false);
        }
    }
}
