using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabbable : MonoBehaviour
{
    public Transform hand;
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
        print("Collided");
        transform.parent = collision.gameObject.transform.parent;
    }

    private void OnTriggerEnter(Collider other)
    {
        transform.parent = hand;
        
    }
}
