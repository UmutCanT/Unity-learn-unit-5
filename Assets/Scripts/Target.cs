using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    Rigidbody targetRb;

    float ySpawnPos = -2;
    float xSpawnRange = 4;
    float minForce = 12;
    float maxForce = 16;
    float maxTorque = 10;


    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        transform.position = RandomSpawnPos();

        ObjectsBehave(targetRb);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    void OnMouseDown()
    {
        Destroy(gameObject);    
    }

    void ObjectsBehave(Rigidbody rigidbody)
    {
        rigidbody.AddForce(RandomForce(), ForceMode.Impulse);
        rigidbody.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minForce, maxForce);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(xSpawnRange, -xSpawnRange), ySpawnPos);
    }
}
