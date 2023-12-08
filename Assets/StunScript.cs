using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunScript : MonoBehaviour
{
    public playerAnimationController anim;
    public float iFrame =0f;
    public bool stunned;
    public float stunCD;

    // Start is called before the first frame update
    void Start()
    {
        anim =  this.gameObject.GetComponent<playerAnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(stunned){
            if(stunCD >= 0f){
                stunCD = stunCD - Time.deltaTime;
            }else{
                stunned = false;
            }
        }
    }
    public void stunMe(){
        
        if(this.gameObject == GameObject.Find("Player") && !stunned && this.gameObject.GetComponent<health>().hp > 0){
            anim.Stun();
            this.gameObject.GetComponent<playerMoveScript>().canMove = false;
            this.gameObject.GetComponent<playerMoveScript>().stunCD = 2f;
           this.gameObject.GetComponent<pickUp>().drop();
            Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
            this.gameObject.GetComponent<Rigidbody2D>().velocity = this.transform.up * -1 * 3f;
            GetComponent<playerMoveScript>().audi.Stun();
    this.gameObject.GetComponent<health>().hp = this.gameObject.GetComponent<health>().hp - 30;
            stunned = true;
            stunCD = 1.5f;

        }
        if(this.gameObject.tag == "box"  && !stunned){
            this.gameObject.transform.position = new Vector3(1000f, 1000f, 0f);
            for(int x = 0; x < transform.childCount -1; x++){
                transform.GetChild(x).tag = "box";
            }
            GameObject.Find("Player").GetComponent<playerMoveScript>().audi.Stun();
            stunned = true;
            stunCD = .5f;
        }
        if(this.gameObject.tag == "npc"  && !stunned && this.gameObject.GetComponent<health>().hp > 0){
            anim.Stun();
            this.gameObject.GetComponent<aiScript>().stuned = true;
            this.gameObject.GetComponent<aiScript>().stunCD = 2f;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = this.transform.up * -1 * 3f;
            this.gameObject.GetComponent<health>().hp = this.gameObject.GetComponent<health>().hp - 40;
            GetComponent<aiScript>().audi.Stun();
            stunned = true;
            stunCD = .5f;
        }
        if(this.gameObject.tag == "bossnpc"  && !stunned && this.gameObject.GetComponent<health>().hp > 0){
            anim.Stun();
            this.gameObject.GetComponent<bossScript>().stuned = true;
            this.gameObject.GetComponent<bossScript>().stunCD = 1.5f;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = this.transform.up * -4 * 1f;
            this.gameObject.GetComponent<health>().hp = this.gameObject.GetComponent<health>().hp - 20;
            GetComponent<bossScript>().drop();
            GetComponent<bossScript>().audi.Stun();
            stunned = true;
            stunCD = 1.5f;
        }
    }
    public void stunMeHard(){
        if(this.gameObject == GameObject.Find("Player") && !stunned && this.gameObject.GetComponent<health>().hp > 0){
            anim.Stun();
            this.gameObject.GetComponent<playerMoveScript>().canMove = false;
            this.gameObject.GetComponent<playerMoveScript>().stunCD = 2.5f;
           this.gameObject.GetComponent<pickUp>().drop();
            Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
            this.gameObject.GetComponent<Rigidbody2D>().velocity = this.transform.up * -1 * 7f;
            GetComponent<playerMoveScript>().audi.Stun();
    this.gameObject.GetComponent<health>().hp = this.gameObject.GetComponent<health>().hp - 30;
            stunned = true;
            stunCD = 1.5f;

        }
    }
   

}
