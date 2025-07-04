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
    GameObject attackHitBox;

    NavMeshAgent agent;

    [SerializeField]
    float health;

    [SerializeField]
    float followPlayerRadius; // Minimum radius for Enemy to follow player

    [SerializeField]
    float attackPlayerRadius; // Minimum radius for Enemy to follow player

    Vector3 startingPosition;
    Vector3 roamPosition;

    [SerializeField]
    float attackCooldown;

    [SerializeField]
    float attackAnimationDuration;

    [SerializeField]
    BoxCollider2D hitBoxCollider;

    [SerializeField]
    float invulnerableTime;

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
        attackHitBox.SetActive(false);
       
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
            if (currentState != EnemyState.attack && currentState != EnemyState.stagger && currentState == EnemyState.idle)
            {
                agent.SetDestination(roamPosition);
            }
        }

        float reachedPositionDistance = 1.5f;
        if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance)
        {
            roamPosition = GetRoamPosition();
        }

    }

    public void TakeDamage()
    {

        AudioManager.instance.PlaySound("sf_enemy_dmg");
        health -= 1;
        if (health == 0)
        {
            // game over
            Destroy(gameObject);
        }

        StartCoroutine(InvulnerableTime());

    }

    IEnumerator InvulnerableTime()
    {
        hitBoxCollider.enabled = false;
        yield return new WaitForSeconds(invulnerableTime);
        hitBoxCollider.enabled = true;
    }

    IEnumerator GenerateAttack()
    {
        Debug.Log("Attacking");
        yield return new WaitForSeconds(attackAnimationDuration);
        attackHitBox.SetActive(true);
        yield return new WaitForSeconds(attackAnimationDuration);
        attackHitBox.SetActive(false);
    }

    IEnumerator EnemyAttack()
    {
        float currentDistanceToPlayer;
        while (true)
        {

            Debug.Log("waiting 5 seconds");
            yield return new WaitForSeconds(attackCooldown);
            currentDistanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);
            if (currentDistanceToPlayer > attackPlayerRadius) continue;
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
