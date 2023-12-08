using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshPlus.Components;

public class aiScript : MonoBehaviour
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
    private GameObject[] guns;
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
     
    private Vector3 originPos;
    private Quaternion startRot;
    // Start is called before the first frame update
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
        guns = GameObject.FindGameObjectsWithTag("gun");
        //blocked = GameObject.Find("AiGrid").GetComponent<blockedList>().getBlockedList();
        
        //pathfinding = new Pathfinding(height, width,blocked);
        //updatePath(player);
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (player.transform.position - transform.position).normalized, Mathf.Infinity, mask);
        
        if(hit.transform.gameObject != player){
        handleMove(player);
        isMove = true;

       }else{
            rb.velocity = Vector3.zero;
            holdingThis.GetComponent<gunScript>().shouldShoot = true;
        }
        }else{
            
            GameObject tempGun = getNearestGun();
           
            if(tempGun != null){
            
                handleMove(tempGun);
                isMove = true;
            if(path.Count == 1){
                 pickUpObj(tempGun);
                 isMove = true;
            }
            }else{
                handleMove(this.gameObject);
                isMove = true;
                guns = null;
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
    /*private void updatePath(GameObject target){
        Vector3 temp = target.transform.position;
        if (pathfinding.GetNode((int)temp.x, (int)temp.y).blocked){
            if (!pathfinding.GetNode((int)temp.x, (int)temp.y + 1).blocked){
                temp = new Vector3(temp.x, temp.y +1, 0f);
            }
            if (!pathfinding.GetNode((int)temp.x+1, (int)temp.y + 1).blocked){
                temp = new Vector3(temp.x+1, temp.y +1, 0f);
            }
            if (!pathfinding.GetNode((int)temp.x+1, (int)temp.y).blocked){
                temp = new Vector3(temp.x +1, temp.y , 0f);
            }
            
            if (!pathfinding.GetNode((int)temp.x, (int)temp.y - 1).blocked){
                temp = new Vector3(temp.x, temp.y -1, 0f);
            }
            if (!pathfinding.GetNode((int)temp.x-1, (int)temp.y - 1).blocked){
                temp = new Vector3(temp.x-1, temp.y -1, 0f);
            }
            if (!pathfinding.GetNode((int)temp.x-1, (int)temp.y).blocked){
                temp = new Vector3(temp.x -1, temp.y , 0f);
            }
        }

        List<Node> tempt = pathfinding.FindPath((int)this.gameObject.transform.position.x, (int)this.gameObject.transform.position.y, (int)temp.x, (int)temp.y);
        
        if (tempt.Count != 0){
            path = tempt;
        }
        
    }*/
    private void doMove(Vector3 thisPoint){
       
        //Vector3 temp = pathfinding.GetWorldNode(x,y);
        Vector3 tempTemp = player.transform.position - this.transform.position;
       
        if(tempTemp.magnitude > 1.2){

        
        //rb.velocity = (temp - this.gameObject.transform.position).normalized * speed;
        agent.SetDestination(thisPoint);
        anim.startMove();
        }else{
          rb.velocity = Vector3.zero;
          anim.stopMove();  
          

          
        if(punchCD <= 0f){
        punchCD = 2f;
        timeToPunch = true;
        anim.shouldPunch(); 
        }if(punchCD > 1 && punchCD <= 1.30f){RaycastHit2D punchCast =  Physics2D.Raycast(transform.position, transform.up, 1.2f, punchlm);
        if (punchCast.collider != null){
            punchCast.collider.gameObject.GetComponent<StunScript>().stunMe();
        }}
    }
        }
    
    private GameObject getNearestGun(){
        GameObject temp = null;
        List<Node> tempPath;
        foreach(GameObject gun in guns){
            
            if(gun.GetComponent<gunScript>().hasBullets > 0 && gun.transform.parent == null){
                if(temp != null){
                    tempPath = pathfinding.FindPath((int)this.gameObject.transform.position.x, (int)this.gameObject.transform.position.y, (int)gun.transform.position.x, (int)gun.transform.position.y);
                    if(tempPath.Count < temp.GetComponent<gunScript>().distanceFromNpc){
                    temp = gun;
                    temp.GetComponent<gunScript>().distanceFromNpc = tempPath.Count;
                    }
                    
                }else{
                    
                    tempPath = pathfinding.FindPath((int)this.gameObject.transform.position.x, (int)this.gameObject.transform.position.y, (int)gun.transform.position.x, (int)gun.transform.position.y);
                    temp = gun;
                    temp.GetComponent<gunScript>().distanceFromNpc = tempPath.Count;
                    
                }
            }
                
            
        }
        return temp;
    }
    private void handleMove(GameObject targetObject){
          /*
               if (new Vector2(Mathf.Round(targetObject.transform.position.x), Mathf.FloorToInt(targetObject.transform.position.y)) == prevPosition){
        if (path.Count > 0){
            Vector3 distance = pathfinding.GetWorldNode(path[0].x,path[0].y) - this.transform.position;
        
        if(distance.magnitude <= satisfationRange){
            if(path.Count > 1){
                path.RemoveAt(0);
                doMove(path[0].x, path[0].y);
            }else{
                doMove(path[0].x, path[0].y);
            }
        }else{
            doMove(path[0].x, path[0].y);
        }
        }}else{
            prevPosition = new Vector2(Mathf.Round(targetObject.transform.position.x), Mathf.FloorToInt(targetObject.transform.position.y));
            updatePath(targetObject);
            if (path.Count > 0){
            Vector3 distance =  pathfinding.GetWorldNode(path[0].x,path[0].y) - this.transform.position;
       
        if(distance.magnitude <= satisfationRange){
            if(path.Count > 1){
                
                path.RemoveAt(0);
                doMove(path[0].x, path[0].y);
            }else{
                doMove(path[0].x, path[0].y);
            }
        }else{
            doMove(path[0].x, path[0].y);
        }
        }
        }*/
        doMove(targetObject.transform.position);
    }
     private void pickUpObj(GameObject obj){
        
        holdingThis = obj;
        holdingThis.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        obj.transform.SetParent(this.transform, true);
        obj.transform.localPosition = Vector3.up * .5f;
        holdingThis.GetComponent<Rigidbody2D>().simulated = false; 
        
        obj.transform.rotation = this.transform.rotation;
        holding = true;
    }

    void OnCollisionEnter2D(Collision2D cal){
       
        if(cal.gameObject.tag == "gun" || cal.gameObject.tag == "liftAble"){
            if(cal.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude >= dropThreshhold){
                drop();
            }
        }
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
    /*void LateUpdate(){
            if(player.GetComponent<pickUp>().punchCD > 0f && player.GetComponent<pickUp>().punchCD < 1f){            
            blocked = GameObject.Find("AiGrid").GetComponent<blockedList>().getBlockedList();

            pathfinding = new Pathfinding(height, width, blocked);
            }
    {
            if(firstFrame){            
            blocked = GameObject.Find("AiGrid").GetComponent<blockedList>().getBlockedList();
            firstFrame = false;
            pathfinding = new Pathfinding(height, width, blocked);
            }
    }
    }*/
    }
   /* void FixedUpdate(){
        agent.SetDestination(player.transform.position);
    }
    void Update(){
        Surface2D.UpdateNavMesh(Surface2D.navMeshData);
       
    }
*/

    
