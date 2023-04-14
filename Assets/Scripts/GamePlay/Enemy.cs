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
    private IsDead isDead = new IsDead();
    private FindEnemy findEnemy = new FindEnemy();

    private int ranBrick;
    public Ground ground;
    public bool checkFind;

    private void Awake()
    {
        checkFind = false;
        anim = character.GetComponent<Animator>();
        currentState = seekBrick;
        ranBrick = Random.Range(3, 11);
        groundState = GameObject.Find("Ground1");
    }

    void Start()
    {
        onBridge = false;
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
        currentState.OnUpdate(this);
        this.destroyCharacter();
        if (getHit)
        {
            SwitchState(idle);
            anim.SetBool("fall", true);
            Invoke("standUp", 2.5f);
        }
        else
        {
            anim.SetBool("run", true);
            if (Physics.Raycast(transform.position + Vector3.up * 2, Vector3.down, Mathf.Infinity, Bridge))
            {
                onBridge = true;
            }
            else
            {
                onBridge = false;
            }
            //if (collectedBrick.Count >= 5 && !onBridge)
            //{
            //    SwitchState(findEnemy);
            //}
            if (!checkFind)
            {
                if (collectedBrick.Count >= ranBrick || (remainBrick() && collectedBrick.Count > 0))
                {
                    agent.SetDestination(new Vector3(0.01962265f, 10.02f, 80.29436f));
                    SwitchState(buildBrick);
                }
                else if (this.collectedBrick.Count == 0)
                {
                    ranBrick = Random.Range(3, 11);
                    SwitchState(seekBrick);
                }
                this.checkSpecialBrick();
            }
        }

    }
    
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        currentState.OnTriggerEnter(this, other);
        if (other.name == "Player" || other.name.StartsWith("Enemy"))
        {
            if (remainBrick())
            {
                SwitchState(buildBrick);
            }
            else
            {
                SwitchState(seekBrick);
            }
        }
    }
    public void SwitchState(EnemyBaseState state)
    {
            state.EnterState(this);
    }
    private void destroyCharacter()
    {
        
        if (!onBridge && remainBrick() && collectedBrick.Count == 0)
        {
            SwitchState(isDead);
        }
    }
    private bool remainBrick()
    {
        int count = 0;
        foreach (GameObject brick in groundState.GetComponent<Ground>().listBricks)
        {
            if (brick.GetComponent<Brick>().color == color)
            {
                if (brick.active) count++;
            }
        }
        return (count == 0);
    }
}
