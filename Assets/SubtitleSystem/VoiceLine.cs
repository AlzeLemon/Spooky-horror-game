using System;
using System.Threading;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "VoiceLine", menuName = "Subtitles/VoiceLine")]
public class VoiceLine : SubtitleEntry
{
    public string text;
    [Min(0)]
    public float holdTime = 1f;
    public AudioClip voice = null;
    [Range(0f, 1f)]
    public float volume = 0.5f;

    // In chars/second
    [Min(0.01f)]
    public float speed = 20f;

    [Min(1)]
    public int charsPerReveal = 1;

    [Range(0.5f, 1.5f)]
    public float basePitch = 1f;

    [Range(0f, 1f)]
    public float pitchOffset = 0.05f;

    private (GameObject, TextMeshProUGUI, AudioSource) Setup(GameObject parent)
    {
        GameObject textFieldObject = new(
                "Text",
                typeof(RectTransform),
                typeof(TextMeshProUGUI),
                typeof(AudioSource)
            );

        textFieldObject.transform.SetParent(parent.transform, false);

        RectTransform textFieldTransform = textFieldObject.GetRectTransform();
        textFieldTransform.anchorMin = Vector2.zero;
        textFieldTransform.anchorMax = new Vector2(1, 0);
        textFieldTransform.offsetMin = Vector2.zero;
        textFieldTransform.offsetMax = Vector2.zero;
        textFieldTransform.pivot = new Vector2(0.5f, 0);

        TextMeshProUGUI textField = textFieldObject.GetComponent<TextMeshProUGUI>();
        textField.alignment = TextAlignmentOptions.Bottom;
        textField.text = text;
        textField.maxVisibleCharacters = 0;

        AudioSource audioSource = textFieldObject.GetComponent<AudioSource>();
        audioSource.playOnAwake = false;

        return (textFieldObject, textField, audioSource);
    }

    private async Awaitable DisplayText(TextMeshProUGUI textField, AudioSource audioSource, CancellationToken cancellationToken)
    {
        textField.ForceMeshUpdate();

        int nrChars = textField.textInfo.characterCount;
        for (int i = 1; i <= nrChars / charsPerReveal; i++)
        {
            await Awaitable.WaitForSecondsAsync(charsPerReveal / speed, cancellationToken);
            textField.maxVisibleCharacters = i * charsPerReveal;

            if (voice != null) {
                bool playSound = false;
                for (int offset = 1; offset <= charsPerReveal; offset++)
                {
                    char c = textField.textInfo.characterInfo[i - offset].character;
                    if (char.IsLetterOrDigit(c))
                    {
                        playSound = true;
                        break;
                    }
                }

                if (playSound)
                {
                    audioSource.pitch = UnityEngine.Random.Range(basePitch - pitchOffset, basePitch + pitchOffset);
                    audioSource.PlayOneShot(voice, volume);
                }
            }
        }

        await Awaitable.WaitForSecondsAsync(holdTime, cancellationToken);
    }

    private void Cleanup(GameObject textFieldObject)
    {
        if (textFieldObject != null)
        {
            textFieldObject.SetActive(false);
            Destroy(textFieldObject);
        }
    }

    public override async Awaitable Play(GameObject parent, Action<SubtitleEntry> callback, CancellationToken cancellationToken = default)
    {
        GameObject textFieldObject = null;
        TextMeshProUGUI textField = null;
        AudioSource audioSource = null;

        try
        {
            (textFieldObject, textField, audioSource) = Setup(parent);
            await DisplayText(textField, audioSource, cancellationToken);
        }
        finally
        {
            Cleanup(textFieldObject);
        }

        callback?.Invoke(next);
    }
}
