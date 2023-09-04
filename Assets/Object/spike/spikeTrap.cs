using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class spikeTrap : MonoBehaviour
{
    public float AppearTime;
    public float DisAppearTime;
    private BoxCollider2D bc;
    private SpriteRenderer sr;
    public UnityEvent Spikehit;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(Toggle());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Spikehit.Invoke();
        }
    }
    private IEnumerator Toggle()
    {
        while (true)
        {
            bc.enabled = true;
            sr.color = Color.white;
            yield return new WaitForSeconds(AppearTime);
            bc.enabled = false;
            sr.color = Color.black;
            yield return new WaitForSeconds(DisAppearTime);
        }
    }
}