using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPopUp : MonoBehaviour
{
    [SerializeField] private GameObject _nextPopUp;

    public void GoToNextPopUp()
    {
        _nextPopUp.SetActive(true);
        gameObject.SetActive(false);
    }
}
