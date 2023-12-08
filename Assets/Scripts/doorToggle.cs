using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorToggle : MonoBehaviour
{
    private Recorder recorder;
    private Vector3 origin;
    private Quaternion rot;
    void Awake()
    {
        EventManager.onRestartLevel += OnRestartLevel;
        origin = transform.position;
        rot = transform.rotation;
        recorder = GetComponent<Recorder>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ReplayData data = new NormalReplayData(this.transform.position, this.transform.rotation);
        recorder.RecordReplayFrame(data);
    }
    
    void OnRestartLevel(){
        transform.position = origin;
        transform.rotation = rot;
    }
    void OnDisable(){
        EventManager.onRestartLevel -= OnRestartLevel;
    }
}
