using Assets.Code.StaticClass;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.CameraControllerToMap
{
    public class TestCamera : MonoBehaviour
    {
        Camera _camera;
        public RectTransform _rectTransform;
        public GameObject SpawnPoint;
        private BoxCollider BoxCollider;
        private GameObject CentralCube;


        [SerializeField] private ContactMapPointToCentralElement[] _contactMapPointToCentralElements;

        [SerializeField] public List<ContactMapPointToCentralElement> ContactPointToCentralCursor = new List<ContactMapPointToCentralElement>();
        private void Awake()
        {
            _camera = GetComponent<Camera>();

            CreateCentralCursor();
           //var gfgf = new Color();
           // gfgf.a = 0;

        }
        private void FixedUpdate()
        {
            RaycastHit hit;
            Ray ray = new Ray(gameObject.transform.position, gameObject.transform.forward);
            Debug.DrawLine(gameObject.transform.position, gameObject.transform.forward * 100, Color.red);
            CentralCube.transform.position = new Vector3(CentralCube.transform.position.x, CentralCube.transform.position.y, 10);
            if (Physics.Raycast(ray, out hit, 100,3))
            {
                //Debug.Log("Луч");
               
            }
          
        }
        private void CreateCentralCursor()
        {
            CentralCube = new GameObject("CreateNew");
            CentralCube.transform.position = transform.position;
            CentralCube.transform.SetParent(SpawnPoint.transform);
            //var a = CentralCube.AddComponent<Image>().color = gfgf;
            CentralCube.AddComponent<RectTransform>();
            BoxCollider = CentralCube.AddComponent<BoxCollider>();
            BoxCollider.size = new Vector3(3, 3, 10);

            TransferPos.PositionZeroCoordinste(CentralCube);
         
            //Подгонка
            var a = CentralCube.GetComponent<RectTransform>();

            for (int i = 0; i < _contactMapPointToCentralElements.Length; i++)
            {
                _contactMapPointToCentralElements[i].Inizialization(CentralCube);
            }
        }
        //private void LateUpdate()
        //{
        //    for (int i = 0; i < ContactPointToCentralCursor.Count; i++)
        //    {
        //        TransferMap(ContactPointToCentralCursor[i].pointContact);
        //    }
        //    ContactPointToCentralCursor.RemoveRange(0, ContactPointToCentralCursor.Count);
        //}
        private void TransferMap(PointContact pointContact)
        {
            var CurrentTransformTOMap = _rectTransform.anchoredPosition;
            switch (pointContact)
            {
                case PointContact.Left:
                    _rectTransform.anchoredPosition = new Vector2(CurrentTransformTOMap.x + 3, CurrentTransformTOMap.y);
                    break;
                case PointContact.Right:
                    _rectTransform.anchoredPosition = new Vector2(CurrentTransformTOMap.x - 3, CurrentTransformTOMap.y);
                    break;
                case PointContact.Top:
                    _rectTransform.anchoredPosition = new Vector2(CurrentTransformTOMap.x, CurrentTransformTOMap.y - 3);
                    break;
                case PointContact.Bottom:
                    _rectTransform.anchoredPosition = new Vector2(CurrentTransformTOMap.x, CurrentTransformTOMap.y + 3);
                    break;
            }
        }
        [ContextMenu("TransferPoint")]
        private void TransferPoint()
        {
            _rectTransform.transform.position = CentralCube.transform.position;
        }
    }
}