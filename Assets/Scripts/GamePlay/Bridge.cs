using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Bounds bounds;

    private void Start()
    {
        bounds.center = transform.position;
        bounds.extents = new Vector3(0.4f, 0, 0.8f);
    }
    public void limitArea()
    {
        if (player.position.x >= bounds.max.x)
        {
            player.position = new Vector3(bounds.max.x, player.position.y, player.position.z);
        }
        if (player.position.x <= bounds.min.x)
        {
            player.position = new Vector3(bounds.min.x, player.position.y, player.position.z);
        }
    }
}
