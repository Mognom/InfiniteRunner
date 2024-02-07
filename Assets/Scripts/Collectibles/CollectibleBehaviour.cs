using UnityEngine;

public class CollectibleBehaviour : MonoBehaviour {

    [SerializeField] private GameObject collectibleVisual;

    private void OnEnable() {
        collectibleVisual.SetActive(true);
    }

    private void OnTriggerEnter(Collider player) {
        if (player != null) {
            collectibleVisual.SetActive(false);
        }
    }
}
