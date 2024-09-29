using System.Collections.Generic;
using System.Timers;

namespace ClockService.SystemAgents
{
internal class SimpleScheduler : IScheduler
{
    private readonly List<Scheduler> _schedulers = new();

    public void AddScheduler(float interval, ElapsedEventHandler sender, bool repeat)
    {
        var timer = new Timer(interval)
        {
            AutoReset = repeat,
        };
        timer.Elapsed += sender;
        timer.Start();

        var scheduler = new Scheduler
        {
            timer = timer,
            sender = sender,
        };

        _schedulers.Add(scheduler);
    }

    public void Dispose()
    {
        foreach (var scheduler in _schedulers) scheduler.timer.Stop();
    }

    private class Scheduler
    {
        public ElapsedEventHandler sender;
        public Timer timer;
    }
}
}