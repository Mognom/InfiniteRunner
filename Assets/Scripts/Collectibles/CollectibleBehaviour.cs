using UnityEngine;

public class CollectibleBehaviour : MonoBehaviour {

    [SerializeField] private GameObject collectibleVisual;
    [SerializeField] private int scoreValue;
    [SerializeField] private IntEventChannel scoreCollectedChannel;

    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable() {
        collectibleVisual.SetActive(true);
    }

    private void OnTriggerEnter(Collider player) {
        if (player != null) {
            scoreCollectedChannel.PostEvent(scoreValue);
            audioSource.Play();
            collectibleVisual.SetActive(false);
        }
    }
}
