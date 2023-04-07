using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    public NavMeshAgent agent;

    public EnemyBaseState currentState;
    private SeekBrick seekBrick = new SeekBrick();
    private BuildBrick buildBrick = new BuildBrick();
    private int ranBrick;

    private void Awake()
    {
        countBrick= 0;
        currentState = seekBrick;
        ranBrick = Random.Range(1, 8); 
    }

    void Start()
    {
        currentState.EnterState(this);
        agent = this.GetComponent<NavMeshAgent>();
    }
    void Update()
    {

        if (GameManager.Instance.endGame) return;
        currentState.OnUpdate(this);
        if (this.countBrick >= ranBrick)
        {
            agent.SetDestination(new Vector3(0.18f, 3.61f, 79.86f));
            SwitchState(buildBrick);
        }
        else if (this.countBrick == 0) 
        {
            ranBrick= Random.Range(1, 8);
            SwitchState(seekBrick);
        }
        this.checkSpecialBrick();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(this, other);
    }
    public void SwitchState(EnemyBaseState state)
    {
            state.EnterState(this);
    }
}
