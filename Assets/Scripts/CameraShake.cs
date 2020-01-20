using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Animator camAnim;

    public void CamShake(string trigger)
    {
        int n = Random.Range(0, 4);
        if (n == 0)
        {
            if (trigger == "ShakeSmall")
                camAnim.SetTrigger("ShakeSmall1");
            else if (trigger == "ShakeBig")
                camAnim.SetTrigger("ShakeBig1");
            else
                Debug.LogWarning("Trigger '" + trigger + "' not found");
        }
        else if (n == 1)
        {
            if (trigger == "ShakeSmall")
                camAnim.SetTrigger("ShakeSmall2");
            else if (trigger == "ShakeBig")
                camAnim.SetTrigger("ShakeBig2");
            else
                Debug.LogWarning("Trigger '" + trigger + "' not found");
        }
        else if (n == 2)
        {
            if (trigger == "ShakeSmall")
                camAnim.SetTrigger("ShakeSmall3");
            else if (trigger == "ShakeBig")
                camAnim.SetTrigger("ShakeBig3");
            else
                Debug.LogWarning("Trigger '" + trigger + "' not found");
        }
        else if (n == 3)
        {
            if (trigger == "ShakeSmall")
                camAnim.SetTrigger("ShakeSmall4");
            else if (trigger == "ShakeBig")
                camAnim.SetTrigger("ShakeBig4");
            else
                Debug.LogWarning("Trigger '" + trigger + "' not found");
        }
    }
}
