using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMoveMent : MonoBehaviour
{
    private float hori;
    public float speed;
    public float acc = 4f;
    public float MaxSpeed = 4f;
    public float velX = 0f;
    public bool runing = true;
    public float drag = 0.99f;
    public float airMaxSpeed = 5f;
    public float airAcc = 2f;
    public float airDrag = 2f;
    public float jumpForce;
    private Rigidbody2D rb;
    private BoxCollider2D bottomcol;
    private BoxCollider2D rightcol;
    private BoxCollider2D leftcol;
    private Animator anim;
    private SpriteRenderer spRend;
    private bool lastground = false;
    private bool jump = false;
    private bool land = false;
    public float climbTimeMax = 3f;
    public float climbTime = 0f;
    public float climbSpeed = 1f;
    public float climbStateCharge = 5f;
    public Vector2 wallJumpPower;
    private bool Crouch = false;
    public float crouchAcc = 40;
    public float crouchMaxSpeed = 3f;
    public float crouchDrag = 7;
    private BoxCollider2D crouchCol;
    private BoxCollider2D standardCol;
    private BoxCollider2D crouchHeadCol;
    
    // Start is called before the first frame update

    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bottomcol = transform.GetChild(0).GetComponent<BoxCollider2D>();
        rightcol = transform.GetChild(1).GetComponent<BoxCollider2D>();
        leftcol = transform.GetChild(2).GetComponent<BoxCollider2D>();
        crouchCol = transform.GetChild(3).GetComponent<BoxCollider2D>();
        crouchHeadCol = transform.GetChild(4).GetComponent<BoxCollider2D>();
        standardCol = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        spRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {   
        bool isGround = bottomcol.IsTouchingLayers(LayerMask.GetMask("ground"));
        bool rightTouch = rightcol.IsTouchingLayers(LayerMask.GetMask("ground"));
        bool leftTouch = leftcol.IsTouchingLayers(LayerMask.GetMask("ground"));
        bool crouchHeadTouch = crouchHeadCol.IsTouchingLayers(LayerMask.GetMask("ground"));

        hori = Input.GetAxisRaw("Horizontal");
        /*
        velX += hori * acc * Time.deltaTime;

        if(velX > 1){ velX = 1; }
        else if(velX < -1) { velX = -1; }

        if(isGround && velX != 0) {
            velX -= velX / Mathf.Abs(velX) * drag * Time.deltaTime;
            //velX *= drag * Time.deltaTime;
        }
        */
        if(hori > 0){
            spRend.flipX = false;
        }else if(hori < 0){
            spRend.flipX = true;
        }

        //if(velX > -0.01f && velX < 0.01f && runing){velX = 0;}


        if(rightTouch && Input.GetKey(KeyCode.D) && climbTime > 0 && !Crouch){
            if(rb.velocity.y < climbSpeed * 1){rb.AddForce(new Vector2(0,1) * climbSpeed);}
            //rb.velocity = new Vector2(rb.velocity.x,climbSpeed);

            climbTime -= Time.deltaTime;

            if(Input.GetKeyDown("space") && !isGround){
                rb.AddForce(new Vector2(-wallJumpPower.x,wallJumpPower.y));
            }
        }else if(leftTouch && Input.GetKey(KeyCode.A) && climbTime > 0 && !Crouch){
            if(rb.velocity.y < climbSpeed * 1){rb.AddForce(new Vector2(0,1) * climbSpeed);}
            //rb.velocity = new Vector2(rb.velocity.x,climbSpeed);

            climbTime -= Time.deltaTime;

            if(Input.GetKeyDown("space") && !isGround){
                rb.AddForce(wallJumpPower);
            }
        }else if(!rightTouch && !leftTouch) {
            climbTime += Time.deltaTime * climbStateCharge;
            if(climbTime > climbTimeMax){
                climbTime = climbTimeMax;
            }
        }else if(isGround){
            climbTime += Time.deltaTime * climbStateCharge;
            if(climbTime > climbTimeMax){
                climbTime = climbTimeMax;
            }
        }

        if(Input.GetKeyDown(KeyCode.LeftShift)){
            if(!Crouch){
                Crouch = true;
                standardCol.enabled = false;
                crouchCol.enabled = true;
            }else if(!crouchHeadTouch && Crouch) {
                Crouch = false;
                standardCol.enabled = true;
                crouchCol.enabled = false;
            }
        }

        if(!isGround){runing = false;}
        else if(Crouch){
            runing = false;
        }else{
            runing = true;
        }

        float a = 0;
        float Mspeed = 0;
        float d = 0;

        if (runing){
            a = acc;
            Mspeed = MaxSpeed;
            d = drag;
        }else if(!Crouch) {
            a = airAcc;
            Mspeed = airMaxSpeed;
            d = airDrag;
        }else if(isGround) {
            a = crouchAcc;
            Mspeed = crouchMaxSpeed;
            d = crouchDrag;
        }

        float x = rb.velocity.x;
        if(Mspeed > x && hori > 0){
            x += hori * a * Time.deltaTime;
        }else if(-Mspeed < x && hori < 0){
            x += hori * a * Time.deltaTime;
        }
        if(isGround && x != 0) {
            x -= x / Mathf.Abs(x) * d * Time.deltaTime;
        }
        rb.velocity = new Vector2(x,rb.velocity.y);
        

        if(isGround && Input.GetKeyDown("space")){
            rb.AddForce(new Vector2(0,1)*jumpForce);
            anim.Play("jump");
            
            Invoke("jumpFalse",0.08f);
        }
        
        if(isGround != lastground){
            anim.Play("land");
            land = true;

            Invoke("landFalse",0.08f);
        }else{land = false;}

        bool run;
        if(hori != 0){
            run = true;
        }else{
            run = false;
        }

        anim.SetBool("runing",run);
        anim.SetBool("onground", isGround);
        anim.SetBool("crouch",Crouch);

        lastground = isGround;

    }

    void jumpFalse(){
        anim.Play("idle");
        jump = false;
    }
    void landFalse(){
        anim.Play("idle");
        land = false;
    }
}
