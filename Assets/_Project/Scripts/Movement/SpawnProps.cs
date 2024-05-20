using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnProps : MonoBehaviour
{
   [SerializeField] private GameObject[] _propGameObjects;
   [SerializeField] private GameObject[] _characterGameObjects;
   [SerializeField] private Transform[] _propPlacements;
   [SerializeField] private Transform _characterPlacement;
   [SerializeField] private Transform _parent;
   [SerializeField] private Transform _house;

   [SerializeField] private float _minScaleMultiplier = 1f;
   [SerializeField] private float _maxScaleMultiplier = 1f;
   
   private void Start()
   {
      foreach (Transform propPlacement in _propPlacements)
      {
         float scaleMultiplier = Random.Range(_minScaleMultiplier, _maxScaleMultiplier);
         int propIndex = Random.Range(0, _propGameObjects.Length);
         GameObject prop = Instantiate(_propGameObjects[propIndex], propPlacement);
         prop.transform.localScale = new Vector3((1 / _parent.localScale.x) * scaleMultiplier, 
            (1 / _parent.localScale.y) * scaleMultiplier , 
            (1 / _parent.localScale.z) * scaleMultiplier); 
      }
      
      int characterIndex = Random.Range(0, _characterGameObjects.Length);
      GameObject character = Instantiate(_characterGameObjects[characterIndex], _characterPlacement);
      character.transform.localScale =
         new Vector3((1 / _parent.localScale.x) / _house.localScale.x , (1 / _parent.localScale.y) / _house.localScale.y, (1 / _parent.localScale.z) / _house.localScale.z);
   }
}
