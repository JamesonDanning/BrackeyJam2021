using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public bool playerNum = false;
    public float baseSpeed;
    public float SpeedBoost;
    public int jumpPower;
    public float fallSpeedMult;
    public float lowJumpMult;

    public float shoeMultiplier = 1;
  


    private bool grounded;
    private bool lastPressed;
    private int counter = 0;
    private int jumpPressed;
    private int direction = 1;

    //public Animator anim;
    private Rigidbody2D rb;
    //public TrailRenderer trail;
    //private int honsePerH;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        //SpeedBoost = 50 +  baseSpeed * (30f / EventManager.Instance.horseFront.GetComponent<horseDistance>().distance);
        //honsePerH = EventManager.Instance.HonsePerHour;
        if(counter == 2) {
            counter = 0;
            doSpeed();
        }
        if(rb.velocity.y < 0){ 
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallSpeedMult) * Time.deltaTime;
        } else if (rb.velocity.y > 0 && jumpPressed == 0) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMult) * Time.deltaTime;
        }
        // anim.SetFloat("velocity", rb.velocity.x/10f);
        // if(honsePerH > 500) {
        //     trail.enabled = true;
        // } else {
        //     trail.enabled = false;
        // }

        
    }

    #region player controls

    void OnLeftPump(){
        
        if(!lastPressed) {
            //Debug.Log("LEFT");
            lastPressed = !lastPressed;
            counter++;
        } else {
            doSpeedDown();
            counter = 0;
        }
         
        
    }
    
    void OnRightPump(){
        
        if(lastPressed){
            // Debug.Log("RIGHT");
            lastPressed = !lastPressed;
            counter++;
        } else {
            doSpeedDown();
            counter = 0;
        }
          
    }

    void OnJump(InputValue value) {
        if(!playerNum){
            if(grounded && value.Get<float>() == 1f) {
                grounded = false;
                jumpPressed = (int) value.Get<float>();
                rb.AddForce(new Vector2(0, jumpPower));
                //anim.SetTrigger("jump");
            }
        }
    }

    void OnSwitchDirection()
    {
        direction = -1 * direction;
    }

    

    #endregion


    void doSpeed(){
        //Debug.Log("adding speed");
        rb.AddForce(new Vector2(SpeedBoost * shoeMultiplier * direction, 0));
    }

    void doSpeedDown(){
        //rb.velocity = new Vector2(rb.velocity.x * 0.8f, rb.velocity.y);
    }

    // void OnPowerUp(){
    //     Debug.Log("POWER UP SPACE");
    //     EventManager.Instance.activatePowerUp();
    // }

    // private void OnCollisionEnter2D(Collision2D other) {
    //     if(other.transform.tag == "ground") {
    //         grounded = true;
    //         anim.SetTrigger("run");
    //     }
    //     if(other.transform.tag == "obstacle") {
    //         //do trip animation
    //         //rb.velocity = new Vector2(0, rb.velocity.y);
    //     }
    //     if(other.transform.tag == "frontHorse")
    //     {
    //         if(EventManager.Instance.currPowerUp != null){
    //             if(EventManager.Instance.currPowerUp.getPowerUpNum() == 0){
    //                 GetComponent<DistanceJoint2D>().enabled = true;
    //             }
    //         }
           
    //     }
    // }
    
}
