using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockedList : MonoBehaviour
{
    [SerializeField]
    private List<Vector2> blocked; 
    [SerializeField] private float height;
    [SerializeField] private float width;
    public void Awake(){
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("blocked");
        blocked = new List<Vector2>();
        
        foreach (GameObject block in blocks){
            Vector3 thisBlock = block.transform.position;
            
            
            
            thisBlock.x = Mathf.FloorToInt((thisBlock).x);
            thisBlock.y = Mathf.FloorToInt((thisBlock).y);


            block.transform.position = thisBlock;
           
            blocked.Add(new Vector2(thisBlock.x , thisBlock.y));
            
            blocked.Add(new Vector2(thisBlock.x+1 , thisBlock.y+1));
            blocked.Add(new Vector2(thisBlock.x+1 , thisBlock.y));
            blocked.Add(new Vector2(thisBlock.x , thisBlock.y+1));


            
        
        }
     
        
    }
    public void FixedUpdate(){
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("blocked");
        GameObject[] boxs =  GameObject.FindGameObjectsWithTag("box");
        blocked = new List<Vector2>();
        
        foreach (GameObject block in blocks){
            Vector3 thisBlock = block.transform.position;
            


            if(thisBlock.x < 100){
            thisBlock.x = Mathf.FloorToInt((thisBlock).x);
            thisBlock.y = Mathf.FloorToInt((thisBlock).y);


            block.transform.position = thisBlock;
           
            blocked.Add(new Vector2(thisBlock.x , thisBlock.y));
            
            blocked.Add(new Vector2(thisBlock.x+1 , thisBlock.y+1));
            blocked.Add(new Vector2(thisBlock.x+1 , thisBlock.y));
            blocked.Add(new Vector2(thisBlock.x , thisBlock.y+1));
            }
    }
    
        foreach (GameObject block in boxs){
            Vector3 thisBlock = block.transform.position;
            


            if(thisBlock.x < 100){
            thisBlock.x = Mathf.FloorToInt((thisBlock).x);
            thisBlock.y = Mathf.FloorToInt((thisBlock).y);


            block.transform.position = thisBlock;
           
            blocked.Add(new Vector2(thisBlock.x , thisBlock.y));
            
            blocked.Add(new Vector2(thisBlock.x+1 , thisBlock.y+1));
            blocked.Add(new Vector2(thisBlock.x+1 , thisBlock.y));
            blocked.Add(new Vector2(thisBlock.x , thisBlock.y+1));
            }
    }
    
    
    }

    public List<Vector2> getBlockedList(){
        return blocked;
    }
}
