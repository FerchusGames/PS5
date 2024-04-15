using System;
using UnityEngine;

public class DeliverObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            GameManager.Instance.AddScore(1);
            Destroy(other.gameObject);
        }
    }
}

