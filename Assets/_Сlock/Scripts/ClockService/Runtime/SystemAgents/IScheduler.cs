using System;
using System.Timers;

namespace ClockService.SystemAgents
{
public interface IScheduler : IDisposable
{
    void AddScheduler(float interval, ElapsedEventHandler sender, bool repeat);
}
}