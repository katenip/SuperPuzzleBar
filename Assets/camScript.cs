using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camScript : MonoBehaviour
{
    private Vector3 origin;
    private GameObject player;
    private bool alive; 
    // Start is called before the first frame update
    public void Awake(){
        origin = this.transform.position;
        player = GameObject.Find("Player");
        EventManager.onGoalReached += OnGoalReached;
        EventManager.onRestartLevel += OnRestartLevel;
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(alive){
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
        this.gameObject.GetComponent<Camera>().orthographicSize = 7;
    }else{
        this.transform.position = origin;
        this.gameObject.GetComponent<Camera>().orthographicSize = 10;
    }
    }
    private void OnGoalReached(){
        alive = false;
    }
    private void OnRestartLevel(){
        alive = true;
    }
    private void OnDisable(){
        EventManager.onGoalReached -= OnGoalReached;
        EventManager.onRestartLevel -= OnRestartLevel;
    }
    
}
