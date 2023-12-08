using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cancelPlayerCollision : MonoBehaviour
{
    public Rigidbody rb; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        rb.velocity =  Vector3.zero; 
    }
}
