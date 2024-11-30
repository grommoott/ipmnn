using UnityEngine;
using System;

namespace Helpers
{
    public class Trigger : MonoBehaviour
    {
        private Collider _collider;

        public Action<Collider> OnEnter;
        public Action<Collider> OnExit;
        public Action<Collider> OnStay;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }


        private void OnTriggerEnter(Collider other)
        {
            if (OnEnter == null)
            {
                return;
            }
            OnEnter.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (OnExit == null)
            {
                return;
            }
            OnExit.Invoke(other);
        }

        private void OnTriggerStay(Collider other)
        {
            if (OnStay == null)
            {
                return;
            }
            OnStay.Invoke(other);
        }
    }
}
