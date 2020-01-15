using UnityEngine;

public class Effects : MonoBehaviour
{
    float time;

    void Update()
    {
        time += Time.deltaTime;
        if (time >= 7)
            Destroy(gameObject);
    }
}
