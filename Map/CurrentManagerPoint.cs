using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Map
{
    public class CurrentManagerPoint : MonoBehaviour // Прикрутить текст к разным слотам.
    {
        public static Action<Point, ContactPointToMap> EventSelectionPoint { get; set; }
        public static Action EventUpdateContracts { get; set; }
        [field: SerializeField] public List<ContactPointToMap> CurrentlistContact { get; set; } = new List<Assets.Code.Map.ContactPointToMap>(10);
        [field: SerializeField] public List<Point> listPoint { get; set; } = new List<Point>(10);
        [field: SerializeField] public List<LookAtTarget> listLookAtTarget { get; set; } = new List<LookAtTarget>(10);
        [SerializeField] private CheckPoint checkPoint { get; set; } = new CheckPoint();
        [SerializeField] private UiDataManagerPoint uiDataManagerPoint = new UiDataManagerPoint();

        public static Action<Point, ContactPointToMap> EventResetPoint { get; set; }
        private Image _currentImagePoint { get; set; }
        public RecentPointToTravel _recentPointToTravel { get; set; } = new RecentPointToTravel();
        [SerializeField] private GameObject Map;
        public void AddRecentPoint()
        {
            bool isCurrentSetApp = false;
            for (int i = 0; i < _recentPointToTravel.LastPointsSetup.Count; i++)
            {
                if (_recentPointToTravel.LastPointsSetup[i].Setup == PlayerData.instanse.instanseSaveCard.CurrentSetupPlayer)
                {
                    _recentPointToTravel.LastPointsSetup[i] = new DataPoint() { Point = listPoint[listPoint.Count - 1], Setup = PlayerData.instanse.instanseSaveCard.CurrentSetupPlayer };
                    isCurrentSetApp = true;
                }
            }
            if (!isCurrentSetApp)
            {
                _recentPointToTravel.LastPointsSetup.Add(new DataPoint() { Point = listPoint[listPoint.Count - 1], Setup = PlayerData.instanse.instanseSaveCard.CurrentSetupPlayer });
            }
        }
        public CheckPoint DateCheckPoint
        {
            get { return checkPoint; }
            set { checkPoint = value; }
        }

        private void Awake()
        {
            EventSelectionPoint += AddPoint;
            EventResetPoint += ResetPoint;
            EventUpdateContracts += UpdateDisplayContracts;
        }
        private void OnDestroy()
        {
            EventSelectionPoint -= AddPoint;
            EventResetPoint -= ResetPoint;
            EventUpdateContracts -= UpdateDisplayContracts;

        }
        private void Start()
        {
            checkPoint.rideSave = new RideSave();
            checkPoint.ContactPoints = CurrentlistContact;
        }
        private void AddPoint(Point Point, ContactPointToMap contactPoints)
        {
            _currentImagePoint = Point.CurrentImagePoint;
           
            if (PlayerData.instanse.DataMap.OnePointWarhouseGoods != null)//&& Point.GetComponent<MapWarhouse>().WareHouseGoodS.GetInstanceID() == PlayerData.instanse.instanseSaveCard.ListActiveCardWareHouseGoodS[0].GetInstanceID())
            {
                var sfdf = Point.GetComponent<MapWarhouse>()?.WareHouseGoodS?.GetInstanceID();             
              
                if (sfdf != null)
                {
                    if (PlayerData.instanse.DataMap.OnePointWarhouseGoods.GetInstanceID() == sfdf)
                    {
                        Debug.Log("Current Point Warhouse" + "Debug Count Point ");
                        return;
                    }
                    
                }
            }
            if ((int) SelectContracts.CurrrentContract <= listPoint.Count)
            {
                StartCoroutine(ManagerMainMenu.instanse.DebugCoroutine("You have reached the point limit of the American contract"));
                return;
            }
            if (_currentImagePoint.color == Color.black || _currentImagePoint.color == Color.blue)
            {
                StartCoroutine(ManagerMainMenu.instanse.DebugCoroutine("You choose a blocked point"));
                return;
            }
            if (_currentImagePoint.color == Color.red)
            {
                ResetToCurrentPoint(Point, contactPoints);
              
                return;
            }
            if (CurrentlistContact.Count == 0)
            {
                var PlayerInstanseSaveCard = PlayerData.instanse.instanseSaveCard;
                bool isContactPoint = false;
                //current setup.
                for (int i = 0; i < _recentPointToTravel.LastPointsSetup.Count; i++)
                {
                    if (_recentPointToTravel.LastPointsSetup[i].Setup == PlayerInstanseSaveCard.CurrentSetupPlayer)
                    {
                        isContactPoint = true;
                        for (int b = 0; b < _recentPointToTravel.LastPointsSetup[i].Point.CurrentContactPoint.CurrentContactsToPoint.Count; b++)
                        {
                            bool isContinuationTheOperation = CheckToPointsContact(_recentPointToTravel.LastPointsSetup[i].Point.CurrentContactPoint, Point, contactPoints);
                            if (isContinuationTheOperation) return;
                        }
                    }
                }
                if (isContactPoint)
                {
                    StartCoroutine(ManagerMainMenu.instanse.DebugCoroutine("Select a point with the last path"));
                    return;
                }
                var AllPointToMap = Map.GetComponentsInChildren<Point>();
                
                if (PlayerInstanseSaveCard.ListActiveCardWareHouseGoodS.Count != 0)
                {
                    Debug.Log("Check To Collision Point");
                    isContactPoint = true;
                    var a = PlayerData.instanse.instanseSaveCard.ListActiveCardWareHouseGoodS[0].Cordinats;
                    for (int i = 0; i < AllPointToMap.Length; i++)
                    {
                        RectTransform PositionPoint =  AllPointToMap[i].GetComponent<RectTransform>();
                        if ((int)PositionPoint.anchoredPosition.x == a[0] && ((int)PositionPoint.anchoredPosition.y) == a[1])
                        {                        
                            bool isContinuationTheOperation = CheckToPointsContact(AllPointToMap[i].CurrentContactPoint, Point, contactPoints);
                            if (isContinuationTheOperation) return;
                        }
                    }
                }
                else
                {
                    for (int h = 0; h < PlayerData.instanse.instanseSaveCard.ListGarageCardWareHouseGoodS.Count; h++)
                    {
                        isContactPoint = true;
                        var CoordinateWarhouse = PlayerData.instanse.instanseSaveCard.ListGarageCardWareHouseGoodS[h].Cordinats;
                        for (int i = 0; i < AllPointToMap.Length; i++) 
                        {
                            RectTransform PositionPoint = AllPointToMap[i].GetComponent<RectTransform>(); 
                            if ((int)PositionPoint.anchoredPosition.x == CoordinateWarhouse[0] && ((int)PositionPoint.anchoredPosition.y) == CoordinateWarhouse[1]) 
                            {
                                bool isContinuationTheOperation = CheckToPointsContact(AllPointToMap[i].CurrentContactPoint, Point, contactPoints);
                                if (isContinuationTheOperation) return;
                            }
                        }
                    }
                }
                if (!isContactPoint)
                {
                    AddSelectPoint(Point,contactPoints,null);
                }
            }
            else
            {
                for (int i = CurrentlistContact.Count - 1; i > -1; i--)
                {
                    for (int b = contactPoints.CurrentContactsToPoint.Count - 1; b > -1; b--)
                    {
                        if (CurrentlistContact[i].gameObject.GetInstanceID() == contactPoints.CurrentContactsToPoint[b].gameObject.GetInstanceID())
                        {
                            AddSelectPoint(Point, contactPoints, CurrentlistContact[i].GetComponentInParent<Point>());
                            return;
                        }

                    }
                }
            }

        }
        private bool CheckToPointsContact(ContactPointToMap CurrentContactPoint,Point Point, ContactPointToMap contactPoints)
        {
            for (int b = 0; b < CurrentContactPoint.CurrentContactsToPoint.Count; b++)
            {
                if (CurrentContactPoint.CurrentContactsToPoint[b].GetComponentInParent<Point>().gameObject.GetInstanceID() == Point.gameObject.GetInstanceID())
                {
                    AddSelectPoint(Point, contactPoints,CurrentContactPoint.GetComponentInParent<Point>());
                    Debug.Log(CurrentContactPoint.GetComponentInParent<Point>().gameObject.transform.position + " " + Point.gameObject.transform.position);
                    return true;
                }

            }
            return false;
        }
        private void AddSelectPoint(Point Point, ContactPointToMap contactPoints, Point TargetPoint)
        {
            CurrentlistContact.Add(contactPoints);
            if (listPoint.IndexOf(Point) < 0)
            {
                listPoint.Add(Point);
            }
            _currentImagePoint.color = Color.red;
            uiDataManagerPoint.UpdsateDisplayText(listPoint.Count);

            if (TargetPoint != null)
            {
                CreatingLineBetweenPoints(TargetPoint, contactPoints);
            }
        }
        private void ResetToCurrentPoint(Point Point, ContactPointToMap contactPoints)
        {
            _currentImagePoint.color = Point.CurrentColorPoint;
            ResetPoint(Point, Point.CurrentContactPoint);
            uiDataManagerPoint.UpdsateDisplayText(listPoint.Count);
        }
        private void CreatingLineBetweenPoints(Point points, ContactPointToMap PointToParentSpawn)
        {
            var b = Instantiate(PlayerData.instanse.MapLine, PointToParentSpawn.GetComponentInParent<Point>().transform.position, Quaternion.identity, PointToParentSpawn.transform);
            b.GetComponent<LookAtTarget>().UpdateTransform(points, PointToParentSpawn.GetComponentInParent<Point>());
            b.GetComponent<Image>().enabled = false;

            var CurrentDecreaseToCard = ControllerMap.instanse.CurrentDecreaseToCard;
     
            var Dustanse = Vector3.Distance(PointToParentSpawn.transform.position, points.transform.position);
            var Сurrentfactor = 1 / CurrentDecreaseToCard;
            var OriginDistanseToObj = Dustanse * Сurrentfactor;
            var RectTransformRoad = b.GetComponent<RectTransform>();
            RectTransformRoad.sizeDelta = new Vector2(10 + OriginDistanseToObj * 13, 50);

            listLookAtTarget.Add(b.GetComponent<LookAtTarget>());
        }
        //Вызывается при успешном прохождении сетапа
        public void BlockPoint()
        {
            AddRecentPoint();
            PointStateTransferfromPointToLockedState();
            for (int i = 0; i < listPoint.Count; i++) //TODO 2: Добавить сохранение точек и их сброс через 24 часа.
            {
                listPoint[i].CurrentImagePoint.color = Color.black;
                if (listPoint.Count - 1 == i)
                {
                    listPoint[i].CurrentImagePoint.color = Color.blue;
                }
            }
            AddPointToSaveSetup();
            ManagerSavePointToMap.instanse.CangesImagePoint();
            ResetPoint(listPoint, CurrentlistContact, true);
            CurrentlistContact.RemoveRange(0, CurrentlistContact.Count);
            listPoint.RemoveRange(0, listPoint.Count);
        }
        private void PointStateTransferfromPointToLockedState()
        {
            Point[] AllPointToMap = Map.GetComponentsInChildren<Point>();
            for (int i = 0; i < AllPointToMap.Length; i++)
            {
                if (AllPointToMap[i].CurrentImagePoint == null) continue;
                
                if (AllPointToMap[i].CurrentImagePoint.color == Color.blue)
                    AllPointToMap[i].CurrentImagePoint.color = Color.black;

            }
        }
        private void AddPointToSaveSetup()
        {
            for (int i = 0; i < listPoint.Count; i++)
            {
                ManagerSavePointToMap.instanse.AddSavePoint(listPoint[i]);
            }
        }
        private void ResetPoint(Point point, ContactPointToMap contactPoints)
        {
            if (point != null) //Eдиничное выполнение 
            {
                ResetPoint(new List<Point>(1) { point },  new List<ContactPointToMap>(1) { contactPoints }, true);

                CurrentlistContact.Remove(contactPoints);
                listPoint.Remove(point);
            }
            else
            {
                ResetPoint(listPoint, CurrentlistContact, false);

                CurrentlistContact.RemoveRange(0, CurrentlistContact.Count);
                listPoint.RemoveRange(0, listPoint.Count);
                
            }
            uiDataManagerPoint.UpdsateDisplayText(listPoint.Count);
        }
        private void ResetPoint(List<Point> points, List<ContactPointToMap> contactPointToMaps, bool isBloke)
        {
            for (int i = 0; i < points.Count; i++)
            {
                Debug.Log(points[i].name + "  CurrentPoint ");
                if (!isBloke)
                {
                    points[i].CurrentImagePoint.color = points[i].CurrentColorPoint;
                }

                for (int b = 0; b < listLookAtTarget.Count; b++)
                {
                    if (listLookAtTarget[b].TargetPoint != null && listLookAtTarget[b].TargetPoint == points[i])
                    {
                        listLookAtTarget[b].TargetPoint = null;
                    }

                    if (listLookAtTarget[b].ParentPoint != null && listLookAtTarget[b].ParentPoint == points[i])
                    {
                        listLookAtTarget[b].ParentPoint = null;
                    }
                }
            }
            for (int i = 0; i < listLookAtTarget.Count; i++)
            {
                if (listLookAtTarget[i].TargetPoint == null || listLookAtTarget[i].ParentPoint == null)
                {
                    var a = listLookAtTarget[i];
                    listLookAtTarget.Remove(listLookAtTarget[i]);
                    Destroy(a.gameObject);
                    i--;
                }
            }
        }
        private void UpdateDisplayContracts()
        {
            uiDataManagerPoint.UpdsateDisplayText(listPoint.Count);
        }
    }
}