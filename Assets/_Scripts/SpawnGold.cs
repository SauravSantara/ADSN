using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGold : MonoBehaviour
{
    [SerializeField]
    Rigidbody objectToThrow;

    [SerializeField, Range(0.0f, 50.0f)]
    float force = 40.0f;

    [SerializeField]
    Transform StartPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Potato")
        {
            Destroy(collision.gameObject);
            ThrowGold();
        }
    }

    private void ThrowGold()
    {
        //force = UnityEngine.Random.Range(3.0f, 30.0f);
        Rigidbody thrownObject = Instantiate(objectToThrow, StartPosition.position, Quaternion.identity);
        thrownObject.AddForce(Vector3.back * force, ForceMode.Impulse);
    }
}
