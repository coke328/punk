using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*F입력 시 플레이어 콜라이더와 충돌된 NPC레이어 오브젝트가 있으면 해당 오브젝트를 게임매니저에 전달*/
public class Scan_Object : MonoBehaviour
{
    UnityEvent chat;
    GameObject ScanObject;
    public GameManager manager;
    public bool isNPC; //NPC Trigger 충돌여부
    private void Start()
    {
        isNPC = false;
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F) && isNPC) //트리거에 대화가능 오브젝트 진입시 F버튼으로 대화 진입
        {
            manager.Action(ScanObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) //NPC 오브젝트와 충돌 시 플래그 변경 및 오브젝트 저장
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("NPC"))
        {
            isNPC = true;
            ScanObject = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) //충돌된 오브젝트 탈출 시 플래그 변경 및 오브젝트 삭제
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("NPC"))
        {
            isNPC = false;
            ScanObject = null;
        }
    }
}
