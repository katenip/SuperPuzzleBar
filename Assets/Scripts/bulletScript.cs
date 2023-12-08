using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float bulletSpeed = 3f; 
    public GameObject thisBullet;
    public GameObject turret; 
    // Start is called before the first frame update
    void Start()
    {
    thisBullet = this.gameObject;
    rb.velocity = this.transform.up * bulletSpeed;
        
    }
    void FixedUpdate(){
        rb.velocity = this.transform.up * bulletSpeed;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        
        if(collision.collider.gameObject.transform.tag == "Player"){
            collision.gameObject.GetComponent<pickUp>().drop();
            GameObject.Find("Player").GetComponent<StunScript>().stunMe();
        }
        destroyThis(thisBullet);
    }
    void destroyThis(GameObject thisBullet){
        turret.GetComponent<BossBun>().bulletsPos.Remove(thisBullet);
        Destroy(thisBullet);
    }

 
}
