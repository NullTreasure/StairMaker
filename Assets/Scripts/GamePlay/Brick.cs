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
        if (other.name == "Player"  || other.name.StartsWith("Enemy"))
        {
            Character character= other.GetComponent<Character>();
            if (character.color == color)
            {
                ground.Bricks.Add(this.transform.position);
                this.gameObject.SetActive(false);
                character.addBrick();
            }
        }
        
    }
}
