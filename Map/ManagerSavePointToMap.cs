using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Map
{
    //TODO 2: В дальнейшем добавить сброс по истечению таймера у точки.
    public class ManagerSavePointToMap : MonoBehaviour
    {
        public static ManagerSavePointToMap instanse;
        public GameObject MapContextParent;
        public Action EventSwitchingSetup { get; set; }

        [SerializeField] private List<SavePoint> list = new List<SavePoint>();

        private Point[] PointsToMap;
        private void Awake()
        {
            EventSwitchingSetup += UpdatePointAtSwitchingSetup;

            if (instanse != null) Destroy(instanse);
            instanse = this;

            PointsToMap =  MapContextParent.GetComponentsInChildren<Point>();
        }
        private void OnDestroy()
        {
            EventSwitchingSetup -= UpdatePointAtSwitchingSetup;
        }
        public void AddSavePoint(Point Point)
        {
            list.Add( new SavePoint { CurrentPoint = Point, colorPoint = Point.CurrentImagePoint.color, Setup = (byte)PlayerData.instanse.instanseSaveCard.CurrentSetupPlayer});
        }
        public void CangesImagePoint()
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].CurrentPoint.CurrentImagePoint.color != list[i].colorPoint)
                {
                    var a = list[i];
                    a.colorPoint = list[i].CurrentPoint.CurrentImagePoint.color;
                    list[i] = a;
                }
            }
        }
        private void UpdatePointAtSwitchingSetup()
        {
            OndisableToAllPoint();
            var a = PlayerData.instanse.instanseSaveCard.CurrentSetupPlayer;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Setup == a)
                {
                    for (int b = 0; b < PointsToMap.Length; b++)
                    {
                        if (list[i].CurrentPoint.transform.position == PointsToMap[b].transform.position)
                        {
                            list[i].CurrentPoint.CurrentImagePoint.color = list[i].colorPoint;
                        }
                       
                    }
                }    
            }
        }
        private void OndisableToAllPoint()
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].CurrentPoint.CurrentImagePoint.color = list[i].CurrentPoint.CurrentColorPoint;
            }
        }
    }
    [System.Serializable]
    public struct SavePoint
    {
        [field:SerializeField]public Point CurrentPoint { get; set; }
        [field: SerializeField] public Color colorPoint { get; set; }
        [field: SerializeField] public byte Setup { get; set; }
    }
}