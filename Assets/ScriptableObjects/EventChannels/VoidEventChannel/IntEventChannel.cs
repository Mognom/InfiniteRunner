using System;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannels/VoidEventChannel")]
public class VoidEventChannel : ScriptableObject {
    public event Action Channel;

    public void PostEvent() {
        Channel?.Invoke();
    }
}