using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//using LootLocker.Requests;
using Unity.VisualScripting;

namespace ADSN
{
    public class GameManager : MonoBehaviour
    {
        /*[SerializeField]
        private UnityEvent playerConnected;

        /// Using IEnumerator instead of void to connect Event with Loot Locker session
        private IEnumerator Start() {
            bool connected = false;

            LootLockerSDKManager.StartGuestSession((response) =>
            {
                if(!response.success) {
                    Debug.Log("Error starting LootLocker session!");
                    return; 
                }
                Debug.Log("Successfully started LootLocker session!");
                connected = true;
            });
            yield return new WaitUntil(() => connected);
            playerConnected.Invoke();
        }*/
    }

}