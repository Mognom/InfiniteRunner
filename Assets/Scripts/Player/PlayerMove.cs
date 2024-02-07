using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private float changeLineTime = 0.3f;
    private int currentLine;
    private int targetLine;
    private float currentSwapTime;

    [SerializeField] private float bufferInputDuration = .1f;
    private int bufferedInput;
    private float currentInputBufferDuration;

    private PlayerInputActions playerInputActions;

    private void Start() {
        currentLine = 0;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Move.performed += OnLanechange;
    }

    private void OnLanechange(InputAction.CallbackContext context) {
        float direction = context.ReadValue<float>();
        currentInputBufferDuration = bufferInputDuration;
        bufferedInput = (int)direction;
    }

    private void Update() {
        HandleForwardMovement();
        HandleHorizontalMovement();
    }

    private void HandleForwardMovement() {
        transform.Translate(speed * Time.deltaTime * transform.forward);
    }

    private void HandleHorizontalMovement() {
        if (bufferedInput != 0) {
            if (targetLine == currentLine) {
                targetLine = currentLine + (int)bufferedInput;
                // Ensure it can't try to go outside the map bounds (-1 <-> 1)
                targetLine = Mathf.Clamp(targetLine, -1, 1);
                currentSwapTime = 0;
                bufferedInput = 0;
            } else {
                currentInputBufferDuration -= Time.deltaTime;
                if (currentInputBufferDuration <= 0) {
                    bufferedInput = 0;
                }
            }
        }

        if (targetLine != currentLine) {
            currentSwapTime += Time.deltaTime;
            float newX = FloatLerp(currentLine, targetLine, currentSwapTime, changeLineTime);
            if (newX == targetLine) {
                currentLine = targetLine;
            }
            this.transform.position = new Vector3(newX, this.transform.position.y, this.transform.position.z);
        }
    }

    private float FloatLerp(float initial, float target, float time, float maxTime) {
        float progress = Mathf.Min(1, time / maxTime);
        return initial * (1 - progress) + target * progress;
    }
}
