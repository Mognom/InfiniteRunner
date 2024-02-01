using System;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannels/IntEventChannel")]
public class IntEventChannel : ScriptableObject {
    public event Action<int> Channel;

    public void PostEvent(int args) {
        Channel?.Invoke(args);
    }
}