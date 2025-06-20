using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{

    public float moveSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        // Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);



    }
}
