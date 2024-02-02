using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour {
    // Jump values
    [SerializeField] private float jumpStrenght;

    // Ground check values
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundedDistance;

    private PlayerInputActions playerInputActions;
    private Rigidbody rb;
    private BoxCollider boxCollider;

    void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
        playerInputActions.Player.Jump.performed += OnPlayerJump;

        rb = GetComponent<Rigidbody>();

        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnPlayerJump(InputAction.CallbackContext context) {
        if (CheckGrounded()) {
            rb.velocity = Vector3.zero; // Ensure it is not falling anymore
            rb.AddForce(Vector3.up * jumpStrenght, ForceMode.Impulse);
        }
    }

    private bool CheckGrounded() {
        Vector3 direction = Vector3.down;

        // Perform a boxcast from above the player collider to avoid starting in contact to the ground
        bool hit = Physics.BoxCast(boxCollider.bounds.center + Vector3.up * groundedDistance, boxCollider.bounds.size, direction, this.transform.rotation, groundedDistance * 2, groundLayer);
        Debug.Log(hit);
        if (hit) {
            return true;
        }

        Debug.Log("from" + (boxCollider.bounds.center + Vector3.up * groundedDistance));
        return false;
    }
}
