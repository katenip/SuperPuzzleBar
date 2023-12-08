using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    public GameObject main;
    public GameObject levelSelecter;
    public GameObject Options;
    public GameObject Lore;
    public GameObject Instructions;
    public AudioSource clickSound;
    void Start(){
        clickSound = GetComponent<AudioSource>();
    }
    public void playSound(){
        clickSound.Play();
    }
    public void onlyMain(){
        main.SetActive(true);
        levelSelecter.SetActive(false);
        Options.SetActive(false);
        Lore.SetActive(false);
        Instructions.SetActive(false);
        clickSound.Play();
    }
    public void levelSel(){
        main.SetActive(false);
        levelSelecter.SetActive(true);
        Options.SetActive(false);
        Lore.SetActive(false);
        Instructions.SetActive(false);
        clickSound.Play();
       
    }
    public void onlyOptions(){
        main.SetActive(false);
        levelSelecter.SetActive(false);
        Options.SetActive(true);
        Lore.SetActive(false);
        Instructions.SetActive(false);
        clickSound.Play();
    }
    public void onlyStory(){
        main.SetActive(false);
        levelSelecter.SetActive(false);
        Options.SetActive(false);
        Lore.SetActive(true);
        Instructions.SetActive(false);
        clickSound.Play();
    }
     public void onlyInstruct(){
        main.SetActive(false);
        levelSelecter.SetActive(false);
        Options.SetActive(false);
        Lore.SetActive(false);
        Instructions.SetActive(true);
        clickSound.Play();
    }


}
