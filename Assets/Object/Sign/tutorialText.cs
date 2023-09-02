using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialText : MonoBehaviour
{
    MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }
    public void meshenable()
    {
        meshRenderer.enabled = true;
    }
    public void meshdisable()
    {
        meshRenderer.enabled = false;
    }
}
