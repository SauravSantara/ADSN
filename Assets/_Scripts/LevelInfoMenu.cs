using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ADSN
{
    public class LevelInfoMenu : MonoBehaviour
    {

        [SerializeField] TextMeshProUGUI levelIdTxt;
        [SerializeField] TextMeshProUGUI ObjectiveTxt;

        void Start()
        {
            levelIdTxt.text = "Level " + LevelManager.sceneIndex.ToString();
            ObjectiveTxt.text = "Earn " + LevelManager.objectiveDesc;
        }

        public void LoadScene()
        {
            SceneManager.LoadScene(LevelManager.sceneIndex);
        }
    }
}