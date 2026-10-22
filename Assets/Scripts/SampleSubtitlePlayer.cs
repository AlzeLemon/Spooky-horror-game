using UnityEngine;

public class SampleSubtitlePlayer : MonoBehaviour
{
    public SubtitleEntry DefaultDoorLocked;
    public void PlayDefaultDoorLocked() {
        this.GetComponent<Subtitles>().PlayLine(DefaultDoorLocked);
    }
}
