using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Hp_orb : MonoBehaviour
{
    SpriteRenderer spriterenderer;
    Transform transformobj;
    [SerializeField]
    private hp Hp;
    //[SerializeField]
    //private hp_UI Hp_UI;
    Vector2 vec;
    Vector2 direction;
    Vector3 scale;
    RaycastHit2D rayHit;
    public int healnum;
    public float scalenum;
    private void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
        transformobj = GetComponent<Transform>();
        vec = new Vector2(transform.position.x, spriterenderer.bounds.min.y);
        direction = Vector2.up;
        scale = transformobj.localScale;
    }
    private void Update()
    {
        scale = new Vector3(scalenum, scalenum, 1);
        rayHit = Physics2D.Raycast(vec, direction, scale.x, LayerMask.GetMask("player"));
        Debug.DrawRay(vec, direction * scalenum, new Color(0, 1, 0, 1));
        if (rayHit.collider != null && !Hp.is_max())
        {
            Hp.heal(healnum);
            //Hp_UI.changeText(Hp.return_hp());
            Destroy(this.gameObject);
        }
    }
}