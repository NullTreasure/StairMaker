using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Bounds bounds;
    [SerializeField] private float zRange = Mathf.Infinity;
    private float yPosition = 0;

    private void Start()
    {
        bounds.center = transform.position;
        bounds.extents = new Vector3(0.7f, 0, 0.8f);
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
    public void updateMaxz()
    {
        zRange = player.position.z;
        yPosition = player.position.y;
    }
    public void resetMaxz()
    {
        zRange = Mathf.Infinity;
    }
}
