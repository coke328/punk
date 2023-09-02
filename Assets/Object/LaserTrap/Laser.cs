using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

/* ==���ܼ���==
 * Y Flip���θ� Ȯ���ϰ� ���� ������ ����
 * ���̿� Player���̾ ������ �α����
 * ������ ���̴� Public���� ����
 * 
 * �÷��̾� ���� �浹 �� �ǰ����� ������Ʈ ����
 */
public class Laser : MonoBehaviour
{
    SpriteRenderer spriterenderer;
    BoxCollider2D boxCollider;
    RaycastHit2D rayHit;
    //Ray&beam Length control
    public float Max_Ray_Length;
    //Laser Wait Time control
    public float Wait_Time;
    //Ray Hit Switch
    bool HitCheck = true;
    //ray position and direction
    Vector2 vec;
    Vector2 direction;
    //Max Possible Length
    float distanceToHit;

    //���η����� �ڵ�
    [SerializeField]
    private beam_line visualizer_line;
    void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        if (spriterenderer.flipY)
        {
            //Y is not Flipped
            vec = new Vector2(transform.position.x, spriterenderer.bounds.min.y);
            direction = Vector2.down;
        }
        else
        {
            //Y is Flipped
            vec = new Vector2(transform.position.x, spriterenderer.bounds.max.y);
            direction = Vector2.up;
        }
        
        //rayHit = Physics2D.Raycast(vec, direction, Max_Ray_Length, LayerMask.GetMask(""));
        rayHit = Physics2D.Raycast(vec, direction, Max_Ray_Length, LayerMask.GetMask("Platform"));
        //Set Beam_Length (���� ����) 
        if (rayHit.collider != null)
            distanceToHit = rayHit.distance;
        else
            distanceToHit = Max_Ray_Length;
    }
    void Update()
    {
        //rayHit = Physics2D.Raycast(vec, direction, Max_Ray_Length, LayerMask.GetMask(""));
        rayHit = Physics2D.Raycast(vec, direction, distanceToHit, LayerMask.GetMask("player"));
        //DrawRay
        Debug.DrawRay(vec, direction * distanceToHit, new Color(0, 1, 0, 1));
        //Ray is Hit
        if (rayHit.collider != null && HitCheck)
        {
            HitCheck = false;
            Invoke("Shot_Laser", Wait_Time);
            Invoke("HitCheckOn", Wait_Time + 1.5f);
        }
    }
    void Shot_Laser()
    {
        // Y�� ��ǥ��+�⺻����ġ
        Vector2 beamvec = new Vector2(transform.position.x, transform.position.y + 0.5f);
        Vector2 beamvec_end = new Vector2(transform.position.x, transform.position.y + 0.5f + distanceToHit);
        //���η����� �ڵ�
        visualizer_line.Play(beamvec, beamvec_end);
    }
    void HitCheckOn()
    {
        HitCheck = true;
        visualizer_line.Stop();
    }
}