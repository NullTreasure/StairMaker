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
        if (player.position.z >= zRange + 0.4f)
        {
            player.position = new Vector3(player.position.x, yPosition, zRange + 0.4f);
        }
        //player.position = new Vector3(Mathf.Clamp(player.position.x, bounds.min.x, bounds.max.x), player.position.y, Mathf.Clamp(player.position.z, -Mathf.Infinity, zRange + 0.4f));

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
