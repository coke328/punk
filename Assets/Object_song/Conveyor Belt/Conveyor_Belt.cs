using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Conveyor_Belt : MonoBehaviour
{
    public float belt_speed;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Vector2 position = rb.position;
        //rb.position += Vector2.left * belt_speed * Time.fixedDeltaTime;
        rb.MovePosition(new Vector2(transform.position.x - (Time.fixedDeltaTime * belt_speed), transform.position.y));
        //rb.MovePosition(new Vector2(18, position.y));
        transform.position = Vector2.MoveTowards(transform.position, new Vector3(15.01f,0.3f,0f), Time.fixedDeltaTime * belt_speed);
    }
    //플레이어가 플랫폼에 탑승하면 같이 움직이게 위치를 자식으로 옮기기
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
