using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playLevel : MonoBehaviour
{
    public int nextLevel;
    public bool readyForNextLevel = false;
    public GameObject clickSound;

    // Update is called once per frame
    public void loadFirstLevel()
    {
        clickSound.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        
    }
    public void loadSecondLevel()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
    public void loadThirdLevel()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
    public void loadForthLevel()
    {
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }
    public void loadNextLevel()
    {   
        
        SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
    }
     public void loadMainMenu()
    {   
        
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    void Awake(){
    nextLevel = SceneManager.GetActiveScene().buildIndex +1;
    EventManager.onGoalReached += OnGoalReached;
    EventManager.onRestartLevel += OnRestartLevel;
    }

    void OnDisable(){
    EventManager.onGoalReached -= OnGoalReached;
    EventManager.onRestartLevel -= OnRestartLevel;
    }
    void OnGoalReached(){
        readyForNextLevel = true;
    }
     void OnRestartLevel(){
        readyForNextLevel = false;
    }
    void Update(){
        if(readyForNextLevel && Input.GetKeyDown(KeyCode.Space)){
            loadNextLevel();
        }
    }
}
