using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IsDead : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        enemy.currentState= this;
        enemy.gameObject.GetComponent<Collider>().enabled = false;
        enemy.skin.SetActive(false);
    }
    public override void OnUpdate(Enemy enemy)
    {

    }
    public override void OnTriggerEnter(Enemy enemy, Collider other)
    {

    }
}
