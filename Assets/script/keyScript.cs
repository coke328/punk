using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyScript : MonoBehaviour
{
    public string keyName;
    public int count;

    void OnTriggerEnter2D(Collider2D other){
        
        respawn respawnScript = other.gameObject.GetComponent<respawn>();
        if(respawnScript){
            respawnScript.getKey(keyName,count);
            this.gameObject.SetActive(false);
        }
    }
}
