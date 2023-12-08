using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBun : MonoBehaviour
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
    public Vector3 originPosi;
    public bool shott = false;
    // Start is called before the first frame update
    void Start()
    {
         originPosi= transform.localPosition;
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
            
        }
        
            
            currentCD -= Time.deltaTime;
        


        
        List<Vector3> positions = new List<Vector3>();
        List<Quaternion> rotations = new List<Quaternion>();
        foreach(GameObject obj in bulletsPos){
            positions.Add(obj.transform.position);
            rotations.Add(obj.transform.rotation);
        }
        if(transform.parent != null){
         Quaternion rotation = Quaternion.LookRotation
            (player.transform.position - transform.position, transform.TransformDirection(-Vector3.forward));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);}
        ReplayData data = new TurretReplayData(this.transform.position, this.transform.rotation, positions, rotations, shott);
        shott = false;
        recorder.RecordReplayFrame(data);
    }
    private void OnGoalReached(){
        canShoot = false;

       
    }
    private void OnRestartLevel(){
        
     
       
    }
    private void tryToShoot(){
         if (currentCD <= 0f){
                GameObject newBull = Object.Instantiate(bullet, chamber.transform.position, Quaternion.identity);
                GetComponent<AudioSource>().volume = (PlayerPrefs.GetFloat("volume") / 100) / 4; 
                GetComponent<AudioSource>().Play();
                newBull.transform.rotation = this.gameObject.transform.rotation;
                bulletsPos.Add(newBull);
                newBull.GetComponent<bulletScript>().turret = this.gameObject;
                currentCD = 1f;
                shott = true;
                //hasBullets = hasBullets - 1;
                }
            }
           
            
    }
    


