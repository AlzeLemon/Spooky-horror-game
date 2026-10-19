using System;
using System.Threading;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "VoiceLine", menuName = "Subtitles/VoiceLine")]
public class VoiceLine : SubtitleEntry
{
    public string text;
    [Min(0)]
    public float holdTime = 1f;

    // In chars/second
    [Min(0.01f)]
    public float speed = 20f;
    
    private (GameObject, TextMeshProUGUI) Setup(GameObject parent)
    {
        GameObject textFieldObject = new(
                "My Text",
                typeof(RectTransform),
                typeof(TextMeshProUGUI)
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

        return (textFieldObject, textField);
    }

    private async Awaitable DisplayText(TextMeshProUGUI textField, CancellationToken cancellationToken)
    {
        textField.ForceMeshUpdate();

        int nrChars = textField.textInfo.characterCount;
        for (int i = 1; i <= nrChars; i++)
        {
            await Awaitable.WaitForSecondsAsync(1 / speed, cancellationToken);
            textField.maxVisibleCharacters = i;
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

        try
        {
            (textFieldObject, textField) = Setup(parent);
            await DisplayText(textField, cancellationToken);
        }
        finally
        {
            Cleanup(textFieldObject);
        }

        callback?.Invoke(next);
    }
}
