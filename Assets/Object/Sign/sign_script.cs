using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class sign_script : MonoBehaviour
{
    public UnityEvent sign_image_on;
    public UnityEvent sign_text_on;
    public UnityEvent sign_image_off;
    public UnityEvent sign_text_off;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Ʈ�����浹�� ������Ʈ�� �̸��� ��
        if (collision.gameObject.name == "player")
        {
            Debug.Log("�浹��");
            sign_image_on.Invoke();
            sign_text_on.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Ʈ�����浹�� ������Ʈ�� �̸��� ��
        if (collision.gameObject.name == "player")
        {
            Debug.Log("�浹���");
            sign_image_off.Invoke();
            sign_text_off.Invoke();
        }
    }
}