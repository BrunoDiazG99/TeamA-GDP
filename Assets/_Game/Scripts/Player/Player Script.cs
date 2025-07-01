using UnityEngine;

public class Move : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 5f;
    private Rigidbody2D rb2D;
    private Vector2 movementInput;
    private Animator animator;





    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
    }

    public void Movimiento()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        movementInput = movementInput.normalized;
        //Darle animacion
        animator.SetFloat("Horizontal", movementInput.x);
        animator.SetFloat("Vertical", movementInput.y);
        animator.SetFloat("SPEED", movementInput.magnitude);

        if (movementInput.x != 0 || movementInput.y != 0)
        {
            animator.SetFloat("UltimoPosX", movementInput.x);
            animator.SetFloat("UltimoPosY", movementInput.y);
        }
    }
    private void FixedUpdate()
    {
        rb2D.linearVelocity = movementInput * speed;
    }


}
