﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    float time = 0;
    float delay = 0.1f;
    void Update()
    {
        time += Time.deltaTime;
        if(time >= delay){
            Destroy(gameObject);
        }
    }
}
