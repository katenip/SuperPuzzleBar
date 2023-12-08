using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishLevel : MonoBehaviour
{
  [SerializeField]
  private GameObject player; 
  private void Start(){
    player = GameObject.Find("Player");
  }
  private void OnTriggerEnter2D(Collider2D collider){
    if(!collider.gameObject.CompareTag("Player")) return;
    EventManager.OnGoalReached();
    
    
  }
        
    }

