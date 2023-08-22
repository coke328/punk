using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*다이얼로그에 필요한 모든 오브젝트 함수를 참조받아서 사용
 XML을 제대로 사용하게 되는 경우 문자열 스플릿과 같은 데이터 처리 부분 수정 될 예정*/
public class GameManager : MonoBehaviour
{
    [Tooltip("토크매니저 참조")]
    public TalkManager talkmanager;
    [Tooltip("플레이어 스탠딩 이미지")]
    public Image PStandImg;
    [Tooltip("NPC 스탠딩 이미지")]
    public Image StandImg;
    [Tooltip("플레이어 이름 이미지")]
    public Image PNameImg;
    [Tooltip("NPC 이름 이미지")]
    public Image NPCNameImg;
    [Tooltip("플레이어 이름")]
    public Text PNameText;
    [Tooltip("NPC 이름")]
    public Text NPCNameText;
    [Tooltip("대화문")]
    public Text TalkText;
    [Tooltip("대화문 이미지")]
    public GameObject TalkPanel;
    [Tooltip("플레이어 오브젝트")]
    public GameObject Player;
    [Tooltip("스캔된 오브젝트")]
    public GameObject ScanObject;
    [Tooltip("대화 상태 진입 여부")]
    public bool isAction;
    [Tooltip("상호 대화 여부")]
    public bool isTalk;
    [Tooltip("대화문 id")]
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
