using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretReplayObject : ReplayObject
{   
    [SerializeField]
    private GameObject fakeBulletPrefab;
    private List<Vector3> bulletPositions; 
    private List<Quaternion> bulletRotations;
    private bool shot;
    public override void SetDataForFrame(ReplayData data){
        TurretReplayData turretData = (TurretReplayData) data; 
    this.transform.position = turretData.position; 
    this.bulletPositions = turretData.bulletPositions;
    this.transform.rotation = turretData.rotation;
    this.bulletRotations = turretData.bulletRotations;
    this.shot = turretData.shot;
    int count = 0;
    if(shot){
        GetComponent<AudioSource>().volume = (PlayerPrefs.GetFloat("volume") / 100) / 4; 
        GetComponent<AudioSource>().Play();
    }
    foreach(Vector3 bullet in bulletPositions){
        GameObject temp = Instantiate(fakeBulletPrefab, bullet, Quaternion.identity);
        temp.transform.rotation = bulletRotations[count];
        count++;
    }
   }
}
