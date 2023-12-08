using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class healthScript : MonoBehaviour
{
    public float hp;
    public health ourParentHP;
    public Transform parent;
    public bool firstFrame = true;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.parent;
        ourParentHP = transform.parent.parent.gameObject.GetComponent<health>();
        transform.parent.SetParent(null);
        EventManager.onGoalReached += OnGoalReached;
        EventManager.onRestartLevel += OnRestartLevel;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (parent != null){
            transform.parent.position = parent.position;
        }
        hp = ourParentHP.hp;
        hp = hp / 100;
        hp = hp * 0.96f;
        transform.localScale = new Vector3(hp, transform.localScale.y, transform.localScale.z);
        
        if(hp <= 0f && parent.gameObject.GetComponent<PlayerReplayObject>() == null){
            if(parent.gameObject.tag == "bossnpc" && firstFrame){
                firstFrame = false;
            GameObject.Find("bossDrop").transform.position = new Vector3(parent.position.x, parent.position.y, 0f);
            GameObject.Find("bossDrop").transform.SetParent(null);
            }
            parent.gameObject.GetComponent<die>().doDie();
        }
    }
    void OnGoalReached(){
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        transform.parent.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        parent.position = new Vector3(10000, 10000, 0);
    }
    void OnRestartLevel(){
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        transform.parent.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        if(parent.gameObject.GetComponent<PlayerReplayObject>() != null){
            GameObject.Destroy(transform.parent.gameObject);
            GameObject.Destroy(this.gameObject);
        }
        firstFrame = true;
    }
    void OnDisable(){
        EventManager.onGoalReached -= OnGoalReached;
        EventManager.onRestartLevel -= OnRestartLevel;
        
    }
}
