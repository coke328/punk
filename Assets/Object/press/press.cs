using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class press : MonoBehaviour
{
    public UnityEvent press_zone_event;
    public float press_length;
    public float press_speed_up;
    public float press_speed_down;
    private float press_speed;
    private BoxCollider2D col;
    private bool flag = false;
    private Vector3 startpos;
    private Vector3 endpos;
    private Vector3 chasingpos;
    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
        col.enabled = false;
        startpos = transform.position;
        endpos = new Vector3(transform.position.x, transform.position.y - press_length, 0);
        chasingpos = endpos;
        press_speed = press_speed_down;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            Debug.Log("프레스가 플레이어 타격");
        }
    }
    private void FixedUpdate()
    {
        if (flag)
            transform.position = Vector3.MoveTowards(transform.position, chasingpos, Time.deltaTime * press_speed);
        check_pos();
    }
    void check_pos()
    {
        if (flag && transform.position.Equals(endpos))//바닥을찍으면 다시 올라오게
        {
            chasingpos = startpos;
            press_speed = press_speed_up;
        }
        if (flag && transform.position.Equals(startpos))//올라왔으면 프레스가 멈추게
        {
            flag = false;
            chasingpos = endpos;
            press_speed = press_speed_down;
            press_zone_event.Invoke();
        }
    }
    public void press_on()
    {
        flag = true;
        col.enabled = true;
        Debug.Log("프레스 작동");
    }
    public void press_off()
    {
        flag = false;
        col.enabled = false;
        Debug.Log("프레스 정지");
    }
}


/*
public class press : MonoBehaviour
{
    SpriteRenderer sr;
    public LayerMask press_groundLayer; // 바닥 레이어
    public LayerMask press_PlayerLayer; // 플레이어 레이어
    private float press_raydistance; // 레이의 길이
    private int Max_distance = 8; // 바닥을 감지할 레이의 길이
    private Vector2 bottomLeftPoint;
    private Vector2 bottomRightPoint;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        // 스프라이트의 왼쪽 아래 꼭짓점과 오른쪽 아래 꼭짓점의 좌표
        bottomLeftPoint = new Vector2(sr.bounds.min.x, sr.bounds.min.y);
        bottomRightPoint = new Vector2(sr.bounds.max.x, sr.bounds.min.y);
        // 스프라이트와 지면과의 거리에 맞게 플레이어를 감지하는 레이의 길이를 설정
        check_ground_distance();
    }
    private void Update()
    {
        // 왼쪽 아래 꼭짓점에서 바닥 방향으로 레이를 쏨
        RaycastHit2D leftRaycastHit = Physics2D.Raycast(bottomLeftPoint, Vector2.down, press_raydistance, press_PlayerLayer);
        Debug.DrawRay(bottomLeftPoint, Vector2.down * press_raydistance, new Color(0, 1, 0, 1));
        // 오른쪽 아래 꼭짓점에서 바닥 방향으로 레이를 쏨
        RaycastHit2D rightRaycastHit = Physics2D.Raycast(bottomRightPoint, Vector2.down, press_raydistance, press_groundLayer);
        Debug.DrawRay(bottomRightPoint, Vector2.down * press_raydistance, new Color(0, 1, 0, 1));

        // 레이를 쏜 결과를 확인하여 바닥과 충돌했는지 체크
        if ((leftRaycastHit.collider != null) || (rightRaycastHit.collider != null))
        {
            Debug.Log("프레스 감지범위에 플레이어가 충돌!");
        }
    }
    void check_ground_distance()
    {
        float left_distance;
        float right_distance;
        // 왼쪽 아래 꼭짓점에서 바닥 방향으로 레이를 쏨
        RaycastHit2D leftRaycastHit = Physics2D.Raycast(bottomLeftPoint, Vector2.down, Max_distance, press_groundLayer);
        // 레이를 쏜 결과를 확인하여 바닥과 충돌했는지 체크
        if (leftRaycastHit.collider != null)
        {
            left_distance = Vector2.Distance(bottomLeftPoint, leftRaycastHit.point);
            Debug.Log("왼쪽아래에서 바닥과 충돌! 거리: " + left_distance);
        }
        else
        {
            left_distance = Max_distance;
            Debug.Log("왼쪽아래에서 바닥과 충돌하지 않음 최대 거리: " + left_distance);
        }
        // 오른쪽 아래 꼭짓점에서 바닥 방향으로 레이를 쏨
        RaycastHit2D rightRaycastHit = Physics2D.Raycast(bottomRightPoint, Vector2.down, Max_distance, press_groundLayer);
        // 레이를 쏜 결과를 확인하여 바닥과 충돌했는지 체크
        if (rightRaycastHit.collider != null)
        {
            right_distance = Vector2.Distance(bottomRightPoint, rightRaycastHit.point);
            Debug.Log("왼쪽아래에서 바닥과 충돌! 거리: " + right_distance);
        }
        else
        {
            right_distance = Max_distance;
            Debug.Log("왼쪽아래에서 바닥과 충돌하지 않음 최대 거리: " + right_distance);
        }
        //프레스의 감지 길이는 2개의 레이 중 짧은 것으로 선택
        press_raydistance = (left_distance < right_distance) ? left_distance : right_distance;
    }
}

 */