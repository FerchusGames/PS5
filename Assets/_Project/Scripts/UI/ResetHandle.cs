using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResetHandle : MonoBehaviour, IPointerUpHandler
{
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _slider.value = 0;
    }
}
