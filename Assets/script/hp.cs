using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class hp : MonoBehaviour
{
    public int Hp = 10;
    public UnityEvent getDamage;
    public UnityEvent daed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void damage(int d){

        Hp -= d;
        getDamage.Invoke();
        if(Hp <= 0){
            daed.Invoke();
        }
    }
}
