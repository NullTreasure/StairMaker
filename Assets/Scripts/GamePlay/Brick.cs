using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Brick : MonoBehaviour
{
    private Ground ground;
    public ColorTypes.Color color;
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
            Renderer rendererPlayer = player.skin.GetComponent<Renderer>();
            if (rendererBrick.material.name == rendererPlayer.material.name)
            {
                ground.Bricks.Add(this.transform.position);
                this.gameObject.SetActive(false);
                player.addBrick();
            }
        }
        if (other.name.StartsWith("Enemy"))
        {
            Renderer rendererBrick = GetComponent<Renderer>();
            Enemy enemy = other.GetComponent<Enemy>();
            Renderer rendererPlayer = enemy.skin.GetComponent<Renderer>();
            if (rendererBrick.material.name == rendererPlayer.material.name)
            {
                ground.Bricks.Add(this.transform.position);
                this.gameObject.SetActive(false);
                enemy.addBrick();
            }
        }
    }
}
