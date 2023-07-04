using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMoveMent : MonoBehaviour
{
    private float hori;//키보드 a,d 입력
    public float acc = 4f;//땅에서 가속도
    public float MaxSpeed = 4f;//땅에서 최대 속도
    public bool runing = true; //웅크리기,공중에 뜸 제외 상태
    public float drag = 0.99f;//땅에서의 마찰
    public float airMaxSpeed = 5f;//공중에서 최고 속도
    public float airAcc = 2f;//공중에서 가속도
    public float airDrag = 2f;//공중에서의 공기 저항
    public float jumpForce;//점프 힘
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
    public Vector2 wallJumpPower;//벽점프 힘
    private bool Crouch = false;//웅크리기 상태
    public float crouchAcc = 40;//웅크리기 상태의 가속도
    public float crouchMaxSpeed = 3f;//웅크리기 상태의 최고 속도
    public float crouchDrag = 7;//웅크리기 상태의 마찰력
    private BoxCollider2D crouchCol;//웅크렸을때의 캐릭터 콜라이더
    private BoxCollider2D standardCol;//기본상태의 캐릭터 콜라이더
    private BoxCollider2D crouchHeadCol;//웅크렸을떄 머리위에 접촉 감지
    public bool able_dash;//대쉬가 가능한지
    private bool Dashing = false;//대쉬를 하고있는지
    public Vector2 dashPower;//대쉬 힘
    public float dashTime = 0.3f;//대쉬 시간
    public float dashCoolTime = 3f;//대쉬 쿨타임
    private float dashT = 0f;//대쉬하고나서 지난시간
    private Vector2 velBefDash;//대쉬이전의 속도 저장하는 곳

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
        bool isGround = bottomcol.IsTouchingLayers(LayerMask.GetMask("ground"));//지면과 접촉여부를 판단하는 변수

        hori = Input.GetAxisRaw("Horizontal");//좌우 입력

        //캐릭터 좌우 돌아보기
        if(hori > 0){
            spRend.flipX = false;
        }else if(hori < 0){
            spRend.flipX = true;
        }

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
        
        if(Input.GetKeyDown("space") && !Crouch && !isGround){
            if(rightcol.IsTouchingLayers(LayerMask.GetMask("ground"))){
                rb.AddForce(new Vector2(-wallJumpPower.x,wallJumpPower.y));
            }else if(leftcol.IsTouchingLayers(LayerMask.GetMask("ground"))){
                rb.AddForce(wallJumpPower);
            }
        }

        //웅크리기 기능
        if(Input.GetKeyDown(KeyCode.LeftControl)){
            if(!Crouch){
                Crouch = true;
                standardCol.enabled = false;
                crouchCol.enabled = true;
            }else if(!crouchHeadCol.IsTouchingLayers(LayerMask.GetMask("ground")) && Crouch) {//웅크리기를 풀떄 머리위가 닿아있으면 못 일어남
                Crouch = false;
                standardCol.enabled = true;
                crouchCol.enabled = false;
            }
        }
        
        //기본상태 변화
        if(!isGround){runing = false;}
        else if(Crouch){
            runing = false;
        }else{
            runing = true;
        }

        
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
                x += a * 0.01f;
            }else if(-Mspeed < x && hori < 0){
                x += a * -0.01f;
            }
            //기본 마찰
            if(isGround && x != 0) {
                x -= x / Mathf.Abs(x) * d * 0.01f;
            }
        }
        rb.velocity = new Vector2(x,rb.velocity.y);//마찰 적용된 속도로 현재속도 바꿈
        
        //점프 기능
        if(isGround && Input.GetKeyDown("space") && !Dashing){
            rb.AddForce(new Vector2(0,1)*jumpForce);
            anim.Play("jump");//점프 애니메이션 재생
            
            Invoke("jumpFalse",0.08f);//일정시간뒤 점프애니메이션 정지
        }
        
        //착지 애니메이션 재생
        if(isGround != lastground){
            anim.Play("land");

            Invoke("landFalse",0.08f);//일정시간뒤 착지 애니메이션 정지
        }

        //애니메이션용 변수
        bool run;
        if(hori != 0){
            run = true;
        }else{
            run = false;
        }

        //대쉬 기능
        if(able_dash && dashT > dashCoolTime && Input.GetKeyDown(KeyCode.LeftShift)){
            if(Input.GetKey(KeyCode.A)){
                Dashing = true;
                velBefDash = rb.velocity;//대쉬를 누른 순간 속도 저장
                rb.gravityScale = 0f;//중력 없애기
                rb.velocity = new Vector2(-dashPower.x,dashPower.y);//속도 대쉬힘으로 변환
                Invoke("dashEnd",dashTime);//일정 시간후 대쉬끝 함수 실행
            }else if(Input.GetKey(KeyCode.D)){
                Dashing = true;
                velBefDash = rb.velocity;//대쉬를 누른 순간 속도 저장
                rb.gravityScale = 0f;
                rb.velocity = dashPower;
                Invoke("dashEnd",dashTime);
            }
        }

        //애니메이션용 변수 적용
        anim.SetBool("runing",run);
        anim.SetBool("onground", isGround);
        anim.SetBool("crouch",Crouch);

        lastground = isGround;
        dashT += Time.deltaTime;
        Debug.Log(rb.gravityScale);
    }

    void jumpFalse(){//점프 애니메이션 끝 낼때 실행되는 함수
        anim.Play("idle");//기본 애니메이션 재생
    }
    void landFalse(){//착지 애니메이션 끝 낼때 실행되는 함수
        anim.Play("idle");//기본 애니메이션 재생
    }
    void dashEnd(){//대쉬 끝날때 실행되는 함수
        rb.velocity = velBefDash;//대쉬 누른 순간의 속도 불러오기
        rb.gravityScale = 3f;//중력
        Dashing = false;//대쉬 끝
    }
}
