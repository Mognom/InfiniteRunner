using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour {
    // Jump values
    [SerializeField] private float jumpStrenght;

    // Ground check values
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundedDistance;

    private PlayerInputActions playerInputActions;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
        playerInputActions.Player.Jump.performed += OnPlayerJump;

        rb = GetComponent<Rigidbody2D>();

        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnPlayerJump(InputAction.CallbackContext context) {
        if (CheckGrounded()) {
            rb.velocity = Vector3.zero; // Ensure it is not falling anymore
            rb.AddForce(Vector2.up * jumpStrenght, ForceMode2D.Impulse);
        }
    }

    private bool CheckGrounded() {
        Vector2 direction = Vector2.down;

        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, direction, groundedDistance, groundLayer);
        if (hit && hit.collider != null) {
            return true;
        }

        return false;
    }
}
