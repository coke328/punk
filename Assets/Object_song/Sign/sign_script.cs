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
        //트리거충돌된 오브젝트의 이름을 비교
        if (collision.gameObject.name == "player")
        {
            Debug.Log("충돌함");
            talkballon.talkballon_Enable();
            tutorialText.meshenable();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //트리거충돌된 오브젝트의 이름을 비교
        if (collision.gameObject.name == "player")
        {
            Debug.Log("충돌벗어남");
            talkballon.talkballon_Disable();
            tutorialText.meshdisable();
        }
    }
}