using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReplayData : ReplayData
{
    
    public bool isMove{get; private set;}
    public bool stunned{get; private set;}
    public bool punch{get; private set;}
    public float hp{get; private set;}

   public PlayerReplayData(Vector3 position, Quaternion rotation, bool isMove, float hp, bool punch, bool stunned)
   {
    this.position = position;
    this.rotation = rotation;
    
    this.hp = hp;
    this.isMove = isMove;
    this.stunned = stunned;
    this.punch = punch;
   }
}
