using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class pickUp : MonoBehaviour
{
    public GameObject[] grabAble;
    public float liftRange = 0.5f;
    private float liftCD = 0.3f;
    public bool holding = false;
    public GameObject holdingThis;
    public float throwSpeed = 4f;
    private Quaternion holdingRotation;
    public playerAnimationController anim;
    public LayerMask punchlm;
     public TurnBased turnControl; 
    public float punchCD = 0f;
    // Start is called before the first frame update
    void Start()
    {
        grabAble = GameObject.FindGameObjectsWithTag("liftAble");
        grabAble = grabAble.Concat(GameObject.FindGameObjectsWithTag("gun")).ToArray();
        EventManager.onRestartLevel += OnRestartLevel;
        anim =  this.gameObject.GetComponent<playerAnimationController>();
        turnControl = GameObject.Find("TimeObject").GetComponent<TurnBased>();
    }

    // Update is called once per frame
    void Update()
    {
        if(liftCD <= 0){
          if (Input.GetMouseButtonDown(1))
        {
            if(holding){
                drop();
                liftCD = 0.3f;
            }else{
                GameObject temp = searchObject();
                if(temp != null){
                pickUpObj(temp);
                liftCD = 0.3f;
                }
            }
        }
        }else{
            liftCD = liftCD - Time.deltaTime; 
        }


        if(!holding){
            if (Input.GetMouseButtonDown(0) && punchCD <= 0f){
                anim.shouldPunch();
                this.gameObject.GetComponent<playerMoveScript>().shouldPunchThing();
                punchCD = 1f;
                liftCD = 0.6f;
                
                
                
                
        }
    }
    if (punchCD > 0f){
        punchCD = punchCD - Time.deltaTime;
        if(punchCD > .2 && punchCD <=.6f){
        RaycastHit2D punchCast =  Physics2D.Raycast(transform.position, transform.up, 1.2f, punchlm);
        if (punchCast.collider != null){
            punchCast.collider.gameObject.GetComponent<StunScript>().stunMe();
        }}
    }
    }
    void FixedUpdate(){
         
    }

    private GameObject searchObject(){
        foreach (GameObject obj in grabAble){
            
            if (((this.transform.position + (this.transform.forward * 0.2f)) - obj.transform.position).magnitude <= liftRange){
                print("holding"); 
                
                return obj;
            }
        }
        return null;

    }
    private void pickUpObj(GameObject obj){
        
        holdingThis = obj;
        holdingThis.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        obj.transform.SetParent(this.transform, true);
        obj.transform.localPosition = new Vector3(0.4f , 0, Vector3.up.z) ;
        holdingThis.GetComponent<Rigidbody2D>().simulated = false; 
        
        obj.transform.rotation = this.transform.rotation;
        holding = true;
    }
    
    public void drop(){
        if(holdingThis != null){
        holdingThis.transform.SetParent(null, true);
        holding = false;
        
        holdingThis.GetComponent<Rigidbody2D>().simulated = true; 
        holdingThis.GetComponent<Rigidbody2D>().velocity = this.transform.up * throwSpeed;
        
        }
    }
    private void OnRestartLevel(){
        
        
        holding = false;
        holdingThis = null;
       
    }
    private void OnDisable(){
         //EventManager.onGoalReached -= OnGoalReached;
         EventManager.onRestartLevel -= OnRestartLevel;
    }
    void LateUpdate(){
        if (punchCD > 0){
            turnControl.ResumeGame();
        }
    }
}
