using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class playerMoveScript : MonoBehaviour
{
    public Rigidbody2D rb; 
    public GameObject player;
    public Vector2 moveDir; 
    public float moveSpeed = 5f;
    public TurnBased turnControl; 
    private Recorder recorder;
    private bool goFast;
    public Vector2 origin; 
    public bool canMove = true;
    public float stunCD = 0f;
    public Camera cam;
    public playerAnimationController anim;
    private bool shouldPunch;
    private bool isMove = false;
    public audioManager audi;
    public bool dead = false;
    public GameObject panel;
    public GameObject fade;
    public GameObject winPanel;
    public GameObject levelManager;
    private void Start(){
        EventManager.onPlayerDeath += OnPlayerDeath;
        EventManager.onRestartLevel += OnRestartLevel;
        EventManager.onGoalReached += OnGoalReached;
        goFast = false;
        origin = this.gameObject.transform.position;
    }
    

    private void Awake()
    {
        levelManager = GameObject.Find("levelManager");
        winPanel.SetActive(false);
        audi = GetComponent<audioManager>();
        player = this.gameObject;
        rb = player.GetComponent<Rigidbody2D>();
        recorder = player.GetComponent<Recorder>();
        anim =  player.GetComponent<playerAnimationController>();
        turnControl = GameObject.Find("TimeObject").GetComponent<TurnBased>();
        Color col = new Color(fade.GetComponent<Image>().color.r, fade.GetComponent<Image>().color.g, fade.GetComponent<Image>().color.b, 0f);
        fade.GetComponent<Image>().color = col;
        
    } 

    
    void Update()
    
    {
        if(!dead){
        if(this.gameObject.GetComponent<pickUp>().punchCD <= 0){
        shouldPunch = false;
    }
        if(canMove){
    if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0){
    moveDir = new Vector2(Input.GetAxisRaw("Horizontal"),  Input.GetAxisRaw("Vertical"));
    rb.velocity = moveDir * moveSpeed;
    if(!goFast){
        anim.startMove();
        audi.Walk();
        isMove = true;
    turnControl.ResumeGame();
    }}else{
        anim.stopMove();
        audi.StopWalk();
        isMove = false;
    rb.velocity = Vector2.zero;
    if(!goFast){
    turnControl.PauseGame();
    }
    }
    
    if (Input.GetKeyDown(KeyCode.R)){
       StartCoroutine(restartCoroutine(.1f));
    }
    if (Input.GetKeyDown(KeyCode.Escape)){
        levelManager.GetComponent<playLevel>().loadMainMenu();
    }
    Vector2 mousePos = new Vector2();
    mousePos.x = Input.mousePosition.x;
    mousePos.y = Input.mousePosition.y;
    
    Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    Debug.DrawLine(this.gameObject.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    diff.Normalize();
   
    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    
    }else{
        turnControl.ResumeGame();
    }
    }else{
        dieScreen();
        turnControl.ResumeGame();
        if (Input.GetKeyDown(KeyCode.R)){
       StartCoroutine(restartCoroutine(.1f));
    }if (Input.GetKeyDown(KeyCode.Escape)){
        levelManager.GetComponent<playLevel>().loadMainMenu();
    }
    }
    }
    void FixedUpdate(){
            if(stunCD > 0f){
            stunCD = stunCD - Time.deltaTime;
            }else{
                stunCD = 0f;
                canMove = true;
            }
        
        ReplayData data = new PlayerReplayData(this.transform.position, this.transform.rotation, isMove, this.gameObject.GetComponent<health>().hp, shouldPunch, !canMove);
        recorder.RecordReplayFrame(data);
    }
    void OnRestartLevel(){
        
         winPanel.SetActive(false);
         AudioListener.volume = PlayerPrefs.GetFloat("volume") / 100;
    }
    void OnPlayerDeath(){
        dead = true;
    }
     IEnumerator restartCoroutine(float timeTo)
    {
        
       
        this.transform.position = origin;
        rb.velocity = Vector3.zero;
        canMove = false;
        this.gameObject.GetComponent<Renderer>().enabled = false;
        goFast = true;
        turnControl.ResumeGame();
        
        yield return new WaitForSeconds(timeTo);
        goFast = false; 
        canMove = true;
        this.gameObject.GetComponent<Renderer>().enabled = true;
        dead = false;
        goFast = false;
        EventManager.OnRestartLevel();

        panel.SetActive(false);
        Color col = new Color(fade.GetComponent<Image>().color.r, fade.GetComponent<Image>().color.g, fade.GetComponent<Image>().color.b, 0f);
        fade.GetComponent<Image>().color = col;
        
    }
    void OnGoalReached(){
        winPanel.SetActive(true);
    }
    private void OnDisable(){
    EventManager.onPlayerDeath -= OnPlayerDeath;
    EventManager.onRestartLevel -= OnRestartLevel;
    EventManager.onGoalReached -= OnGoalReached;
    }
    public void shouldStun(){
        anim.Stun();
        audi.Stun();
        canMove = false;
        
    }
    public void shouldPunchThing(){
        this.shouldPunch = true;
        audi.Punch();
    }
    public void dieScreen(){
    
        panel.SetActive(true);
        Color col = new Color(fade.GetComponent<Image>().color.r, fade.GetComponent<Image>().color.g, fade.GetComponent<Image>().color.b, fade.GetComponent<Image>().color.a + (Time.deltaTime / 5));
        fade.GetComponent<Image>().color = col;
    }
    
    
  
   
}
