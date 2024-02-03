using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileRandomizer : MonoBehaviour {

    [SerializeField] private MeshOption[] meshOptions;

    private MeshFilter meshFilter;
    private void Awake() {
        meshFilter = GetComponent<MeshFilter>();
        // TODO ensure the MeshOption weights are sorted
    }

    private void OnEnable() {
        // Select which mesh it will be used and assing it to the Mesh Filter
        float random = Random.value;
        foreach (MeshOption option in meshOptions) {
            if (option.Weight >= random) {
                meshFilter.mesh = option.Mesh;
                return;
            }
        }
    }

    [Serializable]
    private struct MeshOption {
        [SerializeField] private Mesh mesh;
        [SerializeField] private float weight;

        public readonly Mesh Mesh { get { return mesh; } }
        public readonly float Weight { get { return weight; } } //From 0 to 1
    }
}
