using UnityEngine;

namespace PS5.Core

{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [field:SerializeField]
        public int CurrentScore { get; private set; }  
    
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
        }

        public void AddScore(int scoreToAdd)
        {
            CurrentScore += scoreToAdd;
            Debug.Log("Current Score: " + CurrentScore);
        }
    }
}
