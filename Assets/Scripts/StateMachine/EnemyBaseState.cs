using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState 
{
    public virtual void EnterState(Enemy enemy) 
    { 
    
    }
    public virtual void OnUpdate(Enemy enemy)
    {

    }
    public virtual void OnTriggerEnter(Enemy enemy, Collider other)
    {
        
    }
}
