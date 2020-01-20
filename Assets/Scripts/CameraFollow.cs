using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform target;
    public float smoothSpeed = 0.25f;
    int mapSize;

    void Start()
    {
        target = GameAssets.i.player.transform;
        mapSize = GameAssets.i.mapSize;

    }
    void FixedUpdate()
    {
        if (target)
        {
            Vector3 smoothedPos = Vector3.Lerp(transform.position, target.position, smoothSpeed);

            smoothedPos.x = Mathf.Clamp(smoothedPos.x, -mapSize + 7, mapSize - 7);
            smoothedPos.y = Mathf.Clamp(smoothedPos.y, -mapSize + 7, mapSize - 7);
            smoothedPos.z = -1;

            transform.position = smoothedPos;
        }
    }
}
