using UnityEngine;

namespace Assets.Code.CameraControllerToMap
{
    public enum PointContact
    {
        Left, Right, Top, Bottom
    }
    public class ContactMapPointToCentralElement : MonoBehaviour
    {
        [SerializeField] public PointContact pointContact;
        GameObject Cube;
        BoxCollider BoxCube;
        public void Inizialization(GameObject CentralPosition)
        {
            Cube = CentralPosition;

            //GetCollider();
        }
        //public void GetCollider()
        //{
        //    BoxCube = Cube.GetComponent<BoxCollider>();
        //}
        private void FixedUpdate()
        {
            RaycastHit hit;
            Ray ray = new Ray(gameObject.transform.position, gameObject.transform.forward);
            Debug.DrawLine(gameObject.transform.position, gameObject.transform.forward * 100, Color.red);

            if (Physics.Raycast(ray, out hit, 100, 3))
            {
               // Debug.Log("Луч");

                // var a = new GameObject("CreateNew");

                if (Cube == null)
                {
                    Debug.Log("");
                    return;
                }
                if (hit.transform.gameObject.GetInstanceID() == Cube.GetInstanceID())
                {
                    Debug.Log("Кaсание " + pointContact);
                }
            }
        }
    }
}