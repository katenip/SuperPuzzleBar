using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyScript : MonoBehaviour
{
    private Vector3 origin;
    private Recorder recorder;
    private Quaternion originRot;
   
    private void Awake()
    {
        recorder = GetComponent<Recorder>();
    } 
      private void Start(){
        EventManager.onRestartLevel += OnRestartLevel;
        origin = this.transform.position;
        originRot = transform.rotation;
    }
    private void OnRestartLevel(){
        this.transform.position = origin;
        this.transform.rotation = originRot;
        transform.SetParent(null, true);
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

    }
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag != "npc" && collision.gameObject.tag != "gun"){
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        this.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0f;
    }}

    // Update is called once per frame
    void FixedUpdate(){
        ReplayData data = new NormalReplayData(this.transform.position, this.transform.rotation);
        recorder.RecordReplayFrame(data);
    }
    void OnDisable(){
        EventManager.onRestartLevel -= OnRestartLevel;
    }
}
