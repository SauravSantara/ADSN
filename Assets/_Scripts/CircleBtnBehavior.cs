using UnityEngine;

namespace ADSN
{
    public class CircleBtnBehavior : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            GameManager.gameIsOver = true;
        }
    }
}
