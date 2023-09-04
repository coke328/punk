using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HpUi : MonoBehaviour
{
    //사용중인 하트 UI를 모아놓은 집합체
    public Image[] Heart;

    //읽는건 자유, 쓰는 건 private로 표기한다.
    public int Hp { get; private set; }

    //Hp의 최대치 정의
    private int Hp_Max;

    //앞에 그려질 것과 뒤에 그려질 것
    public Sprite Back, Front;
    public UnityEvent getDamage;
    public UnityEvent dead;
    public UnityEvent getHeal;

    private void Awake()
    {
        //Hp_Max의 사이즈를 정의
        Hp_Max = Heart.Length;

        //Hp 초기화.
        Hp = Hp_Max;

        //Front 이미지 초기화
        for (int i = 0; i < Hp_Max; i++)
            if (Hp > i)
            {
                Heart[i].sprite = Front;
            }
    }

    public void SetHp(int val)
    {
        //Hp 감소
        Hp += val;

        //Hp가 0밑으로 내려가면 0으로 고정하고, Hp_Max를 초과하려고 하면 Hp_Max로 고정함.
        Hp = Mathf.Clamp(Hp, 0, Hp_Max);

        //Front 이미지 모두 제거
        for (int i = 0; i < Hp_Max; i++)
            Heart[i].sprite = Back;

        //Front 이미지 그리기
        for (int i = 0; i < Hp_Max; i++)
            if (Hp > i)
            {
                Heart[i].sprite = Front;
            }
        if(val  > 0)
            getDamage.Invoke();
        else
            getHeal.Invoke();
        if(Hp == 0)
            dead.Invoke();
    }
}
