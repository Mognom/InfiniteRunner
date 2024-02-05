using MognomUtils;
using System.Collections.Generic;
using UnityEngine;


public class MapController : MonoBehaviour {

    // Segment prefabs to use when generating the map
    [SerializeField] private Transform[] mapSegments;
    [SerializeField] private Transform safeSegment;
    [SerializeField] private float replaceInSeconds; // Should be at least player velocity / 6


    [Tooltip("How many safe segments to put initially")]
    [SerializeField] private int initialSegmentCount = 3;
    private List<Transform> currentSegments;

    private Vector3 nextSpawnPosition = Vector3.zero;


    private float timeSinceLastUpdate;

    private void Awake() {
        currentSegments = new List<Transform>();
        nextSpawnPosition.z = currentSegments.Count * 3;
        for (int i = 0; i < initialSegmentCount; i++) {
            AddNewSegment(true);
        }
        AddNewSegment(false);
    }

    private void Update() {
        timeSinceLastUpdate += Time.deltaTime;

        if (timeSinceLastUpdate >= replaceInSeconds) {
            timeSinceLastUpdate -= replaceInSeconds;

            AddNewSegment(true);
            AddNewSegment(false);
            RemoveTailSegment();
            RemoveTailSegment();
        }
    }

    private void RemoveTailSegment() {
        currentSegments[0].Recycle();
        currentSegments.RemoveAt(0);
    }

    private void AddNewSegment(bool safe) {
        // Select which segment to spawn
        Transform segmentToSpawn;
        if (safe) {
            segmentToSpawn = safeSegment;
        } else {
            segmentToSpawn = mapSegments[Random.Range(0, mapSegments.Length)];
        }

        // Spawn the new segment and update the spawn position
        Transform newSegment = segmentToSpawn.Spawn(nextSpawnPosition, Quaternion.identity);
        currentSegments.Add(newSegment);
        nextSpawnPosition.z += 3;
    }
}
