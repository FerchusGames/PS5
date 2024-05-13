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
    private int popIndex;
    private float waitTime = 3f;
    private int tutorialInt;
    
    private bool GetIfTutoIsNotPlayed()
    {
        tutorialInt = PlayerPrefs.GetInt("tutorial");
        
        if (tutorialInt == 0)
        {
            return true;
        }
        return false;
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
        
        if (GetIfTutoIsNotPlayed())
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
                    waitTime = 1.5f;
                    popIndex++;
                    GameManager.Instance.SetGameState(GameState.gaming);               
                }
            }
            else if (popIndex == 1)
            {
                if (waitTime <= 0)
                {
                    popIndex++;
                    waitTime = 1.5f;
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
                    waitTime = 1.5f;
                    GameManager.Instance.EndTuto();
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
                    popups.Last().SetActive(false);
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }

    private void ResetPopUps()
    {
        popIndex = 0;
    }
    
}
