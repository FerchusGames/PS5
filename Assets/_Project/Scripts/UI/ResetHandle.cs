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

    private void OnEnable()
    {
        GameManager.Instance.OnGameStateChange += CheckGameStateChange;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ResetValue();
    }

    private void CheckGameStateChange(GameState gameState)
    {
        if (gameState != GameState.gaming)
            ResetValue();
    }

    private void ResetValue()
    {
        _slider.value = 0;
    }
}
