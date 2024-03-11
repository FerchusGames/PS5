using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PS5.Core;


namespace PS5.Objects
{
    public class ObjectsLose : MonoBehaviour
    {
        private int objectsFell;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Food"))
            {
                Destroy(other.gameObject);
                objectsFell++;
            }
        }
        
        private void Update()
        {
            if (objectsFell > 2)
            {
                GameManager.Instance.OnLose();
                GameManager.Instance.Save();
            }
        }
    }
}
