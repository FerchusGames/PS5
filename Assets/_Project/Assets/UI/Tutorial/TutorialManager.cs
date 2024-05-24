using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject[] popups;
    [SerializeField] private Slider[] controls;
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject mainUI;
    private int popIndex = 0;
    private float waitTime = 10f;
    private int tutorialInt;

    private void Start()
    {
        GameManager.Instance.StartTuto();
    }

    public void ResetIndex()
    {
        popIndex = 0;
    }
    
    private void Update()
    {
        if (GameManager.Instance.GameState == GameState.lose)
        {
            GameManager.Instance.OnLoseTuto();
        }
    }

    private void OriginalUpdate()
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
                waitTime = 10f;
                popIndex++;
                GameManager.Instance.SetGameState(GameState.gaming);               
            }
        }
        else if (popIndex == 1)
        {
            if (waitTime <= 0)
            {
                popIndex++;
                waitTime = 10f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        else if (popIndex == 2)
        {
            if (GameManager.Instance.CurrentScore > 0)
            {
                popIndex++;
                waitTime = 5f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        else if (popIndex == 3)
        {
            if (waitTime <= 0)
            {
                GameManager.Instance.EndTuto();
                popups.Last().SetActive(false);
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
