using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalReplayObject : ReplayObject
{

   public override void SetDataForFrame(ReplayData data){
    NormalReplayData normalData = (NormalReplayData) data; 
    this.transform.position = normalData.position;
    this.transform.rotation = normalData.rotation;
   }
}
