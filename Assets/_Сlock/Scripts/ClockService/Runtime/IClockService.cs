using System;

namespace ClockService
{
public interface IClockService : IDisposable
{
    DateTime ActiveTime { get; }
}
}