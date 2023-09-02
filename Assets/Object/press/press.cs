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
            Debug.Log("�������� �÷��̾� Ÿ��");
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
        if (flag && transform.position.Equals(endpos))//�ٴ��������� �ٽ� �ö����
        {
            chasingpos = startpos;
            press_speed = press_speed_up;
        }
        if (flag && transform.position.Equals(startpos))//�ö������ �������� ���߰�
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
        Debug.Log("������ �۵�");
    }
    public void press_off()
    {
        flag = false;
        col.enabled = false;
        Debug.Log("������ ����");
    }
}


/*
public class press : MonoBehaviour
{
    SpriteRenderer sr;
    public LayerMask press_groundLayer; // �ٴ� ���̾�
    public LayerMask press_PlayerLayer; // �÷��̾� ���̾�
    private float press_raydistance; // ������ ����
    private int Max_distance = 8; // �ٴ��� ������ ������ ����
    private Vector2 bottomLeftPoint;
    private Vector2 bottomRightPoint;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        // ��������Ʈ�� ���� �Ʒ� �������� ������ �Ʒ� �������� ��ǥ
        bottomLeftPoint = new Vector2(sr.bounds.min.x, sr.bounds.min.y);
        bottomRightPoint = new Vector2(sr.bounds.max.x, sr.bounds.min.y);
        // ��������Ʈ�� ������� �Ÿ��� �°� �÷��̾ �����ϴ� ������ ���̸� ����
        check_ground_distance();
    }
    private void Update()
    {
        // ���� �Ʒ� ���������� �ٴ� �������� ���̸� ��
        RaycastHit2D leftRaycastHit = Physics2D.Raycast(bottomLeftPoint, Vector2.down, press_raydistance, press_PlayerLayer);
        Debug.DrawRay(bottomLeftPoint, Vector2.down * press_raydistance, new Color(0, 1, 0, 1));
        // ������ �Ʒ� ���������� �ٴ� �������� ���̸� ��
        RaycastHit2D rightRaycastHit = Physics2D.Raycast(bottomRightPoint, Vector2.down, press_raydistance, press_groundLayer);
        Debug.DrawRay(bottomRightPoint, Vector2.down * press_raydistance, new Color(0, 1, 0, 1));

        // ���̸� �� ����� Ȯ���Ͽ� �ٴڰ� �浹�ߴ��� üũ
        if ((leftRaycastHit.collider != null) || (rightRaycastHit.collider != null))
        {
            Debug.Log("������ ���������� �÷��̾ �浹!");
        }
    }
    void check_ground_distance()
    {
        float left_distance;
        float right_distance;
        // ���� �Ʒ� ���������� �ٴ� �������� ���̸� ��
        RaycastHit2D leftRaycastHit = Physics2D.Raycast(bottomLeftPoint, Vector2.down, Max_distance, press_groundLayer);
        // ���̸� �� ����� Ȯ���Ͽ� �ٴڰ� �浹�ߴ��� üũ
        if (leftRaycastHit.collider != null)
        {
            left_distance = Vector2.Distance(bottomLeftPoint, leftRaycastHit.point);
            Debug.Log("���ʾƷ����� �ٴڰ� �浹! �Ÿ�: " + left_distance);
        }
        else
        {
            left_distance = Max_distance;
            Debug.Log("���ʾƷ����� �ٴڰ� �浹���� ���� �ִ� �Ÿ�: " + left_distance);
        }
        // ������ �Ʒ� ���������� �ٴ� �������� ���̸� ��
        RaycastHit2D rightRaycastHit = Physics2D.Raycast(bottomRightPoint, Vector2.down, Max_distance, press_groundLayer);
        // ���̸� �� ����� Ȯ���Ͽ� �ٴڰ� �浹�ߴ��� üũ
        if (rightRaycastHit.collider != null)
        {
            right_distance = Vector2.Distance(bottomRightPoint, rightRaycastHit.point);
            Debug.Log("���ʾƷ����� �ٴڰ� �浹! �Ÿ�: " + right_distance);
        }
        else
        {
            right_distance = Max_distance;
            Debug.Log("���ʾƷ����� �ٴڰ� �浹���� ���� �ִ� �Ÿ�: " + right_distance);
        }
        //�������� ���� ���̴� 2���� ���� �� ª�� ������ ����
        press_raydistance = (left_distance < right_distance) ? left_distance : right_distance;
    }
}

 */