using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEnemy : EnemyBaseState
{
    private int proFindEnemy;
    public override void EnterState(Enemy enemy)
    {
        proFindEnemy = Random.Range(1,3);
        if (proFindEnemy == 1)
        {
            enemy.currentState= this;
            enemy.checkFind = true;
        }
    }
    public override void OnUpdate(Enemy enemy)
    {

        Collider[] listColliders = Physics.OverlapSphere(enemy.transform.position, 6.0f);
        int count = 0;
        foreach (Collider collider in listColliders)
        {
            if (collider.name == "Player" || collider.name.StartsWith("Enemy"))
            {
                count++;
                if (collider.GetComponent<Character>().collectedBrick.Count < enemy.collectedBrick.Count && collider.GetComponent<Character>().collectedBrick.Count > 0)
                {
                    enemy.agent.SetDestination(collider.transform.position);

                    break;
                }
            }
        }
        if (count == 0) enemy.checkFind = false;
    }
    public override void OnTriggerEnter(Enemy enemy, Collider other)
    {
    }
}
