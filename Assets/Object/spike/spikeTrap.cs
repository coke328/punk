using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class spikeTrap : MonoBehaviour
{
    public int damage;
    public float AppearTime;
    public float DisAppearTime;
    private BoxCollider2D bc;
    private SpriteRenderer sr;
    private Animator anim;
    public UnityEvent<int> Spikehit;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        StartCoroutine(Toggle());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Spikehit.Invoke(damage);
        }
    }
    private IEnumerator Toggle()
    {
        while (true)
        {
            bc.enabled = false;
            anim.Play("Down_");
            yield return new WaitForSeconds(AppearTime);
            bc.enabled = true;
            anim.Play("Up_");
            yield return new WaitForSeconds(DisAppearTime);
        }
    }
}