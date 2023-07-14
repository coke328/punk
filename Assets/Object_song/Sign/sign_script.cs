using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sign_script : MonoBehaviour
{
    [SerializeField]
    private talkballon_script talkballon;
    [SerializeField]
    private tutorialText tutorialText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Ʈ�����浹�� ������Ʈ�� �̸��� ��
        if (collision.gameObject.name == "player")
        {
            Debug.Log("�浹��");
            talkballon.talkballon_Enable();
            tutorialText.meshenable();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Ʈ�����浹�� ������Ʈ�� �̸��� ��
        if (collision.gameObject.name == "player")
        {
            Debug.Log("�浹���");
            talkballon.talkballon_Disable();
            tutorialText.meshdisable();
        }
    }
}