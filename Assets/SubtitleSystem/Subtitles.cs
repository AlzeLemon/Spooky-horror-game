using System;
using System.Threading;
using Unity.SmartStrings.Core.Extensions;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class Subtitles : MonoBehaviour
{
    public float bottomMargin = 36f;
    public float leftMargin = 360f;
    public float rightMargin = 360f;

    private RectTransform rectTransform;
    private CancellationTokenSource currentToken = null;

    void Awake()
    {
        rectTransform = gameObject.GetRectTransform();

        rectTransform.SetY(bottomMargin);
        rectTransform.SetLeft(leftMargin);
        rectTransform.SetRight(rightMargin);
    }

    public void PlayLine(SubtitleEntry line)
    {
        _ = PlayLineAsync(line);
    }
    
    public async Awaitable PlayLineAsync(SubtitleEntry line)
    {
        currentToken?.Cancel();

        if (line == null)
        {
            currentToken = null;
            return;
        }

        using CancellationTokenSource source = CancellationTokenSource.CreateLinkedTokenSource(
            destroyCancellationToken
        );

        currentToken = source;

        try
        {
            await line.Play(gameObject, PlayLine, source.Token);
        }
        catch (OperationCanceledException) when (source.IsCancellationRequested)
        {
            // Expected cancellation
        } 
        finally
        {
            if (currentToken == source)
                currentToken = null;
        }
    }
}
