using UnityEngine;

namespace ADSN
{
    public class SqrBtnBehavior : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Potato")
            {
                GameEvents.current.SqrBtnHitEvent();
                Destroy(other.gameObject);
            }
        }

        /*private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Potato")
            {
                GameEvents.current.SqrBtnHitEvent();
                Destroy(collision.gameObject);
            }
        }*/
    }
}