using UnityEngine;

public class RagdollBehaviour : MonoBehaviour {
    [SerializeField] Rigidbody[] rigidbodies;
    public float explosionForce;
    public Vector3 explosionSource;
    public float explosionRadious;
    void Start() {
        foreach (var rigidbody in rigidbodies) {
            rigidbody.AddForce(transform.forward * explosionForce);
        }
    }
}
