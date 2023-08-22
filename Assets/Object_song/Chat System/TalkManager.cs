using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

/*��ȭ��, ���ĵ� �̹��� ����, �̸� �����͸� �����ϰ� id�� �´� �����͸� ��ȯ�ϴ� �Լ���� ����
 ���� ��� �����ʹ� XML Ȥ�� TXT�� �����ϰ� �Ľ��Ͽ� �ҷ����� ����� ����� �����̱⿡ ������ ������ �� ����*/
public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> TalkData;
    //Dictionary<int, string> NameData;
    Dictionary<int, Sprite> StandData;
    public Sprite[] StandArr;
    public TextAsset text;
    private void Awake()
    {
        TalkData = new Dictionary<int, string[]>();
        StandData = new Dictionary<int, Sprite>();
        GenerateData();
    }
    void GenerateData()
    {
        TalkData.Add(1000, new string[] {"...:0", "�ȳ�?:1", "����� �����̾�.:2" });
        //NameData.Add(1000, new string("�ٸ���"));
        //NameData.Add(2000, new string("�췲��"));
        //NameData.Add(3000, new string("ü��Ĺ"));
        StandData.Add(1000 + 0, StandArr[0]);
        StandData.Add(1000 + 1, StandArr[1]);
        StandData.Add(1000 + 2, StandArr[2]);
        StandData.Add(1000 + 3, StandArr[3]);

    }
    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == TalkData[id].Length)
            return null;
        else
            return TalkData[id][talkIndex];
    }
    public Sprite GetSprite(int id, int StandIndex)
    {
        return StandData[id + StandIndex];
    }
    //public string GetName(int id)
    //{
    //   return NameData[id];
    //}
}
