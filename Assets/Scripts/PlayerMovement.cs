using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public Transform[] spawnPoints = new Transform[3];
    public float jumpForce = 10f;
    public float switchLaneSpeed = 15f; 
    public float gravity = 25f;

    public int currentLane = 1;
    private float verticalVelocity = 0f;
    private bool isGrounded = true;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.A) && currentLane > 0)
        {
            currentLane--;
        }
        
        if (Input.GetKeyDown(KeyCode.D) && currentLane < 2)
        {
            currentLane++;
        }

        float targetX = spawnPoints[currentLane].position.x;
        float currentX = Mathf.Lerp(transform.position.x, targetX, switchLaneSpeed * Time.deltaTime);

        float groundY = spawnPoints[currentLane].position.y; 

        if (animator != null)
        {
            float distanceToTarget = targetX - transform.position.x;

            if (Mathf.Abs(distanceToTarget) > 0.05f)
            {
                float moveDirection = Mathf.Sign(distanceToTarget); 
                animator.SetFloat("LaneSpeed", moveDirection, 0.1f, Time.deltaTime);
            }
            else
            {
                animator.SetFloat("LaneSpeed", 0f, 0.1f, Time.deltaTime);
            }
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = jumpForce;
            isGrounded = false;

            if (animator != null)
            {
                animator.SetBool("IsJumping", true);
            }
        }

        float currentY;

        if (!isGrounded)
        {
            verticalVelocity -= gravity * Time.deltaTime;
            currentY = transform.position.y + (verticalVelocity * Time.deltaTime);

            if (currentY <= groundY && verticalVelocity <= 0)
            {
                currentY = groundY;
                isGrounded = true;
                verticalVelocity = 0f;

                if (animator != null)
                {
                    animator.SetBool("IsJumping", false);
                }
            }
        }
        else
        {
            verticalVelocity = 0f;
            currentY = groundY; 
        }

        transform.position = new Vector3(currentX, currentY, transform.position.z);
    }
}