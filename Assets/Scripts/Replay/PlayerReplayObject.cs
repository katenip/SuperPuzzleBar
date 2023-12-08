using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReplayObject : ReplayObject
{   
    [SerializeField]
    private bool isMove;
    private bool stunned;
    private bool punch;
    private bool walking = false;
    private bool punching  = false;
    private bool stunneddd  = false;
    public audioManager audi;
    private playerAnimationController anim ;
    public override void SetDataForFrame(ReplayData data){
    PlayerReplayData playerData = (PlayerReplayData) data; 
    this.transform.position = playerData.position;
    this.anim = this.gameObject.GetComponent<playerAnimationController>();
    this.audi = GetComponent<audioManager>();
    if(playerData.isMove){
        if(!walking){
            walking = true;
            audi.Walk();
    anim.startMove();
    }}
    if(!playerData.isMove){
        walking = false;
        audi.StopWalk();
    anim.stopMove();
    }
    if(playerData.punch){
        if(!punching){
            punching = true;
        audi.Punch();
        }
    anim.shouldPunch();
    }else{punching = false;}
    if(playerData.stunned){
        if(!stunneddd){
        audi.Stun();
        stunneddd = true;
        }
    anim.Stun();
    }else{stunneddd = false;}
    this.transform.rotation = playerData.rotation;
    this.gameObject.GetComponent<health>().hp = playerData.hp;
    
    
    
   }
  
}
