using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent agent;
    Animator animator;
    public Transform target;
    PlayerController playerController;
    public enum STATE
    {
        IDLE,ATTACK,RUN,DEAD
    }
    public STATE state = STATE.IDLE;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (target == null)
        {
            target = GameObject.Find("Player").GetComponent<Transform>();
        }
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case STATE.IDLE:
                Idle();
                break;
            case STATE.ATTACK:
                Attack();
                break;
            case STATE.RUN:
                Run();
                break;
            case STATE.DEAD:
                Dead();
                break;
            default:
                break;
        }

    }
    public void Idle()
    {
        AllAnimationFalse();
        if(GetDistance()<15f)
        {
            state = STATE.RUN;
        }
    }
    public void Run()
    {
        
        AllAnimationFalse();
        animator.SetBool("IsWalk", true);
        agent.stoppingDistance = 3f;
        if(playerController.IsGameover==false)
        {
            agent.SetDestination(target.transform.position);
        }
        

        if (GetDistance()<=agent.stoppingDistance)
        {
            state = STATE.ATTACK;
        }
        if (GetDistance() > 20f)
        {
            state = STATE.IDLE;
        }


    }
    public void Attack()
    {
        AllAnimationFalse();
        animator.SetBool("IsAttack", true);
        transform.LookAt(target.transform.position);
        
        if (GetDistance() >agent.stoppingDistance)
        {
            state = STATE.IDLE;
        }

        
    }
    public void AllAnimationFalse()
    {
        animator.SetBool("IsAttack", false);
        animator.SetBool("IsWalk", false);
        animator.SetBool("IsDead", false);

    }
    public void Dead()
    {
        AllAnimationFalse();
        animator.SetBool("IsDead", true);
        Destroy(gameObject, 3f);
    }
    public float GetDistance()
    {
        if (playerController.IsGameover == true)
            return Mathf.Infinity;
        return (Vector3.Distance(target.position, this.transform.position));
    }
    public void DamagePlayer()
    {
        playerController.health--;
    }
   
}
