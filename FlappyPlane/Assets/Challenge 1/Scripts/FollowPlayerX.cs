using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset = new Vector3(19, 1.20f, 0);

    void Update()
    {
        if (player)
        {
            transform.position = player.transform.position + offset;
        }
    }
}
