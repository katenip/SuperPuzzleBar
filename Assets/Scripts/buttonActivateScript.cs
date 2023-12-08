using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonActivateScript : MonoBehaviour
{
    public GameObject thingToActivate;
    public GameObject key;
    public GameObject thisButton;
    public float maxDistance = 0.45f;
    private Vector2 startPos;
    public GameObject endDoor;
    public GameObject player;
    private Recorder recorder;
    public AudioSource aud;
    void Start(){
        startPos = thingToActivate.transform.position;
        player = GameObject.Find("Player");
    }

    
    
    void Awake()
    {
        recorder = GetComponent<Recorder>();
    }


    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject == key){
            
            key.transform.position = new Vector2(1000, 1000);
            thingToActivate.transform.position = endDoor.transform.position;
             thingToActivate.transform.rotation = endDoor.transform.rotation;
             aud.Play();
        }
        if (collider.gameObject == player && player.GetComponent<pickUp>().holdingThis == key){
            player.GetComponent<pickUp>().drop();
            player.GetComponent<pickUp>().holdingThis = null;
            key.transform.position = new Vector2(1000, 1000);
            thingToActivate.transform.position = endDoor.transform.position;
            thingToActivate.transform.rotation = endDoor.transform.rotation;
            aud.Play();
        }
    }

    void FixedUpdate()
    {
        ReplayData data = new NormalReplayData(this.transform.position, this.transform.rotation);
        recorder.RecordReplayFrame(data);
    }
   
}
