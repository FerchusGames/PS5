using System;
using UnityEngine;

namespace PS5.Core

{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public GameStates gameStates;

        [field:SerializeField]
        public int CurrentScore { get; private set; }

        public event Action<int> onScoreChangeAction;
        public event Action SaveEvent;
        public event Action OnLoseEvent; 
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            gameStates = GameStates.gaming;
        }

        public void AddScore(int scoreToAdd)
        {
            CurrentScore += scoreToAdd;
            Debug.Log("Current Score: " + CurrentScore);
            onScoreChangeAction?.Invoke(CurrentScore);
        }

        public void Save()
        {
            int finalScore = CurrentScore;
            Debug.Log("Final Score: " + finalScore);
            PlayerPrefs.SetInt("Score", finalScore);
            PlayerPrefs.Save();
            SaveEvent?.Invoke();
        }
        
        public void OnLose()
        {
            gameStates = GameStates.lose;
            OnLoseEvent?.Invoke();
        }
    }
}

public enum GameStates
{
    lose,
    pause,
    gaming
}
