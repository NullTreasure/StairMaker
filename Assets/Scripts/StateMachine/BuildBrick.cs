using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBrick : EnemyBaseState 
{

    private Bridge _bridge;
    private GameObject bridge;

    public override void EnterState(Enemy enemy)
    {
        enemy.currentState = this;
    }
    public override void OnUpdate(Enemy enemy)
    {
        enemy.checkBrick();
    }
    public override void OnTriggerEnter(Enemy enemy, Collider other)
    {

        if (other.name.StartsWith("Real Bridge"))
        {
            bridge = other.transform.parent.gameObject;
            _bridge = bridge.GetComponent<Bridge>();
        }
        if (other.name.StartsWith("Ground"))
        {
            enemy.groundState = other.gameObject;
        }
    }
}
