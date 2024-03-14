using UnityEngine;
using TMPro;

namespace Class
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _uiScore;
        private float score = 0;
        public float Score => score;

        public static GameManager Instance { get; private set; }

        void Start()
        {
            _uiScore.text = Score.ToString("Score: " + Score);
        }

        public void IncreaseScore(float plus)
        {
            score += plus;
            _uiScore.text = Score.ToString("Score: " + Score);
        }

        private void Awake()
        {
            Instance = this;
        }
    }
}
