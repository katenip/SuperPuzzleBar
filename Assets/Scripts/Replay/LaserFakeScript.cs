using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFakeScript : MonoBehaviour
{
    

    
    void Start()
    {
        Object.Destroy(this.gameObject, 0.01f);
    }
}
