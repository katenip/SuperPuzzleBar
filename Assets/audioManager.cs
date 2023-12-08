using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public AudioSource stunSound;
    public AudioSource punchSound;
    public AudioSource walkSound;
    public bool walking = false;
   
   void Start(){
    AudioListener.volume = PlayerPrefs.GetFloat("volume") / 100;
    stunSound.volume = (PlayerPrefs.GetFloat("volume") / 100) / 4; 
    walkSound.volume = (PlayerPrefs.GetFloat("volume") / 100) / 2; 
    EventManager.onPlayerDeath += OnPlayerDeath;
    EventManager.onRestartLevel += OnRestartLevel;
   }
   void OnPlayerDeath(){
    walkSound.volume = 0f; 
    punchSound.volume = 0f;
   }
   void OnRestartLevel(){
    if(walkSound != null){
    walkSound.volume = (PlayerPrefs.GetFloat("volume") / 100) / 2; 
    punchSound.volume = 1f;
    }
   }
   void OnDisable(){
    
    EventManager.onPlayerDeath -= OnPlayerDeath;
   }
    
    public void Walk(){
        if (!walking){
            walkSound.Play();
            walking = true;
                }}
    public void Punch(){
            
            punchSound.Play();
        }
    public void Stun(){
            walkSound.Stop();
            stunSound.Play();
        }
    public void StopWalk(){
        walkSound.Stop();
        walking = false;
    }
    void Update(){
        
        float time = Time.timeScale;
        if(time == 0.3f){
            time = 0.8f;
        }
        stunSound.pitch = time;
        punchSound.pitch = time;
        walkSound.pitch = time;
    }
    
   


    }

