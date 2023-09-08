using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class respawn : MonoBehaviour
{
    public PlayerDataScriptableObject currentData;
    public PlayerDataScriptableObject lastData;
    private hp hpScript;
    void Start()// 씬이 로딩되면 호출됨(다시로딩 돼도 호출됨)
    {

        hpScript = GetComponent<hp>();

        if(currentData.deadCnt != lastData.deadCnt){
            //죽었을때
            hpScript.Hp = currentData.health;
            lastData.deadCnt = currentData.deadCnt;
        }else{
            //새 스테이지에 왔을때
            lastData.coin = currentData.coin;
            lastData.health = currentData.health;
            //lastData.keys = currentData.keys.ToList<key>();//얕은 복사
            lastData.keys = currentData.keys.ConvertAll(k => new key(k.keyName,k.keyCount));//깊은 복사
            lastData.setSceneName(SceneManager.GetActiveScene().name);
        }
    }

        //플레이어 데이터 관련
    public void loadLastData(){//리스폰 할때 Hp스크립트 dead이벤트랑 연결 ,죽으면 씬 로딩 다시
        currentData.coin = lastData.coin;//스테이지 시작할때의 데이터를 현재 데이터에 저장
        currentData.health = lastData.health;
        //currentData.keys = lastData.keys.ToList<key>();//얕은 복사
        currentData.keys = lastData.keys.ConvertAll(k => new key(k.keyName,k.keyCount));//깊은 복사
        currentData.deadCnt++;//죽은 횟수 추가
        SceneManager.LoadScene(lastData.sceneName);
    }
    public void getKey(string keyName,int cnt){//열쇠를 얻었을때
        currentData.getKey(keyName,cnt);
    }
    public void addCoin(int cnt){//코인 얻음
        currentData.coin += cnt;
    }
    public void setHealth(){//체력지정 Hp스크립트랑 연결(체력을 잃거나 얻었을떄 동기화)
        currentData.health = hpScript.Hp;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            hpScript.damage(5);//데미지 입히기
        }
        if(Input.GetKeyDown(KeyCode.K)){//현재가진 열쇠 콘솔로 띄우기
            Debug.Log("current");
            currentData.keyPrint();
            Debug.Log("last");
            lastData.keyPrint();
        }
        if(Input.GetKeyDown(KeyCode.R)){//열쇠 초기화
            currentData.resetKey();
            lastData.resetKey();
        }
    }
}
