using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactionDistance = 3f;

    private void Update()
    {
        if (playerCamera == null)
            return;

        Ray ray = playerCamera.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0f)
        );

        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
        {
            Interactable interactable =
                hit.collider.GetComponentInParent<Interactable>();

            if (interactable != null &&
                Keyboard.current != null &&
                Keyboard.current.eKey.wasPressedThisFrame)
            {
                interactable.Interact();
            }
        }
    }
}
