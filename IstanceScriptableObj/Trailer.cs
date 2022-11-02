using UnityEngine;


[CreateAssetMenu(fileName = "DataTrailer", menuName = "ScriptableObject/DataTrailer")]
[System.Serializable]
public class Trailer : ScriptableObject, IDataGoods
{
    public Sprite Icon;
    public Rarity Rarity;
    public int level;
    public int LoadCapacity;
    public int indexCard;

    public bool IsActive;
    public int CurrentSetApp;
    public int Travel = 0;
    [Field: SerializeField] public int CommonGoods { get; set; }
    [Field: SerializeField] public int RareGoods { get; set; }
    [Field: SerializeField] public int EpicGoods { get; set; }
    [Field: SerializeField] public int LegendaryGoods { get; set; }

    [Field: SerializeField] public int TotalCountResource { get; set; }
    [Field: SerializeField] public int CurrentCountResource { get; set; }
}
