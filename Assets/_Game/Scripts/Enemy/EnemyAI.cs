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
    private Animator animator;

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
    [SerializeField] private SpriteRenderer spRender;
    [SerializeField] private Material whiteShader;
    private Material materialOriginal;
    [SerializeField] private GameObject corpseObject;//Cadaver Objecto

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
        corpseObject.SetActive(false);//Cadaver desactivado
        attackHitBox.SetActive(false);
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        animator = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(EnemyAttack());
        materialOriginal = spRender.material;//Guardar material original
        currentState = EnemyState.idle;
        startingPosition = transform.position;
        roamPosition = GetRoamPosition();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /* bool facingLeft = roamPosition.x - transform.position.x < 0 ? true : false;
        animator.SetBool("moving", true);
        animator.SetBool("facingLeft", facingLeft);
        animator.SetFloat("movX", facingLeft ? -1 : 1); */


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

        Vector3 movementInput = agent.velocity; //identificar movimiento
        float speed = movementInput.magnitude;
        // Dar animación
        animator.SetFloat("speed", speed);
        animator.SetFloat("movX", movementInput.x);
        animator.SetFloat("moveY", movementInput.y);

        if (movementInput.x != 0 || movementInput.y != 0)
        {
            Vector2 dir = new Vector2(movementInput.x, movementInput.y).normalized;
            animator.SetFloat("posX", dir.x);
            animator.SetFloat("posY", dir.y);
        }
        /* if (currentState == EnemyState.attack)
        {
            Debug.Log("Disparando trigger de ataque");
            animator.SetTrigger("attack");
        } */
    }

    public void TakeDamage()
    {

        AudioManager.instance.PlaySound("sf_enemy_dmg");
        health -= 1;
        animator.SetTrigger("hit");
        if (health == 0)
        {
            if (corpseObject != null)
            {
                corpseObject.SetActive(true); // 👁️ Mostrar sprite de cadáver
                corpseObject.transform.SetParent(null); // 🔓 Lo suelta del enemigo para que no se destruya
            }
            // game over
            Destroy(gameObject);
        }

        StartCoroutine(InvulnerableTime());

    }

    IEnumerator InvulnerableTime()
    {
        hitBoxCollider.enabled = false;
        for (int i = 0; i < 2; i++)
        {
            spRender.material = whiteShader;
            yield return new WaitForSeconds(0.1f); // tiempo visible del blanco
            spRender.material = materialOriginal;
            yield return new WaitForSeconds(0.1f); // tiempo visible del normal
        }
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
                animator.SetTrigger("attack");
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
