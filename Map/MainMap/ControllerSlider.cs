using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Code.Map.MainMap
{
    public class ControllerSlider : MonoBehaviour, IPointerEnterHandler
    {
        [field: SerializeField] private Slider _slider { get; set; }
        [field: SerializeField] private GameObject _contentMap { get; set; }

        private RectTransform _contentTransform;
        private Vector3 TargetPointToCentralMap;
        private void Awake()
        {
            _contentTransform = _contentMap.GetComponent<RectTransform>();
        }
        public void ChangesSlider(float CurrentValue)
        {
            _contentMap.transform.localScale = new Vector3(CurrentValue, CurrentValue, 1);
  
            var a = _contentMap.GetComponent<RectTransform>();
            a.anchoredPosition = new Vector2(TargetPointToCentralMap.x * CurrentValue, TargetPointToCentralMap.y * CurrentValue);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            Vector3 contentScale = _contentTransform.localScale;
            TargetPointToCentralMap = new Vector2(_contentTransform.anchoredPosition.x / contentScale.x, _contentTransform.anchoredPosition.y / contentScale.y);
            Debug.Log("Welcom To New Count" + _contentTransform.anchoredPosition.x.ToString() + "   " + _contentTransform.anchoredPosition.y.ToString());
        }
    }
}