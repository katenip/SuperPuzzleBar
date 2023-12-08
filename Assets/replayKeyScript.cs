using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class replayKeyScript : MonoBehaviour
{
    private Vector3 origin;
    private bool atOrigin = false;
    public AudioSource audi;
    void Awake(){
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == origin){
            atOrigin = true;
        }else{
            if (atOrigin){
            audi.Play();
            atOrigin = false;}
        }
    }
}
