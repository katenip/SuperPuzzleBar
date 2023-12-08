using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    [Header("Prefab to instantiate")]
    [SerializeField] private GameObject replayObjectPrefab;

    public Queue<ReplayData> recordingQueue {get; private set;}
    private Recording recording;
    private bool isDoingReplay = false;

    // Update is called once per frame
    private void Awake()
    {
        recordingQueue = new Queue<ReplayData>();
    }
    private void Start(){
        // add code to have an event manager trigger this when we finish or restart
        EventManager.onGoalReached += OnGoalReached;
        EventManager.onRestartLevel += OnRestartLevel;
    }
    private void OnDisable(){
        //add code to handle the unsubcribing from event manager
        EventManager.onGoalReached -= OnGoalReached;
        EventManager.onRestartLevel -= OnRestartLevel;
    }
    private void OnGoalReached(){
        this.gameObject.GetComponent<Renderer>().enabled = false;
        foreach (Transform child in this.gameObject.transform)
            {
                if(child.gameObject.GetComponent<Renderer>() != null){
                    child.gameObject.GetComponent<Renderer>().enabled = false;
                }
            }

        StartReplay();
    }
    private void OnRestartLevel(){
        Reset();
        this.gameObject.GetComponent<Renderer>().enabled = true;
        foreach (Transform child in this.gameObject.transform)
            {
                if(child.gameObject.GetComponent<Renderer>() != null){
                    child.gameObject.GetComponent<Renderer>().enabled = true;
                }
            }
    }
    private void Update(){
 if (Input.GetKeyDown(KeyCode.Insert))
        {
            StartReplay();
        }
    }

    private void FixedUpdate(){
       


        if(!isDoingReplay){
            return;
        }
    bool hasMoreFrames = recording.PlayNextFrame();

    if(!hasMoreFrames){
        RestartReplay();
    }
    }




    public void RecordReplayFrame(ReplayData data){
        recordingQueue.Enqueue(data);
       
    }
    private void StartReplay(){
        isDoingReplay = true;
        recording = new Recording(recordingQueue);

        recordingQueue.Clear();
        recording.InstantiateReplayObject(replayObjectPrefab);
        }
    private void RestartReplay(){
        isDoingReplay = true;
        recording.RestartFromBeginning();
    }
    private void Reset(){
        isDoingReplay = false;
        recordingQueue.Clear();
        if(recording != null){
        recording.DestroyReplayObjectIfExists();
        recording = null;
    }}
}
