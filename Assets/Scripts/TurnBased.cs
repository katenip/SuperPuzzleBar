using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnBased : MonoBehaviour
{
    public bool timePassing = true; 
    
    
    public float timeSpeed = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.onGoalReached += OnGoalReached;
        EventManager.onRestartLevel += OnRestartLevel;
        PauseGame();
        timePassing = true;
    }
    private void OnDisable(){
        EventManager.onGoalReached -= OnGoalReached;
        EventManager.onRestartLevel -= OnRestartLevel;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnGoalReached(){
        timePassing = false;
        Time.timeScale = 2f;
    }
    private void OnRestartLevel(){
        timePassing = true;
        
    }
    public void PauseGame()
    {
        if(timePassing){
        Time.timeScale = timeSpeed;
        }
    }
    
    public void ResumeGame()
    {
        if(timePassing){
        Time.timeScale = 2f;
        }
        
    }   

}
