using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeReplay : MonoBehaviour
{
    public Vector3 origin;
    private bool played = false;
    void Start()
    {
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position != origin){
        if (!played){
            played = true;
            GetComponent<AudioSource>().volume = (PlayerPrefs.GetFloat("volume") / 100) / 4; 
            GetComponent<AudioSource>().pitch = Time.timeScale;
            GetComponent<AudioSource>().Play();
        }
    }else{
        played = false;
    }
    }
}
