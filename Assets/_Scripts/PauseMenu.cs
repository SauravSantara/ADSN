using UnityEngine;
using UnityEngine.SceneManagement;

namespace ADSN
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject pauseMenuUI;
        SelectTarget st;
       
        /*void Start ()
        {
            st = new SelectTarget();
        }*/

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.gameIsOver)
            {
                if (GameManager.gameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameManager.gameIsPaused = false;
            SelectTarget.InputToggle(true);
        }

        public void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameManager.gameIsPaused = true;
            SelectTarget.InputToggle(false);
        }

        public void LoadMenu()
        {
            Time.timeScale = 1f;
            Debug.Log("Return to Menu ...");
            SceneManager.LoadScene(0);
        }

        public void QuitGame()
        {
            Debug.Log("Quit game ...");
            Application.Quit();
        }
    }
}