﻿namespace ConferencePlanner.GraphQL.Data;

public sealed class SessionSpeaker
{
    public int SessionId { get; init; }
    public Session Session { get; init; } = null!;

    public int SpeakerId { get; init; }
    public Speaker Speaker { get; init; } = null!;
}
