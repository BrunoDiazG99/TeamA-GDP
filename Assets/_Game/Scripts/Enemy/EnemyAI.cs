using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    idle,
    attack,
    stagger
}

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    Transform playerTarget;

    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    BoxCollider2D hitBox;

    [SerializeField]
    BoxCollider2D moveBox;

    [SerializeField]
    BoxCollider2D attackHitBox;

    NavMeshAgent agent;

    [SerializeField]
    float health;

    [SerializeField]
    float followPlayerRadius = 10f; // Minimum radius for Enemy to follow player

    Vector3 startingPosition;
    Vector3 roamPosition;

    [SerializeField]
    float attackCooldown = 5f;

    [SerializeField]
    float attackAnimationDuration = 0.4f;
    //Animator animator;
    bool facingLeft;

    EnemyState currentState;


    static Vector3 GetRandomDir()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    Vector3 GetRoamPosition()
    {
        Vector3 randomDir = GetRandomDir();

        randomDir.x = randomDir.x + Random.Range(-1f, 1f);
        randomDir.y = randomDir.y + Random.Range(-1f, 1f);

        return startingPosition + randomDir;
    }

    void Awake()
    {
        facingLeft = false;
        attackHitBox.enabled = false;
       
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        
        //animator = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(EnemyAttack());
        currentState = EnemyState.idle;
        startingPosition = transform.position;
        roamPosition = GetRoamPosition();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //facingLeft = roamPosition.x - transform.position.x < 0 ? true : false;
        //animator.SetBool("moving", true);
        //animator.SetBool("facingLeft", facingLeft);
        //animator.SetFloat("movX", facingLeft ? -1 : 1);


        float currentDistanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);

        if (currentDistanceToPlayer < followPlayerRadius)
        {

            if (currentState != EnemyState.attack && currentState != EnemyState.stagger && currentState == EnemyState.idle)
            {
                agent.SetDestination(playerTarget.position);
            }
        }
        else
        {
            agent.SetDestination(roamPosition);
        }

        float reachedPositionDistance = 1.5f;
        if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance)
        {
            roamPosition = GetRoamPosition();
        }

    }

    IEnumerator GenerateAttack()
    {
        Debug.Log("Attacking");
        yield return new WaitForSeconds(attackAnimationDuration);
        attackHitBox.enabled = true;
        yield return new WaitForSeconds(attackAnimationDuration);
        attackHitBox.enabled = false;
    }

    IEnumerator EnemyAttack()
    {
        while (true)
        {

            //Debug.Log("waiting 5 seconds");
            yield return new WaitForSeconds(attackCooldown);
            agent.isStopped = true;
            if (currentState != EnemyState.attack && currentState != EnemyState.stagger)
            {
                //Debug.Log("5 second passed");
                //animator.SetBool("moving", false);
                //animator.SetBool("attacking", true);
                currentState = EnemyState.attack;

                //Debug.Log("starting attack");
                StartCoroutine(GenerateAttack());
                //Debug.Log("Finishing attack");

                //animator.SetBool("attacking", false);
                //animator.SetBool("moving", true);
                currentState = EnemyState.idle;
            }
            agent.isStopped = false;

        }
    }
}
