using System;
using System.Diagnostics;
using ClockService.ClockTextParser;
using ClockService.SystemAgents;

namespace ClockService
{
internal class SwatchInternetTimeManager : IClockService
{
    private readonly ClockSettings _clockSettings;
    private readonly IDateTimeParser _dateTimeParser;
    private readonly Stopwatch _requestTime;
    private readonly IScheduler _scheduler;
    private readonly IWebRequest _webRequest;
    private DateTimeSnapshot _snapshot;

    public SwatchInternetTimeManager(ClockSettings clockSettings, IWebRequest webRequest, IScheduler scheduler,
                                     IDateTimeParser dateTimeParser)
    {
        _clockSettings = clockSettings;
        _webRequest = webRequest;
        _scheduler = scheduler;
        _dateTimeParser = dateTimeParser;
        _requestTime = new Stopwatch();
        SetupClockScheduler();
        UpdateInternetTime();
    }

    public DateTime ActiveTime => GetActiveTime();

    public void Dispose()
    {
        _scheduler?.Dispose();
    }

    private void SetupClockScheduler()
    {
        var interval = _clockSettings.intervalInMilliseconds;
        _scheduler.AddScheduler(interval,
                                (sender, args) => { UpdateInternetTime(); },
                                true);
    }

    private async void UpdateInternetTime()
    {
        StartRequestTimer();
        var json = await _webRequest.RequestJsonTime();
        var requestTime = StopRequestTimer();
        var time = _dateTimeParser.Parse(json);
        time = time.Add(requestTime);
        time = ConvertToLocalTimeIfNotUtc(time);
        UpdateSnapshot(time);
    }

    private void StartRequestTimer() => _requestTime.Start();

    private TimeSpan StopRequestTimer()
    {
        _requestTime.Stop();
        _requestTime.Reset();

        return _requestTime.Elapsed;
    }

    private void UpdateSnapshot(DateTime time)
    {
        var updatedTime = DateTime.Now;
        _snapshot = new DateTimeSnapshot
        {
            what = time,
            when = updatedTime,
        };
    }

    private DateTime ConvertToLocalTimeIfNotUtc(DateTime time) => _clockSettings.useUtc ? time : time.ToLocalTime();

    private DateTime GetActiveTime()
    {
        var activeTime = DateTime.Now;
        var timeSpan = activeTime - _snapshot.when;

        return _snapshot.what.Add(timeSpan);
    }

    private struct DateTimeSnapshot
    {
        public DateTime what;
        public DateTime when;
    }
}
}