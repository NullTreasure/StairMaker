using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : EnemyBaseState
{
        public override void EnterState(Enemy enemy)
        {
            enemy.currentState= this;
            enemy.agent.velocity = Vector3.zero;
        }
        public override void OnUpdate(Enemy enemy)
        {
            enemy.agent.velocity = Vector3.zero;
        }
        public override void OnTriggerEnter(Enemy enemy, Collider other)
        {

        }
}
