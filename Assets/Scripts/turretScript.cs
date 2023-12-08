using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretScript : MonoBehaviour
{
    public bool tryToShoot; 
    public float delay = 0f;
    public float shootCD = 0.5f;
    public float currentCD = 0f;
    public GameObject bullet;
    public GameObject thisTurret;
    public List<GameObject> bulletsPos;
    private Recorder recorder;
    private bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {
         EventManager.onGoalReached += OnGoalReached;
         EventManager.onRestartLevel += OnRestartLevel;
    }
      private void Awake()
    {
        recorder = GetComponent<Recorder>();
    } 
    private void OnDisable(){
         EventManager.onGoalReached -= OnGoalReached;
         EventManager.onRestartLevel -= OnRestartLevel;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canShoot){
        if (delay <= 0f){
            tryToShoot = true;
        }else{
            delay -= Time.deltaTime;
        }
        if(tryToShoot){
            if (currentCD >= shootCD){
                GameObject newBull = Object.Instantiate(bullet, new Vector3(0,-.5f,0) + thisTurret.transform.position, Quaternion.identity);
                bulletsPos.Add(newBull);
                newBull.GetComponent<bulletScript>().turret = this.gameObject;
                currentCD = 0f;
            }else{
                currentCD += Time.deltaTime;
            }
        }


        }
        List<Vector3> positions = new List<Vector3>();
        foreach(GameObject obj in bulletsPos){
            positions.Add(obj.transform.position);
        }
       // ReplayData data = new TurretReplayData(this.transform.position, this.transform.rotation, positions);
        //recorder.RecordReplayFrame(data);
    }
    private void OnGoalReached(){
        canShoot = false;

       
    }
    private void OnRestartLevel(){
        canShoot = true;

       
    }

}
