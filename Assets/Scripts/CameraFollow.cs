using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform target;
    public float smoothSpeed = 0.25f;

    void Start()
    {
        target = GameAssets.i.player.transform;
    }
    void FixedUpdate()
    {
        int mapSize = GameAssets.i.mapSize;
        
        Vector3 smoothedPos = Vector3.Lerp(transform.position, target.position, smoothSpeed);

        smoothedPos.x = Mathf.Clamp(smoothedPos.x, -mapSize + 7, mapSize - 7);
        smoothedPos.y = Mathf.Clamp(smoothedPos.y, -mapSize + 7, mapSize - 7);
        smoothedPos.z = -1;

        transform.position = smoothedPos;

    }
}
