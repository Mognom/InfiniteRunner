using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SegmentRandomizer : MonoBehaviour {

    [SerializeField] private Transform[] items;
    [SerializeField] private Vector3[] positions = { Vector3.zero, Vector3.left, Vector3.right };

    private void OnEnable() {
        List<Vector3> availablePositions = new List<Vector3>(positions);

        foreach (Transform item in items) {
            int randomIndex = Random.Range(0, availablePositions.Count);

            // Assign the position and remove it from the list of available positions
            item.localPosition = availablePositions[randomIndex];
            availablePositions.RemoveAt(randomIndex);
        }
    }
}
