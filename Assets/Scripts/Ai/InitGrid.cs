using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGrid : MonoBehaviour
{
   
    void Start()
    {
        List<Vector2> blocked = new List<Vector2>();
        blocked.Add(new Vector2(5,2));
        blocked.Add(new Vector2(5,3));
        Pathfinding pathfinding = new Pathfinding(16, 8,blocked);
    }

 
}
