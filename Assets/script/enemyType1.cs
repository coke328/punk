using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyType1 : MonoBehaviour
{
    public GameObject chaseSector;
    public Transform playerPos;
    private float wanderingStart;
    private float wanderingEnd;
    public float wanderingMaxspeed = 3;
    public float wanderingAcc = 0.5f;
    public float chaseMaxspeed = 5;
    public float chaseAcc = 0.5f;
    private Rigidbody2D rb;
    private SpriteRenderer spRend;
    private Animator anim;
    private bool direction = true;
    private bool lastTmp = false;
    public float distanceMatch = 1.5f;//애니메이션 아이들 상태 들어가려면기위한 최소 속도
    public float damageCoolTime = 1;
    public float damageT;
    public hp hpScript;
    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spRend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        wanderingStart = chaseSector.transform.GetChild(0).transform.position.x;
        wanderingEnd = chaseSector.transform.GetChild(1).transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {

        if(chaseSector.GetComponent<chaseSector>().playerIn){
            chasePlayer();
        }else{
            wandering();
        }

        look();
        Animation();
    }

    void wandering(){
        float x = transform.position.x;
        if(x < wanderingStart){
            direction = true;
        }else if(x > wanderingEnd){
            direction = false;
        }

        x = rb.velocity.x;
        if(direction && x < wanderingMaxspeed){
            x += wanderingAcc;
        }else if(!direction && x > -wanderingMaxspeed){
            x -= wanderingAcc;
        }
        rb.velocity = new Vector2(x,rb.velocity.y);
    }

    void chasePlayer(){
        float x = transform.position.x;
        float px = playerPos.position.x;
        if(x < px){
            direction = true;
        }else if(x > px){
            direction = false;
        }

        x = rb.velocity.x;
        if(direction && x < chaseMaxspeed){
            x += chaseAcc;
        }else if(!direction && x > -chaseMaxspeed){
            x -= chaseAcc;
        }
        rb.velocity = new Vector2(x,rb.velocity.y);
    }

    void look(){
        if(direction){
            spRend.flipX = false;
        }else{
            spRend.flipX = true;
        }
    }

    void Animation(){
        
        bool tmp = Mathf.Abs(rb.velocity.x) < distanceMatch;
        if(tmp){

            if(!lastTmp){
                anim.Play("idle");
            }
            lastTmp = true;
        }else{

            if(lastTmp){
                anim.Play("run");
            }
            lastTmp = false;
        }
    }
    void OnCollisionEnter2D(Collision2D o){
        if(o.transform.CompareTag("Player")){
            hpScript.damage(damage);
            damageT = damageCoolTime;
        }
    }

    void OnCollisionStay2D(Collision2D o){
        if(o.transform.CompareTag("Player")){
            if(damageT < 0){
                hpScript.damage(damage);
                damageT = damageCoolTime;
            }

            damageT -= Time.deltaTime;
        }
    }
}
