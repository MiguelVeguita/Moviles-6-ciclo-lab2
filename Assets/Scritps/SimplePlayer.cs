using UnityEngine;
using Unity.Netcode;

public class SimplePlayer : NetworkBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f; 

    [SerializeField] private LayerMask groundLayer; 

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void OnNetworkSpawn()
    {
     
        CameraFollow.Instance.SetTarget(transform);
    }

    void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                RequestJumpServerRpc();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");

        ValidateMovementRpc(inputX, inputZ);
        
    }

    private bool IsGrounded()
    {
       
        float raycastDistance = 1.1f; 
        return Physics.Raycast(transform.position, Vector3.down, raycastDistance, groundLayer);
    }

    [Rpc(SendTo.Server)]
    public void ValidateMovementRpc(float inputX, float inputZ)
    {
        Vector3 moveDirection = new Vector3(inputX, 0, inputZ).normalized;

        rb.linearVelocity = new Vector3(moveDirection.x * speed, rb.linearVelocity.y, moveDirection.z * speed);
    }
    [Rpc(SendTo.Server)]
    private void RequestJumpServerRpc()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
/*
public class SimplePlayer : NetworkBehaviour

{

    public float speed;

    void Update()

    {

        if (!IsOwner) return;

        float y = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;

        float x = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;



        ValidateMovementRpc(x, y);

    }

    [Rpc(SendTo.Server)]

    public void ValidateMovementRpc(float x, float y)

    {

        Vector3 moveDir = new Vector3(x, y, 0).normalized;



        transform.position += moveDir * speed * Time.deltaTime;

    }

}*/