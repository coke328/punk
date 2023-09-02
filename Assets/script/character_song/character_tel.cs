using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_tel : MonoBehaviour
{
   public void teleport(Vector3 pos)
    {
        transform.position = pos;
    }
}
