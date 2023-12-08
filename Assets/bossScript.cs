using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshPlus.Components;

public class bossScript : MonoBehaviour
{
    public bool isAlive;
    public bool stuned;
    public float stunCD;
    public GameObject player;
    private Rigidbody2D rb; 
    public float moveSpeed;
    private bool holding;
    public GameObject holdingThis;
    private int state;
    private Recorder recorder;
    public GameObject[] guns;
    private Transform npcTrans;
    private Pathfinding pathfinding;
    private List<Node> path;
    private List<Vector2> blocked; 
    [SerializeField] private float satisfationRange; 
    [SerializeField] private float speed; 
    [SerializeField] private int height;
    [SerializeField] private int width;
    private Vector2 prevPosition;
    [SerializeField] private LayerMask mask; 
    [SerializeField] private float dropThreshhold; 
    [SerializeField] private float throwSpeed; 
    public float punchCD = 0f;
    public LayerMask punchlm;
    private bool firstFrame = true;
    public NavMeshAgent agent;
    public GameObject navmesh;
    public NavMeshSurface Surface2D;
    public playerAnimationController anim;
    public float deSpawnTimer = 3f;
    private bool isMove = false;
    public audioManager audi;
    private bool timeToPunch = false;
    public GameObject bossGun;
     
    private Vector3 originPos;
    private Quaternion startRot;
    void Awake()
    {
        audi = GetComponent<audioManager>();
        Surface2D = GameObject.Find("NavMesh").GetComponent<NavMeshSurface>();
        agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;

        originPos = transform.position;
        startRot = transform.rotation;
        npcTrans = this.gameObject.transform;
        recorder = this.gameObject.GetComponent<Recorder>();
        isAlive = true;
        player = GameObject.Find("Player");
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        if(holdingThis != null){
            holding = true;
            state = 0;
        }else{
            holding = false;
            state = 2;
        }
        guns = GameObject.FindGameObjectsWithTag("bossGun");
     
        EventManager.onGoalReached += OnGoalReached;
        EventManager.onRestartLevel += OnRestartLevel;
         prevPosition = transform.position;
         anim =  this.gameObject.GetComponent<playerAnimationController>();
    }

    void Update()
    {
        if (stunCD > 0f){
            stunCD = stunCD - Time.deltaTime;
        }else{
            stuned = false;
        }
        if (punchCD > 0f){
        punchCD = punchCD - Time.deltaTime;
        }
        
    }
 
    void FixedUpdate()
    {
        if(isAlive){
        if(!stuned){
        if(guns.Length != 0){
        if(holding){
        RaycastHit2D hit = Physics2D.Raycast(holdingThis.transform.position, (player.transform.position - holdingThis.transform.position).normalized, Mathf.Infinity, mask);
        
        if(hit.transform.gameObject != player){
        handleMove(player);
        isMove = true;

       }else{
            rb.velocity = Vector3.zero;
            holdingThis.GetComponent<BossBun>().shouldShoot = true;
        }
        }else{
            
            GameObject tempGun = bossGun;
           
            if(tempGun != null){
            
                handleMove(tempGun);
                isMove = true;
            }
            
            
        }
        }else{
            handleMove(player);
            isMove = true;
        }
        
        Quaternion rotation = Quaternion.LookRotation
            (player.transform.position - transform.position, transform.TransformDirection(-Vector3.forward));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        }else{anim.Stun(); 
            isMove = false;}
        }else{
            anim.Stun(); 
            isMove = false;
            this.GetComponent<NavMeshAgent>().enabled = false;
            deSpawnTimer = deSpawnTimer - Time.deltaTime;
            if(deSpawnTimer <= 0f){
                this.transform.position = new Vector3 (1000, 1000, 0f);

            }
        } 
        ReplayData data = new PlayerReplayData(this.transform.position, this.transform.rotation, isMove, this.gameObject.GetComponent<health>().hp, punchCD > 0, stuned);
        recorder.RecordReplayFrame(data);
       if(isMove){
            audi.Walk();
        }else{
            audi.StopWalk();
        }
        if(timeToPunch){
            timeToPunch = false;
            audi.Punch();
        }
       
    }
 
    private void doMove(Vector3 thisPoint){
       
       
        Vector3 tempTemp = player.transform.position - this.transform.position;
       
        if(tempTemp.magnitude > 4){

        
       
        agent.SetDestination(thisPoint);
        anim.startMove();
        }else{
          rb.velocity = Vector3.zero;
          anim.stopMove();  
          

          
        if(punchCD <= 0f){
        punchCD = 1f;
        timeToPunch = true;
        anim.shouldPunch(); 
        }if(punchCD > 0f && punchCD <= .80f){RaycastHit2D punchCast =  Physics2D.Raycast(transform.position, transform.up, 4f, punchlm);
        if (punchCast.collider != null){
            punchCast.collider.gameObject.GetComponent<StunScript>().stunMeHard();
        }}
    }
        }
    

    private void handleMove(GameObject targetObject){
         
        doMove(targetObject.transform.position);
        if((targetObject.transform.position - transform.position).magnitude <= 4f) {
            if(targetObject.tag == "bossGun"){
               pickUpObj(targetObject); 
            }
            
        }
    }
     private void pickUpObj(GameObject obj){
        
        holdingThis = obj;
        holdingThis.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        obj.transform.SetParent(this.transform, true);
        obj.transform.localPosition = new Vector3(.3f, .08f, .03f);
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
        holdingThis.GetComponent<BossBun>().shouldShoot = false;
        }
    }
     private void OnRestartLevel(){
        holding = false;
        rb.velocity = Vector3.zero;
        this.transform.position = originPos;
        this.transform.rotation = startRot;
        this.GetComponent<NavMeshAgent>().enabled = true;
        isAlive = true;
     }

  
         private void OnDisable(){
         EventManager.onGoalReached -= OnGoalReached;
         EventManager.onRestartLevel -= OnRestartLevel;
    }
    void OnGoalReached(){
        isAlive = false;
    }
}

    
