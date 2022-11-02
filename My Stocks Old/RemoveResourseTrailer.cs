using UnityEngine.UI;

[System.Serializable]
public class RemoveResourseTrailer
{
    public GridLayoutGroup CurrentActiveTrailer;

    PlayerData _playerData;
    public void Start()
    {
        _playerData = PlayerData.instanse;
    }
    private void ResetGoodsToNull()
    {
        var a = CurrentActiveTrailer.GetComponentsInChildren<LinkCurrentCard>();
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i]._dataCurrentCardTrailer.CurrentDataCard.CurrentCountResource != 0)
            {
                var b = a[i]._dataCurrentCardTrailer.CurrentDataCard;

                b.CommonGoods = 0;
                b.RareGoods = 0;
                b.EpicGoods = 0;
                b.LegendaryGoods = 0;

                b.CurrentCountResource = 0;
                a[i].GetComponent<linkToElements>().CurrentLoadGoods.text = "0 / " + b.TotalCountResource;
            }
        }
    }
    public int CheckResourse()
    {
       var a = ResetGoodsInActiveSetupTrailer(true);
       return a.CountCommonGoods + a.CountRareGoods + a.CountEpicGoods + a.CountLegendaryGoods;
    }
    public (int CountCommonGoods, int CountRareGoods, int CountEpicGoods, int CountLegendaryGoods) ResetGoodsInActiveSetupTrailer(bool isCheck = false ) 
    {
        int CountCommonGoods = 0;
        int CountRareGoods = 0;
        int CountEpicGoods = 0;
        int CountLegendaryGoods = 0;

        var a = CurrentActiveTrailer.GetComponentsInChildren<LinkCurrentCard>();
        if (a != null)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i]._dataCurrentCardTrailer.CurrentDataCard.IsActive == true &&
                    a[i]._dataCurrentCardTrailer.CurrentDataCard.CurrentSetApp == PlayerData.instanse.instanseSaveCard.CurrentSetupPlayer &&
                    a[i]._dataCurrentCardTrailer.CurrentDataCard.CurrentCountResource != 0 )
                {
                    var b = a[i]._dataCurrentCardTrailer.CurrentDataCard;

                    CountCommonGoods = b.CommonGoods;
                    CountRareGoods = b.RareGoods;
                    CountEpicGoods = b.EpicGoods;
                    CountLegendaryGoods = b.LegendaryGoods;
                }
            }
            if (!isCheck)
            {
                ResetGoodsToNull();
            }
            return (CountCommonGoods, CountRareGoods, CountEpicGoods, CountLegendaryGoods);
        }
        else
        {
            return (0, 0, 0, 0);
        }
    }
}
