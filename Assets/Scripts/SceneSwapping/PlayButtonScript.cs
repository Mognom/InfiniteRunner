using UnityEngine;

public class PlayButtonScript : MonoBehaviour {
    public void GoToNextScene() {
        GameStateManager.I.GoToNextScene();
    }
}
