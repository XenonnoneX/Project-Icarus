using UnityEngine;

public class StayInScreen : MonoBehaviour
{
    Vector3 originPosition;

    float margin = 0.5f;

    private void Start()
    {
        originPosition = transform.position;
    }

    private void Update()
    {

        transform.position = GetClosestScreenBorderPosition(originPosition, margin);
    }

    Vector3 GetClosestScreenBorderPosition(Vector3 pos, float margin)
    {
        Vector3 cameraPos = Camera.main.transform.position;

        float cameraHeight = 2f * Camera.main.orthographicSize;
        float halfCameraWidth = cameraHeight * Camera.main.aspect / 2f;

        float x = pos.x;
        float y = pos.y;

        if (pos.x > cameraPos.x + halfCameraWidth - margin)
        {
            x = cameraPos.x + halfCameraWidth - margin;
        }
        else if (pos.x < cameraPos.x - halfCameraWidth + margin)
        {
            x = cameraPos.x - halfCameraWidth + margin;
        }

        if (pos.y > cameraPos.y + cameraHeight / 2f - margin)
        {
            y = cameraPos.y + cameraHeight / 2f - margin;
        }
        else if (pos.y < cameraPos.y - cameraHeight / 2f + margin)
        {
            y = cameraPos.y - cameraHeight / 2f + margin;
        }

        return new Vector3(x, y, 0f);
    }
}