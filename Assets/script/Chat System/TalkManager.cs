using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

/*대화문, 스탠딩 이미지 종류, 이름 데이터를 저장하고 id에 맞는 데이터를 반환하는 함수기능 존재
 이후 모든 데이터는 XML 혹은 TXT에 저장하고 파싱하여 불러오는 방식을 사용할 예정이기에 언제나 수정될 수 있음*/
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
        TalkData.Add(1000, new string[] {"...:0", "안녕?:1", "여기는 마을이야.:2" });
        //NameData.Add(1000, new string("앨리스"));
        //NameData.Add(2000, new string("헤럴드"));
        //NameData.Add(3000, new string("체셔캣"));
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
