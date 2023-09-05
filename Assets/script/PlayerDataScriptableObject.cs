using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "playerData",menuName ="ScriptableObject/playerData")]
public class PlayerDataScriptableObject : ScriptableObject
{
    public Dictionary<string,int> key;//열쇠의 이름과 갯수
    public int coin;
    public int health;
    public string sceneName;
    public int deadCnt;
    public void setSceneName(string name){
        sceneName = name;
    }
}
