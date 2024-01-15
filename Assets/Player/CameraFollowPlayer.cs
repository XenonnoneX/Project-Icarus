using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] Vector2 center;
    [SerializeField] Vector2 maxDistFromCenter;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);

        if (transform.position.x > maxDistFromCenter.x + center.x)
        {
            transform.position = new Vector3(maxDistFromCenter.x + center.x, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -maxDistFromCenter.x + center.x)
        {
            transform.position = new Vector3(-maxDistFromCenter.x + center.x, transform.position.y, transform.position.z);
        }

        if (transform.position.y > maxDistFromCenter.y + center.y)
        {
            transform.position = new Vector3(transform.position.x, maxDistFromCenter.y + center.y, transform.position.z);
        }
        else if (transform.position.y < -maxDistFromCenter.y + center.y)
        {
            transform.position = new Vector3(transform.position.x, -maxDistFromCenter.y + center.y, transform.position.z);
        }
    }
}
