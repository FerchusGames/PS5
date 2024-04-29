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
    private float waitTime = 2.5f;

    private void Update()
    {
        if (PlayerPrefs.GetInt("tutorial") == 0)
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
                if (waitTime <= 0)
                {
                    GameManager.Instance.StartGame();
                    popIndex++;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
            else if (popIndex == 2)
            {
                spawner.SetActive(true);
                if (GameManager.Instance.CurrentScore > 0)
                {
                    waitTime = 2.5f;
                    popIndex++;
                }
            }
            else if (popIndex == 3)
            {
                if (waitTime <= 0)
                {
                    popups.Last().SetActive(false);
                    GameManager.Instance.EndTuto();
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }
}
