using UnityEngine;

public class PlayerMove : MonoBehaviour {

    [SerializeField] private float speed;

    private void Update() {
        transform.Translate(speed * Time.deltaTime * transform.forward);
    }
}
