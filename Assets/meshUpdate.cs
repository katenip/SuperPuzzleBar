using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshPlus.Components;
public class meshUpdate : MonoBehaviour
{
    public NavMeshSurface Surface2D;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
          Surface2D.UpdateNavMesh(Surface2D.navMeshData);
    }
}
