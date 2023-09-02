using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*다이얼로그에 필요한 모든 오브젝트 함수를 참조받아서 사용
 XML을 제대로 사용하게 되는 경우 문자열 스플릿과 같은 데이터 처리 부분 수정 될 예정*/
public class GameManager : MonoBehaviour
{
    //대화문 오브젝트(항상 표시)
    [Tooltip("대화 오브젝트 전체")]
    public GameObject TalkSet;
    [Tooltip("공통 대화문")]
    public Text Talk_text;
    //대화문 오브젝트(대화일때만 표시)
    [Tooltip("플레이어 이미지 및 텍스트")]
    public GameObject Player_Obj;
    [Tooltip("NPC 이미지 및 텍스트")]
    public GameObject NPC_Obj;
    //그외
    [Tooltip("다이얼로그 UI 캔버스")]
    public GameObject Canvas;
    [Tooltip("플레이어 오브젝트")]
    public GameObject Player;
    [Tooltip("스캔된 오브젝트")]
    public GameObject ScanObject;

    //오브젝트 외 변수
    [Tooltip("Xml매니저 참조")]
    public XmlManager xmlmanager;
    int ID;
    private void Start()
    {
        Canvas.SetActive(false); //시작할땐 캔버스 꺼놓기
        TalkSet.SetActive(true); //기본이미지만 켜놓기
        Talk_text.enabled = true; //기본텍스트만 켜놓기
        ID = 0;
    }
    public void Action(GameObject scanObj)
    {
        ScanObject = scanObj;
        Obj_Dialog_Data objData = ScanObject.GetComponent<Obj_Dialog_Data>();
        xmlmanager.LoadXml(objData.XmlName); //XML로드하기
        bool Obj_Type = xmlmanager.Return_Object_Type();
        ObjectType(Obj_Type); //오브젝트 타입에 맞게 설정
        Canvas.SetActive(true); //캔버스 켜기
        Player.GetComponent<playerMoveMent>().enabled = false; //플레이어 움직임 끄기
        if (Obj_Type)
            Talk_NPC();
        else
            Talk_Object();

    }

    void Talk_NPC()
    {
        Dialog_inform inform = xmlmanager.Return_Dialog(ID);
        if (inform != null)
        {
            Text Teller;
            if (inform.Position.Equals("Player"))
            {
                Player_Obj.SetActive(true); //플레이어 오브젝트 활성화
                Player_Obj.transform.Find("PlayerName").gameObject.SetActive(true);//플레이어 이름 활성화
                NPC_Obj.transform.Find("NPCName").gameObject.SetActive(false); //NPC 이름 비활성화
                Teller = Player_Obj.GetComponentInChildren<Text>(); //플레이어 이름 적용
                Sprite Player_Model = Resources.Load<Sprite>(inform.Model); //모델명과 일치하는 스프라이트 로드
                Player_Obj.transform.Find("Player Standing").GetComponent<Image>().sprite = Player_Model; //스탠딩 이미지 적용
                NPC_Obj.transform.Find("NPC Standing").GetComponent<Image>().color = new Color(50, 50, 50, 255); //NPC 스탠딩 어둡게
                Player_Obj.transform.Find("Player Standing").GetComponent<Image>().color = new Color(255, 255, 255, 255); //플레이어 스탠딩 밝게
                NPC_Obj.transform.Find("NPC Standing").GetComponent<Image>().SetNativeSize();
                Player_Obj.transform.Find("Player Standing").GetComponent<Image>().SetNativeSize();
            }
            else
            {
                NPC_Obj.SetActive(true); //NPC 오브젝트 활성화
                NPC_Obj.transform.Find("NPCName").gameObject.SetActive(true);//NPC 이름 활성화
                Player_Obj.transform.Find("PlayerName").gameObject.SetActive(false); //플레이어 이름 비활성화
                Teller = NPC_Obj.GetComponentInChildren<Text>(); //NPC 이름 적용
                Sprite Player_Model = Resources.Load<Sprite>(inform.Model); //모델명과 일치하는 스프라이트 로드
                NPC_Obj.transform.Find("NPC Standing").GetComponent<Image>().sprite = Player_Model; //스탠딩 이미지 적용
                Player_Obj.transform.Find("Player Standing").GetComponent<Image>().color = new Color(50, 50, 50, 255); //플레이어 스탠딩 어둡게
                NPC_Obj.transform.Find("NPC Standing").GetComponent<Image>().color = new Color(255, 255, 255, 255); //NPC 스탠딩 밝게
                NPC_Obj.transform.Find("NPC Standing").GetComponent<Image>().SetNativeSize();
                Player_Obj.transform.Find("Player Standing").GetComponent<Image>().SetNativeSize();
            }
            Teller.text = inform.Teller;
            Talk_text.text = inform.Content;
        }
        else
        {
            ID = 0;
            Canvas.SetActive(false); //캔버스 켜기
            Player.GetComponent<playerMoveMent>().enabled = true; //플레이어 움직임 끄기
            return;
        }
        ID++;
    }

    void Talk_Object()
    {

    }

    void ObjectType(bool isVisible) //true - dialogue, description - false
    {
        Player_Obj.SetActive(isVisible);
        NPC_Obj.SetActive(isVisible);
    }
}
