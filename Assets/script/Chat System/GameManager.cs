using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*���̾�α׿� �ʿ��� ��� ������Ʈ �Լ��� �����޾Ƽ� ���
 XML�� ����� ����ϰ� �Ǵ� ��� ���ڿ� ���ø��� ���� ������ ó�� �κ� ���� �� ����*/
public class GameManager : MonoBehaviour
{
    //��ȭ�� ������Ʈ(�׻� ǥ��)
    [Tooltip("��ȭ ������Ʈ ��ü")]
    public GameObject TalkSet;
    [Tooltip("���� ��ȭ��")]
    public Text Talk_text;
    //��ȭ�� ������Ʈ(��ȭ�϶��� ǥ��)
    [Tooltip("�÷��̾� �̹��� �� �ؽ�Ʈ")]
    public GameObject Player_Obj;
    [Tooltip("NPC �̹��� �� �ؽ�Ʈ")]
    public GameObject NPC_Obj;
    //�׿�
    [Tooltip("�÷��̾� ������Ʈ")]
    public GameObject Player;
    [Tooltip("��ĵ�� ������Ʈ")]
    public GameObject ScanObject;

    //������Ʈ �� ����
    [Tooltip("Xml�Ŵ��� ����")]
    public XmlManager xmlmanager;
    int ID;
    private void Start()
    {
        TalkSet.SetActive(false); //�⺻�̹����� �ѳ���
        Talk_text.enabled = true; //�⺻�ؽ�Ʈ�� �ѳ���
        ID = 0;
    }
    public void Action(GameObject scanObj)
    {
        ScanObject = scanObj;
        Obj_Dialog_Data objData = ScanObject.GetComponent<Obj_Dialog_Data>();
        xmlmanager.LoadXml(objData.XmlName); //XML�ε��ϱ�
        bool Obj_Type = xmlmanager.Return_Object_Type();
        ObjectType(Obj_Type); //������Ʈ Ÿ�Կ� �°� ����
        TalkSet.SetActive(true); //��ȭâ �ѱ�
        Player.GetComponent<playerMoveMent>().enabled = false; //�÷��̾� ������ ����
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
                Player_Obj.SetActive(true); //�÷��̾� ������Ʈ Ȱ��ȭ
                Player_Obj.transform.Find("PlayerName").gameObject.SetActive(true);//�÷��̾� �̸� Ȱ��ȭ
                NPC_Obj.transform.Find("NPCName").gameObject.SetActive(false); //NPC �̸� ��Ȱ��ȭ
                Teller = Player_Obj.GetComponentInChildren<Text>(); //�÷��̾� �̸� ����
                Sprite Player_Model = Resources.Load<Sprite>(inform.Model); //�𵨸�� ��ġ�ϴ� ��������Ʈ �ε�
                Player_Obj.transform.Find("Player Standing").GetComponent<Image>().sprite = Player_Model; //���ĵ� �̹��� ����
                NPC_Obj.transform.Find("NPC Standing").GetComponent<Image>().color = new Color(50, 50, 50, 255); //NPC ���ĵ� ��Ӱ�
                Player_Obj.transform.Find("Player Standing").GetComponent<Image>().color = new Color(255, 255, 255, 255); //�÷��̾� ���ĵ� ���
                NPC_Obj.transform.Find("NPC Standing").GetComponent<Image>().SetNativeSize();
                Player_Obj.transform.Find("Player Standing").GetComponent<Image>().SetNativeSize();
            }
            else
            {
                NPC_Obj.SetActive(true); //NPC ������Ʈ Ȱ��ȭ
                NPC_Obj.transform.Find("NPCName").gameObject.SetActive(true);//NPC �̸� Ȱ��ȭ
                Player_Obj.transform.Find("PlayerName").gameObject.SetActive(false); //�÷��̾� �̸� ��Ȱ��ȭ
                Teller = NPC_Obj.GetComponentInChildren<Text>(); //NPC �̸� ����
                Sprite Player_Model = Resources.Load<Sprite>(inform.Model); //�𵨸�� ��ġ�ϴ� ��������Ʈ �ε�
                NPC_Obj.transform.Find("NPC Standing").GetComponent<Image>().sprite = Player_Model; //���ĵ� �̹��� ����
                Player_Obj.transform.Find("Player Standing").GetComponent<Image>().color = new Color(50, 50, 50, 255); //�÷��̾� ���ĵ� ��Ӱ�
                NPC_Obj.transform.Find("NPC Standing").GetComponent<Image>().color = new Color(255, 255, 255, 255); //NPC ���ĵ� ���
                NPC_Obj.transform.Find("NPC Standing").GetComponent<Image>().SetNativeSize();
                Player_Obj.transform.Find("Player Standing").GetComponent<Image>().SetNativeSize();
            }
            Teller.text = inform.Teller;
            Talk_text.text = inform.Content;
        }
        else
        {
            ID = 0;
            TalkSet.SetActive(false); //��ȭâ ����
            Player.GetComponent<playerMoveMent>().enabled = true; //�÷��̾� ������ ����
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
