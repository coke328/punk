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
        // ���ڻ� ����
        HpText.color = Color.black;
        // ���� ũ�� ����
        HpText.fontSize = 24;
        // ��Ʈ ����
        //Font customFont = Resources.Load<Font>("Fonts/CustomFont");
        //HpText.font = customFont;
    }
    public void changeText(int num)
    {
        HpText.text = "ü��: " + num.ToString();
    }
}