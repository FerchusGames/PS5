using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnProps : MonoBehaviour
{
   [SerializeField] private GameObject[] _propGameObjects;
   [SerializeField] private Transform[] _propPlacements;

   private void Start()
   {
      foreach (Transform propPlacement in _propPlacements)
      {
         int propIndex = Random.Range(0, _propGameObjects.Length);
         Instantiate(_propGameObjects[propIndex], propPlacement);
      }
   }
}
