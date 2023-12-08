using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class die : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void doDie(){
        if(transform.gameObject.tag == "npc"){
            transform.gameObject.GetComponent<aiScript>().isAlive = false;
        }
        if(transform.gameObject.tag == "Player"){
            EventManager.OnPlayerDeath();
        }
        if(transform.gameObject.tag == "bossnpc"){
            transform.gameObject.GetComponent<bossScript>().isAlive = false;
           
        }
    }
}
