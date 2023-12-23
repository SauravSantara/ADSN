using UnityEngine;
using UnityEngine.SceneManagement;

namespace ADSN
{
    public class GameOverMenu : MonoBehaviour
    {
        public GameObject gameOverUI;
        public GameObject inGameUI;

        private void Update()
        {
            if (GameManager.gameIsOver)
                OnGameStop();
        }

        public void OnGameStop()
        {
            gameOverUI.SetActive(true);
            inGameUI.SetActive(false);
            Time.timeScale = 0f;
        }

        public void OnReplayLevel()
        {
            Time.timeScale = 1f;
            GameManager.gameIsOver = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}