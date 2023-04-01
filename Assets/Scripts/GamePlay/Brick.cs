using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Brick : MonoBehaviour
{
    private Ground ground;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.StartsWith("Ground"))
        {
            ground = other.GetComponent<Ground>();
        }
        if (other.name == "Player")
        {
            Renderer rendererBrick = GetComponent<Renderer>();
            Player player = other.GetComponent<Player>();
            Renderer rendererPlayer = player.player.GetComponent<Renderer>();
            if (rendererBrick.material.name == rendererPlayer.material.name)
            {
                ground.Bricks.Add(this.transform.position);
                this.gameObject.SetActive(false);
                player.addBrick();
            }
        }
    }
}
