using Assets.Code.StaticClass;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateActiveCardTrailerToMainMenu: MonoBehaviour
{
    public static Action<GridLayoutGroup> CardSpavnToMainMenu { get; set; }
    public static Action<LinkCurrentCard[]> EventDeliteCurrentCardToMainMenu { get; set; }
    public static Action<Sprite> EventSpavnTrailerCardMainMenu { get; set; }

    public GridLayoutGroup _gridLayoutGroupMainMenuActivePlayer;

    private GridLayoutGroup CurrentgridToActiveTrailer;

    public void Start()
    {
        EventDeliteCurrentCardToMainMenu += DeliteGameObjTrailer;
        EventSpavnTrailerCardMainMenu += SpavnToAddCardTrailer;
    }

    private void SpavnToStartTrailer()
    {
        this.CurrentgridToActiveTrailer = CurrentgridToActiveTrailer;
        var a = CurrentgridToActiveTrailer.GetComponentsInChildren<LinkCurrentCard>();
        for (int i = 0; i < a.Length; i++)
        {
            GameObject NewCreateGameObject = GameObject.Instantiate(a[i].gameObject, Vector3.zero, Quaternion.identity, _gridLayoutGroupMainMenuActivePlayer.transform);
            //NewCreateGameObject.transform.SetParent(_gridLayoutGroupMainMenuActivePlayer.transform, true);
            NewCreateGameObject.GetComponent<linkToElements>().LoadButton.gameObject.SetActive(false);
            NewCreateGameObject.GetComponent<linkToElements>().CurrentLoadGoods.gameObject.SetActive(false);
            NewCreateGameObject.transform.localScale = Vector3.one;
            var playerData = PlayerData.instanse;
            NewCreateGameObject.AddComponent<Image>().sprite = playerData.IconcToMainMenu;
              TransferPos.PositionZeroCoordinste(NewCreateGameObject);
        }
    }
    private void SpavnToAddCardTrailer(Sprite icon)
    {
        var a = CurrentgridToActiveTrailer.GetComponentsInChildren<LinkCurrentCard>();
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i]._dataCurrentCardTrailer.CurrentDataCard.Icon == icon)
            {
                GameObject NewCreateGameObject = GameObject.Instantiate(a[i].gameObject, Vector3.zero, Quaternion.identity, _gridLayoutGroupMainMenuActivePlayer.transform);
                //NewCreateGameObject.transform.SetParent(_gridLayoutGroupMainMenuActivePlayer.transform, true);
                NewCreateGameObject.GetComponent<linkToElements>().LoadButton.gameObject.SetActive(false);
                NewCreateGameObject.GetComponent<linkToElements>().CurrentLoadGoods.gameObject.SetActive(false);
                NewCreateGameObject.transform.localScale = Vector3.one;
                var playerData = PlayerData.instanse;
                NewCreateGameObject.AddComponent<Image>().sprite = playerData.IconcToMainMenu;
                TransferPos.PositionZeroCoordinste(NewCreateGameObject);
            }
        }
    }
    private void DeliteGameObjTrailer(LinkCurrentCard[] linkCurrentCards)
    {
        var a = _gridLayoutGroupMainMenuActivePlayer.GetComponentsInChildren<Image>();
        List<Image> IconTrailerToMainMenu = new List<Image>();
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] == PlayerData.instanse.IconcToMainMenu)
            {
                continue;
            }
            bool isTrue = false;
            for (int b = 0; b < linkCurrentCards.Length; b++)
            {
                if (linkCurrentCards[b]._dataCurrentCardTrailer.CurrentDataCard.Icon != a[i].sprite)
                {
                    isTrue = true;
                }
            }
            if (isTrue == false)
            {
                IconTrailerToMainMenu.Add(a[i]);
            }
           
        }
        for (int i = 0; i < IconTrailerToMainMenu.Count; i++)
        {
            GameObject.Destroy(a[i]);
        }
    }
}