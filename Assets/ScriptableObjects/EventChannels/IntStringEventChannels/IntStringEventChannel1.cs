using System;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannels/IntStringEventChannel")]
public class IntStringEventChannel : ScriptableObject {
    public event Action<int, String> Channel;

    public void PostEvent(int arg1, String arg2) {
        Channel?.Invoke(arg1, arg2);
    }
}