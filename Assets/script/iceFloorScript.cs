using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceFloorScript : MonoBehaviour
{
    public playerMoveMent playerMoveScript;

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.name == "player"){
            playerMoveScript.onIce = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.name == "player"){
            playerMoveScript.onIce = false;
        }
    }
}