using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "DataDriver", menuName = "ScriptableObject/DataDriver")]
public class Driver : ScriptableObject
{
    public Sprite Icon;
    public int indexCard; // От 0 до 500.
    public int MaxEnergy;
    public int CurrentEnergy = 10;
    public int MaxHunger;
    public int CurrentHunger = 10;
    public Rarity Rarity;
    public bool IsActive;
    public int CurrentSetup;
    public int Travel = 0;

    public int SkillLevel;
    public SkillPlayer SkillPlayer;
    public int PercentValueSkill;
    public float Income;
}
