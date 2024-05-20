using UnityEngine;

public class DisableIfTuto : MonoBehaviour
{
    [SerializeField] private GameObject _tutorialButton;
    
    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("tutorial") == 0)
        {
            _tutorialButton.SetActive(false);
        }

        else
        {
            _tutorialButton.SetActive(true);
        }
    }
}
