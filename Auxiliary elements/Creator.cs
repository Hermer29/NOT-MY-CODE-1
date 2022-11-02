using UnityEngine;
using UnityEngine.UI;

public class Creator
{
    public void Create(int CurrentSetAppCard, Sprite ImageCard, GameObject ParentTransform)
    {
        if (CurrentSetAppCard == PlayerData.instanse.instanseSaveCard.CurrentSetupPlayer)
        {
            var a = new GameObject();
            a.AddComponent<Image>().sprite = ImageCard;
            a.transform.SetParent(ParentTransform.transform, true);
            a.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public void Create(Sprite ImageCard, GameObject ParentTransform)
    {
        var a = new GameObject();
        a.AddComponent<Image>().sprite = ImageCard;
        a.transform.SetParent(ParentTransform.transform, true);
        a.transform.position = ParentTransform.transform.position;
        a.transform.localScale = new Vector3(1, 1, 1);
        a.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 300);
        
    }
}
