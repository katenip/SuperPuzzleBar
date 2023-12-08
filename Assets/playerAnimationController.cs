using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimationController : MonoBehaviour
{
    Animator m_Animator;
    public bool isStuned;

    
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }



    public void shouldPunch(){
        m_Animator.SetTrigger("Punch");
    }
    public void startMove(){
        m_Animator.SetBool("Walking", true);
    }
    public void Stun(){
        m_Animator.SetTrigger("Stun");
    }
    public bool isStun(){
    return isStuned;
    }
    public void stopMove(){
        m_Animator.SetBool("Walking", false);
    }
}
