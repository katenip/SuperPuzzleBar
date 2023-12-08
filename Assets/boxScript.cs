using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxScript : MonoBehaviour
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
        
       for(int x = 0; x < transform.childCount -1; x++){
                transform.GetChild(x).tag = "blocked";
            }

    }
   

    // Update is called once per frame
    void FixedUpdate(){
        ReplayData data = new NormalReplayData(this.transform.position, this.transform.rotation);
        recorder.RecordReplayFrame(data);
    }
    void OnDisable(){
        EventManager.onRestartLevel -= OnRestartLevel;
    }
}
