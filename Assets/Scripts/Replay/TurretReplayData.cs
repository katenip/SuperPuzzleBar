using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretReplayData : ReplayData
{
    
    public List<Vector3> bulletPositions{get; private set;}
    public List<Quaternion> bulletRotations{get; private set;}
    public bool shot{get; private set;}
   public TurretReplayData(Vector3 position, Quaternion rotation, List<Vector3> bulletPositions, List<Quaternion> bulletRotations, bool shot)
   {
    this.position = position;
    this.bulletPositions = bulletPositions;
    this.rotation = rotation;
    this.bulletRotations = bulletRotations;
    this.shot = shot;
   }
}
