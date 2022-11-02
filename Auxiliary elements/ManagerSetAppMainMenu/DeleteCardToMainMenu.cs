using UnityEngine;
using UnityEngine.UI;

public static class DeleteCardToMainMenu
{
    public static void Delete(GameObject ParentTransform)
    {
        GameObject.Destroy(ParentTransform.GetComponentInChildren<Image>());
    }
}


