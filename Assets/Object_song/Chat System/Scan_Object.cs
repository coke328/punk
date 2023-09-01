using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*F�Է� �� �÷��̾� �ݶ��̴��� �浹�� NPC���̾� ������Ʈ�� ������ �ش� ������Ʈ�� ���ӸŴ����� ����*/
public class Scan_Object : MonoBehaviour
{
    UnityEvent chat;
    GameObject ScanObject;
    public GameManager manager;
    public bool isNPC; //NPC Trigger �浹����
    private void Start()
    {
        isNPC = false;
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F) && isNPC) //Ʈ���ſ� ��ȭ���� ������Ʈ ���Խ� F��ư���� ��ȭ ����
        {
            manager.Action(ScanObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) //NPC ������Ʈ�� �浹 �� �÷��� ���� �� ������Ʈ ����
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("NPC"))
        {
            isNPC = true;
            ScanObject = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) //�浹�� ������Ʈ Ż�� �� �÷��� ���� �� ������Ʈ ����
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("NPC"))
        {
            isNPC = false;
            ScanObject = null;
        }
    }
}
