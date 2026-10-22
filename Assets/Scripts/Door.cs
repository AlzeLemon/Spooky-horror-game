using UnityEngine;

public class Door : MonoBehaviour
{
    public bool IsOpen = false;
    public bool IsLocked = false;
    public void Toggle() {
        if (!IsOpen && !IsLocked) {
            IsOpen = true;
        }
        else if (IsOpen) {
            IsOpen = false;
        }
        if(IsLocked) {
            FindAnyObjectByType<SampleSubtitlePlayer>().PlayDefaultDoorLocked();
        }
        this.GetComponent<Animator>().SetBool("IsOpen", IsOpen);
    }
}
