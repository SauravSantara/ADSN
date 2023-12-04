using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ADSN
{
    public class ScoreUpdater : MonoBehaviour
    {

        [SerializeField]
        private TextMeshProUGUI scoreText;

        public void UpdateScore(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}