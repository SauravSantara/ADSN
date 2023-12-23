using System;
using UnityEngine;

namespace ADSN
{
    public class GameEvents : MonoBehaviour
    {
        public static GameEvents current;

        private void Awake()
        {
            current = this;
        }

        public event Action OnSqrBtnHit;
        public void SqrBtnHitEvent()
        {
            if (OnSqrBtnHit != null)
            {
                OnSqrBtnHit();
            }
        }

        public event Action<int> OnGoldSpawn;
        public void GoldSpawnEvent(int count)
        {
            if (OnGoldSpawn != null)
            {
                OnGoldSpawn(count);
            }
        }

        public event Action<int> OnGameOver;
        public void GameOverEvent(int score)
        {
            if (OnGameOver != null)
            {
                OnGameOver(score);
            }
        }
    }
}
