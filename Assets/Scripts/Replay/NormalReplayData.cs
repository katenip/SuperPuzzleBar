using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalReplayData : ReplayData
{
    public NormalReplayData(Vector3 position, Quaternion rotation){
        this.position = position;
        this.rotation = rotation;
    }
}
