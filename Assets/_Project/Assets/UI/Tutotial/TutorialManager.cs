using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject[] popups;
    [SerializeField] private Slider[] controls;
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject mainUI;
    private int popIndex;
    private float waitTime = 2f;
    

    private void Update()
    {
        for (int i = 0; i < popups.Length; i++)
        {
            if (i == popIndex)
            {
                popups[i].SetActive(true);
            }
            else
            {
                popups[i].SetActive(false);
            }
        }
        
        if (popIndex == 0)
        {
            if (controls[0].value > 0 || controls[0].value < 0 || controls[1].value > 0 || controls[1].value < 0)
            {
                popIndex++;
            }
        }
        else if (popIndex == 1)
        {
            spawner.SetActive(true);
            if (GameManager.Instance.CurrentScore > 0)
            {
                popIndex++;
            }
        }
        else if (popIndex == 2)
        {
            if (waitTime <= 0)
            {
                mainUI.SetActive(true);
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
