using UnityEngine;

public class Move : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 5f;
    private Rigidbody2D rb2D;
    private Vector2 movementInput;
    private Animator animator;

    public Transform Aim;
    bool isWalkin = false;


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
            Debug.Log("SE MUEVE...");
            isWalkin = true;
            animator.SetFloat("UltimoPosX", movementInput.x);
            animator.SetFloat("UltimoPosY", movementInput.y);
        }
        else
        {
            isWalkin = false;
        }
    }
    private void FixedUpdate()
    {
        rb2D.linearVelocity = movementInput * speed;

        if (isWalkin)
        {
            Vector3 vector3 = Vector3.left * movementInput.x + Vector3.down * movementInput.y;
            Aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
        }
    }


}
