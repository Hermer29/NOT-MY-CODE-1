using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Map
{
    public class ContactPointToMap : MonoBehaviour
    {
        [field: SerializeField] public List<ContactPointToMap> CurrentContactsToPoint { get; set; } = new List<ContactPointToMap>(5);

        private void Start()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var ContactPoint = collision.GetComponentInChildren<ContactPointToMap>();

            if (ContactPoint != null)
            {
               var IndexContactPoint = CurrentContactsToPoint.IndexOf(ContactPoint);
                if (!(IndexContactPoint >=0))
                {
                    CurrentContactsToPoint.Add(ContactPoint);
                }
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            var ContactPoint = collision.GetComponentInChildren<ContactPointToMap>();

            if (ContactPoint != null)
            {
                CurrentContactsToPoint.Remove(ContactPoint);
            }
        }
    }
}