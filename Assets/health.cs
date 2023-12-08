using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    public float hp = 100f;
    // Start is called before the first frame update
    void Start()
    {

        EventManager.onRestartLevel += OnRestartLevel;
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0){
            hp = 0f;
        }
    }
    void OnDisable(){
         EventManager.onRestartLevel -= OnRestartLevel;
    }
    void OnRestartLevel(){
        hp = 100;
    }
}
