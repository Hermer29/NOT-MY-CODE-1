using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DopSpavnTrailer : MonoBehaviour
{
    public static Action EventStartSpavnInMyStocksPanel { get; set; }
    public static Action<GameObject> OnActionPanelGoods { get; set; }
    public static Action<int> EventDeliteobj { get; set; }
    public static Action<Sprite> EventCreateObj { get; set; }
    [field: SerializeField] private RemoveResourseTrailer newTestSSDSD { get; set; } = new RemoveResourseTrailer();
    [field: SerializeField] private GridLayoutGroup GridLayoutGroup { get; set; }
    [field: SerializeField] private GridLayoutGroup GridLayoutGroupMainMenu { get; set; }
    private PlayerData _playerData { get; set; }
    [field: SerializeField] private GameObject PanelLoadingGoods { get; set; }
    [field: SerializeField] private GridLayoutGroup CurrentPanelToActiveTrailer { get; set; }

    private void Awake()
    {
        EventStartSpavnInMyStocksPanel += StartEventMyStocksPanel;

        EventDeliteobj += DeliteObj;
        EventCreateObj += CreateObj;
    }
    private void OnDestroy()
    {
        //Add To Event
    }
    private void Start()
    {
        newTestSSDSD.Start();
        newTestSSDSD.CurrentActiveTrailer = GridLayoutGroup;
        //StartCoroutine(CurrentUpdateCountGoods());
    }
    //private IEnumerator CurrentUpdateCountGoods()
    //{
    //    int i = 0;
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(1f);
    //        newTestSSDSD.IninzializationsGoods();
    //    }
    //}
    private void CreateObj(Sprite Icon)
    {
        var GameObjToTrailerPanel = CurrentPanelToActiveTrailer.GetComponentsInChildren<LinkCurrentCard>();
        for (int i = 0; i < GameObjToTrailerPanel.Length; i++)
        {
            if (GameObjToTrailerPanel[i]._dataCurrentCardTrailer.CurrentDataCard.Icon == Icon)
            {
                SpavnGameObject(GameObjToTrailerPanel[i].gameObject);
                UpdateActiveCardTrailerToMainMenu.EventSpavnTrailerCardMainMenu?.Invoke(Icon);
                return;
            }
        }
    }
    private void DeliteObj(int IndexCard)
    {
        var GameObjToTrailerPanel = CurrentPanelToActiveTrailer.GetComponentsInChildren<LinkCurrentCard>();

                UpdateActiveCardTrailerToMainMenu.EventDeliteCurrentCardToMainMenu?.Invoke(GameObjToTrailerPanel);
    }
    private void StartEventMyStocksPanel()
    {
        var a = CurrentPanelToActiveTrailer.GetComponentsInChildren<LinkCurrentCard>();

        UpdateActiveCardTrailerToMainMenu.CardSpavnToMainMenu?.Invoke(GridLayoutGroup);

    }
    private void SpavnGameObject(GameObject CurrentGameObject)
    {
        //GameObject NewGameObject = Instantiate(CurrentGameObject, Vector3.zero, Quaternion.identity);
        //NewGameObject.name = CurrentGameObject.name;
        //NewGameObject.transform.SetParent(GridLayoutGroup.transform);
        //NewGameObject.transform.localScale = new Vector3(1, 1, 1);
        //Destroy(NewGameObject.GetComponentInChildren<ClickCardToTrailePanel>());
        //LinkCurrentCard  linkCurrentCardNewGameObj = NewGameObject.GetComponent<LinkCurrentCard>();
        //var a =  DefineStat.GeneratorState(linkCurrentCardNewGameObj._dataCurrentCardTrailer.CurrentDataCard.Rarity, linkCurrentCardNewGameObj._dataCurrentCardTrailer.CurrentDataCard.level);

        //linkToElements linkToElementsNewGameObj = NewGameObject.GetComponentInChildren<linkToElements>();
        

        //NewGameObject.AddComponent<DataGoods>().TotalCountResource = a.Item1;
        //linkToElementsNewGameObj.CurrentLoadGoods.text = "0 / " + a.Item1.ToString();

        //linkToElementsNewGameObj.LoadButton.gameObject.SetActive(true);
        //linkToElementsNewGameObj.LoadButton.onClick.AddListener(() => PanelLoadingGoods.SetActive(true));
        //linkToElementsNewGameObj.LoadButton.onClick.AddListener(() => OnActionPanelGoods?.Invoke(NewGameObject));
    }
}
public static class InizializationGoods
{
    public static void StartInizializationGoods(PlayerData _playerData, Text CommonGoods, Text RareGoods, Text EpicGoods, Text LegendaryGoods)
    {
        try
        {
            CommonGoods.text = _playerData.instanseSavePlayerState.CommonGoods.ToString();
            RareGoods.text = _playerData.instanseSavePlayerState.RareGoods.ToString();

            EpicGoods.text = _playerData.instanseSavePlayerState.EpicGoods.ToString();
            LegendaryGoods.text = _playerData.instanseSavePlayerState.legendaryGoods.ToString();
        }
        catch (Exception)
        {
            throw;
        }

    }

}
