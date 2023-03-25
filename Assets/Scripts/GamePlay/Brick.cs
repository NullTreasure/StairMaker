using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Brick : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Renderer rendererBrick = GetComponent<Renderer>();
        Player player = other.GetComponent<Player>();
        Renderer rendererPlayer = player.player.GetComponent<Renderer>();
        if (rendererBrick.material.name == rendererPlayer.material.name)
        {
            Destroy(this.gameObject);
            player.addBrick();
        }
    }
}
