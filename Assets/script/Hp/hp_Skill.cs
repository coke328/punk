using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hp_Skill : MonoBehaviour
{
    [SerializeField]
    private hp Hp;
    [SerializeField]
    //private hp_UI Hp_UI;
    private void Start()
    {
        //Hp_UI.changeText(Hp.return_hp());
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Hp.heal(1);
            //Hp_UI.changeText(Hp.return_hp());
        }
    }
}