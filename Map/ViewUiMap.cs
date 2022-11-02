using UnityEngine;
using UnityEngine.UI;

namespace Code.Map
{
    public class ViewUiMap : MonoBehaviour
    {
        public static ViewUiMap instanse;
        public void Start()
        {
            if (instanse != null)
            {
                Destroy(instanse);
            }
            instanse = this;
        }
        public void UpdsateDisplayText(Text CurrentCount, int CurrentPoint, int MaxPoint)
        {
            CurrentCount.text = $"{CurrentCount} / {MaxPoint}";
        }
    }
}