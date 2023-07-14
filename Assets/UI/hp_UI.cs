using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hp_UI : MonoBehaviour
{
    private Text HpText;
    private void Start()
    {
        HpText = GetComponent<Text>();
        // 글자색 변경
        HpText.color = Color.black;
        // 글자 크기 변경
        HpText.fontSize = 24;
        // 폰트 변경
        //Font customFont = Resources.Load<Font>("Fonts/CustomFont");
        //HpText.font = customFont;
    }
    public void changeText(int num)
    {
        HpText.text = "체력: " + num.ToString();
    }
}