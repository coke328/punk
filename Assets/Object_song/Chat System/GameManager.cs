using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*���̾�α׿� �ʿ��� ��� ������Ʈ �Լ��� �����޾Ƽ� ���
 XML�� ����� ����ϰ� �Ǵ� ��� ���ڿ� ���ø��� ���� ������ ó�� �κ� ���� �� ����*/
public class GameManager : MonoBehaviour
{
    [Tooltip("��ũ�Ŵ��� ����")]
    public TalkManager talkmanager;
    [Tooltip("�÷��̾� ���ĵ� �̹���")]
    public Image PStandImg;
    [Tooltip("NPC ���ĵ� �̹���")]
    public Image StandImg;
    [Tooltip("�÷��̾� �̸� �̹���")]
    public Image PNameImg;
    [Tooltip("NPC �̸� �̹���")]
    public Image NPCNameImg;
    [Tooltip("�÷��̾� �̸�")]
    public Text PNameText;
    [Tooltip("NPC �̸�")]
    public Text NPCNameText;
    [Tooltip("��ȭ��")]
    public Text TalkText;
    [Tooltip("��ȭ�� �̹���")]
    public GameObject TalkPanel;
    [Tooltip("�÷��̾� ������Ʈ")]
    public GameObject Player;
    [Tooltip("��ĵ�� ������Ʈ")]
    public GameObject ScanObject;
    [Tooltip("��ȭ ���� ���� ����")]
    public bool isAction;
    [Tooltip("��ȣ ��ȭ ����")]
    public bool isTalk;
    [Tooltip("��ȭ�� id")]
    public int talkIndex;
    private void Start()
    {
        TalkPanel.SetActive(false);
    }
    public void Action(GameObject scanObj)
    {
        ScanObject = scanObj;
        Obj_Dialog_Data objData = ScanObject.GetComponent<Obj_Dialog_Data>();
        Talk(objData.Id, objData.isTalk);
        TalkPanel.SetActive(isAction);
        Player.GetComponent<playerMoveMent>().enabled = !isAction;
    }

    void Talk(int id, bool isTalk)
    {
        string talkData = talkmanager.GetTalk(id, talkIndex);
        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            return;
        }
        ImageVisibility(isTalk);
        if (isTalk)
        {
            TalkText.text = talkData.Split(':')[0];
            StandImg.sprite = talkmanager.GetSprite(id, int.Parse(talkData.Split(':')[1]));
            StandImg.SetNativeSize();
        }
        else
        {
            TalkText.text = talkData;
        }

        isAction = true;
        talkIndex++;
    }

    void ImageVisibility(bool isVisible) //true - dialogue, description - false
    {
        int num;
        if (isVisible)
            num = 1;
        else
            num = 0;
        PStandImg.color = new Color(1, 1, 1, num);
        StandImg.color = new Color(1, 1, 1, num);
        PNameImg.color = new Color(1, 1, 1, num);
        NPCNameImg.color = new Color(1, 1, 1, num);
        PNameText.enabled = isVisible;
        NPCNameText.enabled = isVisible;

    }
}
