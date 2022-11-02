using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "DataTruck", menuName = "ScriptableObject/DataTruck")]
public class Truck : ScriptableObject
{
    public Sprite Icon;
    public int indexCard; //ŒÚ 500 ‰Ó 1000
    public int MaxParts;
    public int CurrentParts = 10;
    public int MaxFuel;
    public int CurrentFuel = 10;
    public int —arrying—apacity;
    public Rarity Rarity;

    [field: SerializeField] public bool IsActive { get; set; }
    public int CurrentSetup;
    public int Travel = 0;

    public int SkillLevel;
}
