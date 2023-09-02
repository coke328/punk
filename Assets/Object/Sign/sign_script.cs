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
        //트리거충돌된 오브젝트의 이름을 비교
        if (collision.gameObject.name == "player")
        {
            Debug.Log("충돌함");
            sign_image_on.Invoke();
            sign_text_on.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //트리거충돌된 오브젝트의 이름을 비교
        if (collision.gameObject.name == "player")
        {
            Debug.Log("충돌벗어남");
            sign_image_off.Invoke();
            sign_text_off.Invoke();
        }
    }
}