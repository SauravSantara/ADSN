using UnityEngine;

namespace ADSN
{
    public class SpawnGold : MonoBehaviour
    {
        [SerializeField] Transform startPosition;
        [SerializeField] Rigidbody objectToThrow;
        [SerializeField] float force = 5f;
        public int countOfGold = 0;

        void Start()
        {
            if (startPosition == null)
                startPosition = GetComponent<Transform>();

            GameEvents.current.OnSqrBtnHit += ThrowGold;
        }

        private void ThrowGold()
        {
            force = UnityEngine.Random.Range(5f, 25f);
            Rigidbody thrownObject = Instantiate(objectToThrow, startPosition.position, Quaternion.identity);
            thrownObject.AddForce(Vector3.back * force, ForceMode.Impulse);

            countOfGold += 1;
            GameEvents.current.GoldSpawnEvent(countOfGold);
        }

        /*public int CountOfGold()
        {
            countOfGold += 1;
            return countOfGold;
        }*/

        private void OnDestroy()
        {
            GameEvents.current.OnSqrBtnHit -= ThrowGold;
        }
    }
}