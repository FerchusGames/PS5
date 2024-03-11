using System;
using UnityEngine;
using PS5.Core;

namespace PS5.Deliveries
{
    public class DeliverObject : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Delivery"))
            {
                GameManager.Instance.AddScore(1);
                Destroy(other.gameObject);
            }
        }

    }
}
