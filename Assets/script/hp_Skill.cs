using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hp_Skill : MonoBehaviour
{
    [SerializeField]
    private hp Hp;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Hp.heal(1);
        }
    }
}
