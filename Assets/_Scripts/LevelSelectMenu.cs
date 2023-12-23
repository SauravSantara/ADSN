using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ADSN
{
    public class LevelSelectMenu : MonoBehaviour
    {
        [SerializeField] int sceneId;
        [SerializeField] string objectiveAmount;
        [SerializeField] TextMeshProUGUI sceneIdTxt;

        private void Start()
        {
            sceneIdTxt.text = sceneId.ToString();
        }

        public void UpdateLevelInfo()
        {
            LevelManager.sceneIndex = sceneId;
            LevelManager.objectiveDesc = objectiveAmount;
        }

        
    }
}
