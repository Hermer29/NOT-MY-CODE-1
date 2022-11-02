using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.StaticClass
{
    public static class TransferPos
    {
        public static void TransferToPointToMap(RectTransform Map, Vector2 Coordinate, Slider slider)
        {
            Map.anchoredPosition = Coordinate;
            slider.value =1f;
        }
        public static void TransferToPointToMap(RectTransform Map, int[] Coordinate, Slider slider)
        {
            Map.anchoredPosition = new Vector2(Coordinate[0], Coordinate[1]);
            slider.value = 1f;
        }
        public static void PositionZeroCoordinste(GameObject gameObject)
        {
            var a = gameObject.GetComponent<RectTransform>();
            a.anchoredPosition3D = new Vector3(a.anchoredPosition.x, a.anchoredPosition.y, 0);
        }
    }
}
