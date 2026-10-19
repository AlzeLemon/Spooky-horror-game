using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public abstract class SubtitleEntry : ScriptableObject
{
    // Vezi exemplu basic de implementare in clasa VoiceLine

    public SubtitleEntry next;

    // Initializeaza, foloseste, si sterge obiectele necesare
    public abstract Awaitable Play(GameObject parent, Action<SubtitleEntry> callback, CancellationToken cancelationToken = default);
}
