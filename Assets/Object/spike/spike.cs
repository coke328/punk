using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class spike : MonoBehaviour
{
    public float[] spike_tel_pos = new float[2];
    public UnityEvent<Vector3> spike_hit;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            spike_hit.Invoke(new Vector3(spike_tel_pos[0], spike_tel_pos[1], 0));
        }
    }
}
