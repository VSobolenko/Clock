using ClockService.ClockTextParser;
using ClockService.SystemAgents;

namespace ClockService
{
public static class ClockServiceInstaller
{
    public static IClockService InstallBindings(ClockSettings settings)
    {
        var request = new WebRequest(settings.url);
        var scheduler = new SimpleScheduler();
        var timeParser = new YandexJsonParser();
        var clockService = new SwatchInternetTimeManager(settings, request, scheduler, timeParser);

        return clockService;
    }
}
}