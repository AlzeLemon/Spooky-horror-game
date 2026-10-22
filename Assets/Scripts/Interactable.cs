using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent unityEvent;
    public void Interact()
    {
        unityEvent.Invoke();
    }
}