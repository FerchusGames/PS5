using System;
using System.Collections;
using System.Collections.Generic;
using PS5.Core;
using UnityEngine;
using TMPro;

namespace PS5.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;

        private void Start()
        {
            GameManager.Instance.onScoreChangeAction += UpdateScore;
        }

        private void UpdateScore(int score)
        {
            _scoreText.text = score.ToString();
        }
    }
}
