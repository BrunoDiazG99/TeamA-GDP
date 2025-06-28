using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{

    Transform target;

    float topLeftX, topLeftY;
    float botRightX, botRightY;

    public GameObject startingMap;
    //public GameObject player;


    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        //target = player.transform;
        SetBound(startingMap);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void Update()
    {
        // Movement
        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");
        //Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;
        //transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        transform.position = new Vector3(
            target.position.x,
            target.position.y,
            //Mathf.Clamp(target.position.x,topLeftX,botRightX),
            //Mathf.Clamp(target.position.y,botRightY,topLeftY),
            transform.position.z
            );

    }

    public void SetBound(GameObject map)
    {
        float cameraSize = Camera.main.orthographicSize;

        topLeftX = map.transform.position.x + cameraSize;
        topLeftY = map.transform.position.y - cameraSize;

        botRightX = map.transform.position.x + 0 - cameraSize;
        botRightY = map.transform.position.y - 0 + cameraSize;
    }
}
