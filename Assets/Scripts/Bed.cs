using UnityEngine;
using UnityEngine.Events;

public class Bed : MonoBehaviour
{
    public bool CanProgressToNextDay = false;
    public UnityEvent unityEvent;
    public void UnlockProgression() {
        CanProgressToNextDay = true;
    }
    public void Interact() {
        if (CanProgressToNextDay) {
            unityEvent.Invoke();
        }
    }
}
