using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAlternate : MonoBehaviour
{
    public float minScale, maxScale;
    Vector3 newScale;
    bool state;
    float changeValue;
    private void Awake()
    {
        state = true;
        newScale = transform.localScale;
        changeValue = 0.0004f;
    }

    private void Update()
    {

        if (transform.localScale.x >= maxScale)
        {
            state = false;
        }
        if (transform.localScale.x <= minScale)
        {
            state = true;
        }
        
        if(state == true)
        {
            newScale.x += changeValue;
            newScale.y += changeValue;
            newScale.z += changeValue;

        }
        else
        {
            newScale.x -= changeValue;
            newScale.y -= changeValue;
            newScale.z -= changeValue;
        }
        transform.localScale = newScale;
    }
}
