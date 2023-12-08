using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunScript : MonoBehaviour
{
    public bool shouldShoot; 
    public float delay = 0f;
    public float shootCD = 0.5f;
    public float currentCD = 0f;
    public GameObject bullet;
    public GameObject thisTurret;
    public List<GameObject> bulletsPos;
    private Recorder recorder;
    public bool canShoot = false;
    public GameObject chamber; 
    public GameObject player;
    public int hasBullets; 
    public int startBull; 
    public int distanceFromNpc;
    private Vector3 originPos;
    private Quaternion startRot;
    // Start is called before the first frame update
    void Start()
    {
        
         EventManager.onGoalReached += OnGoalReached;
         EventManager.onRestartLevel += OnRestartLevel;
    }
      private void Awake()
    {
        originPos = transform.position;
        startRot = transform.rotation;
        player = GameObject.Find("Player");
        recorder = GetComponent<Recorder>();
        startBull = hasBullets;
    } 
    private void OnDisable(){
         EventManager.onGoalReached -= OnGoalReached;
         EventManager.onRestartLevel -= OnRestartLevel;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.gameObject.transform.IsChildOf(player.transform)){
            if(Input.GetMouseButtonDown(0)){
                shouldShoot = true;
            }
            }
       
        if(shouldShoot){
            tryToShoot();
            shouldShoot = false;
        }
        
            
            currentCD += Time.deltaTime;
        


        
        List<Vector3> positions = new List<Vector3>();
        List<Quaternion> rotations = new List<Quaternion>();
        foreach(GameObject obj in bulletsPos){
            positions.Add(obj.transform.position);
            rotations.Add(obj.transform.rotation);
        }
        ReplayData data = new TurretReplayData(this.transform.position, this.transform.rotation, positions, rotations, true);
        recorder.RecordReplayFrame(data);
    }
    private void OnGoalReached(){
        canShoot = false;

       
    }
    private void OnRestartLevel(){
        transform.SetParent(null, true);
        this.transform.position = originPos;
        this.transform.rotation = startRot;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        hasBullets = startBull;

       
    }
    private void tryToShoot(){
         if (currentCD >= shootCD && hasBullets > 0){
                GameObject newBull = Object.Instantiate(bullet, chamber.transform.position, Quaternion.identity);
                newBull.transform.rotation = this.gameObject.transform.rotation;
                bulletsPos.Add(newBull);
                newBull.GetComponent<bulletScript>().turret = this.gameObject;
                currentCD = 0f;
                //hasBullets = hasBullets - 1;
                }
            }
      
            
    }
    


