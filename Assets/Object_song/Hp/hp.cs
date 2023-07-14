using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class hp : MonoBehaviour
{
    public int MaxHp = 10;
    public int Hp = 10;
    public UnityEvent getDamage;
    public UnityEvent daed;
    public UnityEvent getHeal;
    public void damage(int d){

        Hp -= d;
        getDamage.Invoke();
        if(Hp <= 0){
            daed.Invoke();
        }
    }
    public void heal(int d)
    {
        //회복가능
        if ((Hp + d) <= MaxHp)
            Hp += d;
        else//최대체력이라 회복불가
            Hp = MaxHp;
        getHeal.Invoke();
    }
    public int return_hp() { return Hp; }
    public bool is_max() { return MaxHp == Hp; }
}
