using UnityEngine;
using TMPro;

namespace ADSN
{
    public class ScoreUpdater : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI goldCountText;
        [SerializeField] TextMeshProUGUI scoreText;
        [SerializeField] TextMeshProUGUI timerText;
        
        public GameObject inGameUI;
        
        
        [Range(1f, 300f)] public float timeLeft = 10;
        
        bool timerON = false;
        int score = 0;

        void Start()
        {
            GameEvents.current.OnGoldSpawn += UpdateGoldCountTxt;
            GameEvents.current.OnGameOver += UpdateScoreTxt;

            timerON = true;
        }
        private void Update()
        {
            if (timerON)
            {
                if (timeLeft > 0)
                {
                    timeLeft -= Time.deltaTime;
                    UpdateTimerTxt(CalculateTimer(timeLeft).minute, CalculateTimer(timeLeft).second);
                }
                else
                {
                    timeLeft = 0;
                    timerON = false;
                    GameManager.gameIsOver = true;
                    Debug.Log("Time is UP!");
                }
            }

            if (GameManager.gameIsOver)
            {
                GameEvents.current.GameOverEvent(score);
            }
        }

        private (float minute, float second) CalculateTimer(float currentTime)
        {
            currentTime += 1;

            float minutes = Mathf.FloorToInt(currentTime / 60);
            float seconds = Mathf.FloorToInt(currentTime % 60);

            return (minutes, seconds);
        }

        private void UpdateTimerTxt(float minutes, float seconds)
        {
            timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
        }

        public void UpdateGoldCountTxt(int count)
        {
            goldCountText.text = "X " + count.ToString();
            UpdateScoreValue(count);
        }

        public void UpdateScoreValue(int goldCount)
        {
            int multiplier = Mathf.FloorToInt(CalculateTimer(timeLeft).minute) + 1;
            int scoreValue = goldCount * multiplier;
            score = scoreValue;
        }

        public void UpdateScoreTxt(int score)
        {
            if (score > 0) scoreText.text = "-- " + score.ToString() + " --";
            else scoreText.text = "-- 00 --";
        }

        private void OnDestroy()
        {
            GameEvents.current.OnGoldSpawn -= UpdateGoldCountTxt;
            GameEvents.current.OnGameOver -= UpdateScoreTxt;
        }
    }
}