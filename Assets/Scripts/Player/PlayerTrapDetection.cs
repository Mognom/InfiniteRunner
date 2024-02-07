using UnityEngine;

public class PlayerTrapDetection : MonoBehaviour {

    [SerializeField] private string trapTag;
    [SerializeField] private GameObject playerRagdoll;
    [SerializeField] private VoidEventChannel playerDeadChannel;
    private bool isAlive = true;

    private void OnTriggerEnter(Collider other) {
        // check if it is indeed a trap
        if (isAlive && other.gameObject.CompareTag(trapTag)) {
            isAlive = false; // This is to avoid race conditions with multiple collisions on the same frame
            GameObject ragdoll = Instantiate(playerRagdoll, transform.position, transform.rotation);
            ragdoll.transform.parent = transform.parent;
            playerDeadChannel.PostEvent();
            Destroy(this.gameObject);
        }
    }
}
