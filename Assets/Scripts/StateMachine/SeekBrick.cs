using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeekBrick : EnemyBaseState
{

    float distance = Mathf.Infinity;
    Vector3 des = Vector3.zero;


    public override void EnterState(Enemy enemy)
    {
        enemy.currentState = this;
        enemy.agent.SetDestination(SeekClosestBrick(enemy));
    }
    public override void OnUpdate(Enemy enemy)
    {
        if(enemy.agent.remainingDistance < 0.2f)
        {

            enemy.agent.SetDestination(SeekClosestBrick(enemy));

        }
    }
    public override void OnTriggerEnter(Enemy enemy,Collider other)
    {
        if (other.name.StartsWith("Ground"))
        {
            enemy.agent.SetDestination(SeekClosestBrick(enemy));
            enemy.ground = other.GetComponent<Ground>();
            enemy.groundState = other.gameObject;
        }
    }

    private Vector3 SeekClosestBrick(Enemy enemy)
    {
        distance = Mathf.Infinity;
        des = Vector3.zero;
        if (enemy.ground.listBricks.Count > 0)
        {

            foreach (GameObject brick in enemy.ground.listBricks)
            {
                if (brick.active)
                {
                    if (brick.GetComponent<Brick>().color == enemy.color)
                    {
                        if (Vector3.Distance(enemy.transform.position, brick.transform.position) < distance)
                        {
                            distance = Vector3.Distance(enemy.transform.position, brick.transform.position);
                            des = brick.transform.position;
                        }
                    }
                }
            }
            return des;
        }
        else return enemy.transform.position;
    }
}

