using UnityEngine;
using UnityEngine.InputSystem;

public class CircleScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    InputAction jumpAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jumpAction = InputSystem.actions.FindAction("Jump");
         
    }

    // Update is called once per frame
    void Update()
    {
        if(jumpAction.IsPressed())
        {
            //Debug.Log("saltando");
            myRigidbody.linearVelocity = Vector2.up * flapStrength;
        } 
        
    }
}
