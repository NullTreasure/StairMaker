using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Bounds bounds;

    private void Start()
    {
        bounds.center = transform.position;
        bounds.extents = new Vector3(10.0f,0,10.0f);
    }
    public void limitArea()
    {
        if (player.position.x >= bounds.max.x) 
        { 
            player.position = new Vector3(bounds.max.x,player.position.y,player.position.z);
        }
        if (player.position.z >= bounds.max.z)
        {
            player.position = new Vector3(player.position.x, player.position.y, bounds.max.z);
        }
        if (player.position.x <= bounds.min.x)
        {
            player.position = new Vector3(bounds.min.x, player.position.y, player.position.z);
        }
        if (player.position.z <= bounds.min.z)
        {
            player.position = new Vector3(player.position.x, player.position.y, bounds.min.z);
        }

    }
    
}
