using System;

namespace ClockService
{
[Serializable]
public struct ClockSettings
{
    public string url;
    public bool useUtc;
    public float intervalInMilliseconds;
}
}