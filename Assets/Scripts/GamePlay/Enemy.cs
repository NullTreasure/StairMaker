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
    private Idle idle= new Idle();

    private int ranBrick;
    public Ground ground;

    private void Awake()
    {
        countBrick= 0;
        currentState = seekBrick;
        ranBrick = Random.Range(1, 8); 
    }

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        currentState.EnterState(this);
    }
    void Update()
    {

        if (GameManager.Instance.endGame) 
        {
            SwitchState(idle);
            return;
        }
        if (this.countBrick >= ranBrick)
        {
            agent.SetDestination(new Vector3(0.18f, 3.61f, 79.86f));
            SwitchState(buildBrick);
        }
        else if (this.countBrick == 0 ) 
        {
            ranBrick= Random.Range(1, 8);
            SwitchState(seekBrick);
        }
        this.checkSpecialBrick();
        currentState.OnUpdate(this);
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
