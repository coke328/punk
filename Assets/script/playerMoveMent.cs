using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMoveMent : MonoBehaviour
{
    private float hori;//키보드 a,d 입력
    public float acc = 40f;//땅에서 가속도
    public float MaxSpeed = 8f;//땅에서 최대 속도
    public bool runing = true; //웅크리기,공중에 뜸 제외 상태
    public float drag = 15f;//땅에서의 마찰
    public float airMaxSpeed = 10f;//공중에서 최고 속도
    public float airAcc = 20f;//공중에서 가속도
    public float airDrag = 2f;//공중에서의 공기 저항
    public float jumpForce = 700;//점프 힘
    private Rigidbody2D rb;
    private BoxCollider2D bottomcol;//캐릭터 발밑 접촉 감지
    private BoxCollider2D rightcol;//캐릭터 오른쪽 접촉 감지
    private BoxCollider2D leftcol;//캐릭터 왼쪽 접촉 감지
    private Animator anim;
    private SpriteRenderer spRend;
    private bool lastground = false;//이전 프레임에 땅에 닿아있었는지
/*    public float climbTimeMax = 3f;  //기어오르기 기능 변수들
    public float climbTime = 0f;
    public float climbSpeed = 1f;
    public float climbStateCharge = 5f;*/
    public Vector2 wallJumpPower = new Vector2(500,700);//벽점프 힘
    public bool Crouch = false;//웅크리기 상태
    public float crouchAcc = 30;//웅크리기 상태의 가속도
    public float crouchMaxSpeed = 2f;//웅크리기 상태의 최고 속도
    public float crouchDrag = 7;//웅크리기 상태의 마찰력
    private BoxCollider2D crouchCol;//웅크렸을때의 캐릭터 콜라이더
    private BoxCollider2D standardCol;//기본상태의 캐릭터 콜라이더
    private BoxCollider2D crouchHeadCol;//웅크렸을떄 머리위에 접촉 감지
    public bool able_dash = true;//대쉬가 가능한지
    private bool Dashing = false;//대쉬를 하고있는지
    public Vector2 dashPower = new Vector2(30,0);//대쉬 힘
    public float dashTime = 0.07f;//대쉬 시간
    public float dashCoolTime = 1f;//대쉬 쿨타임
    private float dashT = 0f;//대쉬하고나서 지난시간
    private Vector2 velBefDash;//대쉬이전의 속도 저장하는 곳
    private bool isGround;
    private bool spontaneityAnim = false;
    public float jumpAnimTime = 0.35f;
    public float landAnimTime = 0.1f;
    public float crouchingTIme = 0.3f;
    public float wallJumpAnimTime = 0.3f;
    public float slidingSpeed = 3f;
    public int currentAnim = 0;
    public bool secondJumpAble = true;
    public Vector2 secondJumpPower  = new Vector2(0,15);
    public float secondJumpAnimTime = 0.5f;
    public float wallGrabSpeed = 3f;
    private bool grabWall = false;
    private bool lookAble = true;

    void Start()
    {   //컴포넌트 불러오기
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

    void Update()
    {   
        //시작 셋업
        updateSetup();

        //캐릭터 좌우 돌아보기
        look();
        
        //벽 점프
        if(rightcol.IsTouchingLayers(LayerMask.GetMask("ground")) && Input.GetKey(KeyCode.D)){
            wallGrab();

            wallJump(true);
            
        }else if(leftcol.IsTouchingLayers(LayerMask.GetMask("ground")) && Input.GetKey(KeyCode.A)){
            wallGrab();

            wallJump(false);
            
        }else{
            grabWall = false;
        }

        //웅크리기 기능
        if(Input.GetKeyDown(KeyCode.LeftControl) && !Dashing){
            crouch();
        }
        
        //기본상태 변화
        state();

        //움직임
        movement();
        
        //점프 기능
        if(isGround && Input.GetKeyDown("space") && !Dashing){
            Jump();
        }

        //이단점프
        if(!isGround && secondJumpAble && Input.GetKeyDown("space") && !spontaneityAnim){

            secondJump();
        }

        //대쉬 기능
        if(able_dash && dashT > dashCoolTime && Input.GetKeyDown(KeyCode.LeftShift)){
            if(Input.GetKey(KeyCode.A)){
                dash(false);
            }else if(Input.GetKey(KeyCode.D)){
                dash(true);
            }
        }

        //애니메이션
        Animation();

        //마지막 업데이트
        updateLast();
    }

    private void updateSetup(){
        isGround = bottomcol.IsTouchingLayers(LayerMask.GetMask("ground"));//지면과 접촉여부를 판단하는 변수

        hori = Input.GetAxisRaw("Horizontal");//좌우 입력
    }
    private void updateLast(){
        lastground = isGround;
        dashT += Time.deltaTime;
    }
    private void look(){
        if(lookAble){
            if(rb.velocity.x > 0){
                spRend.flipX = false;
            }else if(rb.velocity.x < 0){
                spRend.flipX = true;
            }
        }
    }
    public void Jump(){
        rb.AddForce(new Vector2(0,1)*jumpForce);
        changeAnim((int)animIndex.firstJump);//점프 애니메이션 재생
        spontaneityAnim = true;
        Invoke("toIdle",jumpAnimTime);//일정시간뒤 점프애니메이션 정지
    }
    public void secondJump(){
        rb.velocity = new Vector2(rb.velocity.x,secondJumpPower.y);
        changeAnim((int)animIndex.secondJump);
        spontaneityAnim = true;
        Invoke("toIdle",secondJumpAnimTime);
        secondJumpAble = false;
    }
    public void wallJump(bool dir){
        if(Input.GetKeyDown("space") && !Crouch && !isGround && !Dashing){
            spontaneityAnim = true;
            lookAble = false;
            if(dir){
                rb.AddForce(new Vector2(-wallJumpPower.x,wallJumpPower.y));
                
            }else{
                
                rb.AddForce(wallJumpPower);
            }
            changeAnim((int)animIndex.wallJump);
            Invoke("toIdle",wallJumpAnimTime);
            Invoke("lookTrue",wallJumpAnimTime);
        }
    }

    private void wallGrab(){
        if(rb.velocity.y < -wallGrabSpeed){
            rb.velocity = new Vector2(rb.velocity.x,-wallGrabSpeed);
        }
        grabWall = true;
    }
    public void crouch(){
        if(!Crouch){
            Crouch = true;
            standardCol.enabled = false;
            crouchCol.enabled = true;
            spontaneityAnim = true;
            changeAnim((int)animIndex.crouchingDown);
            Invoke("toIdle",crouchingTIme);
        }else if(!crouchHeadCol.IsTouchingLayers(LayerMask.GetMask("ground")) && Crouch) {//웅크리기를 풀떄 머리위가 닿아있으면 못 일어남
            Crouch = false;
            standardCol.enabled = true;
            crouchCol.enabled = false;
            spontaneityAnim = true;
            changeAnim((int)animIndex.crouchingUp);
            Invoke("toIdle",crouchingTIme);
        }
    }
    
    private void state(){
        if(!isGround){runing = false;}
        else if(Crouch){
            runing = false;
        }else{
            runing = true;
        }
    }
    private void movement(){
        float a = 0;//가속력
        float Mspeed = 0;//최대속력
        float d = 0;//마찰

        //상태에따라 변화하는 가속도,최대속도,마찰을 위 변수에 저장
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
        if(!Dashing){
            //최대속도보다 현재속도가 크면 가속 중지 그리고 반대방향으로는 가속가능
            if(Mspeed > x && hori > 0){
                x += a * Time.deltaTime;
            }else if(-Mspeed < x && hori < 0){
                x += a * -Time.deltaTime;
            }
            //기본 마찰
            if(isGround && x != 0) {
                x -= x / Mathf.Abs(x) * d * Time.deltaTime;
            }
        }
        if(Mathf.Abs(x) < Time.deltaTime){
            x = 0;
        }
        rb.velocity = new Vector2(x,rb.velocity.y);//마찰 적용된 속도로 현재속도 바꿈
    }
    public void dash(bool dir){
        spontaneityAnim = true;
        Dashing = true;
        velBefDash = rb.velocity;//대쉬를 누른 순간 속도 저장
        rb.gravityScale = 0f;//중력 없애기
        changeAnim((int)animIndex.dash);

        if(!dir){

            rb.velocity = new Vector2(-dashPower.x,dashPower.y);//속도 대쉬힘으로 변환
        }else{

            rb.velocity = dashPower;
        }
        Invoke("dashEnd",dashTime);//일정 시간후 대쉬끝 함수 실행
    }
    private enum animIndex
    {
        idle,//0
        run,//1
        air,//2
        crouch,//3
        crouchMove,//4
        firstJump,//5
        secondJump,//6
        dash,//7
        land,//8
        crouchingDown,//9
        crouchingUp,//10
        wallJump,//11
        sliding,//12
        grabWall//13
    }
    private void Animation(){
        //착지 애니메이션 재생
        if(isGround && !lastground){
            spontaneityAnim = true;
            secondJumpAble = true;
            changeAnim((int)animIndex.land);
            
            Invoke("toIdle",landAnimTime);//일정시간뒤 착지 애니메이션 정지
        }
        
        //애니메이션용 변수
        bool run;
        if(hori != 0){
            run = true;
        }else{
            run = false;
        }

        if(!spontaneityAnim){
            if(!run && !Crouch && isGround){
                changeAnim((int)animIndex.idle);
            }else if(run && !Crouch && isGround){
                changeAnim((int)animIndex.run);
            }else if(Crouch && isGround){
                
                if(Mathf.Abs(rb.velocity.x) > slidingSpeed){
                    changeAnim((int)animIndex.sliding);
                }else if(run){
                    changeAnim((int)animIndex.crouchMove);
                }else{
                    changeAnim((int)animIndex.crouch);
                }
                
            }else if(grabWall){
                changeAnim((int)animIndex.grabWall);
            }else if(!isGround){
                changeAnim((int)animIndex.air);
            }
        } 

    }

    void changeAnim(int animIdx){
        if(currentAnim != animIdx){

            switch(animIdx){
                case 0:
                    anim.Play("idle_");
                    break;
                case 1:
                    anim.Play("run_");
                    break;
                case 2:
                    anim.Play("air_");
                    break;
                case 3:
                    anim.Play("crouch_");
                    break;
                case 4:
                    anim.Play("crouchMove_");
                    break;
                case 5:
                    anim.Play("firstJump_");
                    break;
                case 6:
                    anim.Play("secondJump_");
                    break;
                case 7:
                    anim.Play("dash_");
                    break;
                case 8:
                    anim.Play("land_");
                    break;
                case 9:
                    anim.Play("crouchingDown");
                    break;
                case 10:
                    anim.Play("crouchingUp");
                    break;
                case 11:
                    anim.Play("wallJump_");
                    break;
                case 12:
                    anim.Play("sliding_");
                    break;
                case 13:
                    anim.Play("wallGrab_");
                    break;
            }

            currentAnim = animIdx;
        }
    }

    void toIdle(){//착지 애니메이션 끝 낼때 실행되는 함수
        //changeAnim((int)animIndex.idle);//기본 애니메이션 재생
        spontaneityAnim = false;
    }
    void dashEnd(){//대쉬 끝날때 실행되는 함수
        rb.velocity = velBefDash;//대쉬 누른 순간의 속도 불러오기
        rb.gravityScale = 3f;//중력
        Dashing = false;//대쉬 끝
        //changeAnim((int)animIndex.idle);
        spontaneityAnim = false;
    }
    void lookTrue(){
        lookAble = true;
    }

    public void force(Vector2 force){
        Vector2 tmp = rb.velocity;
        //rb.AddForce(force);
        tmp += force;
        rb.velocity = tmp;
    }
    private void climb(){
/* 기어오르는 기능
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
        }*/
    }
}
