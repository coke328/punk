using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMoveMent : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerPos;
    public float followSpeed = 1f;
    public Vector3 camlocalPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tmp = transform.position - camlocalPos;
        tmp = Vector3.Lerp(tmp,playerPos.position,Time.deltaTime * followSpeed);
        transform.position = tmp + camlocalPos;
    }
}
