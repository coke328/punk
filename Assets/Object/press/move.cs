using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), Time.deltaTime * 1);
    }
}
