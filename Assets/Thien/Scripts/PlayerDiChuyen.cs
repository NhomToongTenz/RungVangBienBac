using UnityEngine;

public class PlayerDiChuyen : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 movement;
    private playerScript fishingScript;

    void Start()
    {
        fishingScript = GetComponent<playerScript>();
    }

    void Update()
    {
        if (fishingScript != null && fishingScript.isFishing)
        {
            // Disable movement if fishing
            movement = Vector2.zero;
        }
        else
        {
            // Get input for movement
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }

      
    }

    void FixedUpdate()
    {
        // Apply movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
