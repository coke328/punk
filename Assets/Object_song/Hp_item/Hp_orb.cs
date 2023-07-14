using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Hp_orb : MonoBehaviour
{
    SpriteRenderer spriterenderer;
    Transform transformobj;
    [SerializeField]
    private hp Hp;//플레이어에게서 가져오기
    [SerializeField]
    private hp_UI Hp_UI;//플레이어에게서 가져오기
    //좌표,방향,크기
    Vector2 vec;
    Vector2 direction;
    Vector3 scale;
    RaycastHit2D rayHit;
    public int healnum;//회복량
    public float scalenum;//아이템 크기
    private void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
        transformobj = GetComponent<Transform>();
        //좌표및 길이 설정
        vec = new Vector2(transform.position.x, spriterenderer.bounds.min.y);
        direction = Vector2.up;
        scale = transformobj.localScale;
    }
    private void Update()
    {
        scale = new Vector3(scalenum, scalenum, 1); 
        //스프라이트 정중앙에 레이쏘기
        rayHit = Physics2D.Raycast(vec, direction, scale.x, LayerMask.GetMask("Player"));
        Debug.DrawRay(vec, direction * scalenum, new Color(0, 1, 0, 1));
        //플레이어가 닿았고 풀피가 아니라면
        if (rayHit.collider != null && !Hp.is_max())
        {
            Hp.heal(healnum);
            Hp_UI.changeText(Hp.return_hp());
            Destroy(this.gameObject);
        }
    }
}