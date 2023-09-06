using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "playerData",menuName ="ScriptableObject/playerData")]
public class PlayerDataScriptableObject : ScriptableObject
{

    public List<key> keys = new List<key>();//열쇠 리스트
    public int coin;
    public int health;
    public string sceneName;
    public int deadCnt;
    public void setSceneName(string name){
        sceneName = name;
    }
    public void getKey(string name,int cnt){
        int idx = containsKey(name);
        if(idx != -1){
            keys[idx].keyCount += cnt;
        }else{
            keys.Add(new key(name,cnt));
        }
    }
    public int containsKey(string keyName){
        for(int i=0; i<keys.Count; i++){
            if(keys[i].keyName == keyName){//key를 찾으면 인덱스를 리턴하고,못찾으면 -1 리턴
                return i;
            }
        }
        return -1;
    }
    public bool containsKeyOver(string name,int cnt){//키를 찾아서 일정 갯수 이상일때 true 리턴
        int idx = containsKey(name);
        if(idx != -1){
            if(keys[idx].keyCount >= cnt){
                return true;
            }
        }
        return false;
    }
    public void removeKey(string name,int cnt){
        int idx = containsKey(name);
        if(idx != -1){
            if(keys[idx].keyCount>cnt){
                keys[idx].keyCount -= cnt;
            }else{
                keys.RemoveAt(idx);
            }
        }
    }
    public void resetKey(){
        keys.Clear();
    }
    public void keyPrint(){
        foreach(key k in keys){
            Debug.Log(k.keyName + " : " + k.keyCount);
        }
    }
}

[System.Serializable]
public class key
{
    public key(string name,int count){
        this.keyName = name;
        this.keyCount = count;
    }
    public string keyName;
    public int keyCount;
}