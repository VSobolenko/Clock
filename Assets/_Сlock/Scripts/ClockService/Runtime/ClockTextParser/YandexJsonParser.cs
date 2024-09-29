using System;
using Newtonsoft.Json.Linq;

namespace ClockService.ClockTextParser
{
public class YandexJsonParser : IDateTimeParser
{
    public DateTime Parse(string json)
    {
        var unixTime = GetUnixTimestampValueFromJson(json);
        var time = UnixTimestampToDateTime(unixTime);

        return time;
    }

    private static long GetUnixTimestampValueFromJson(string text)
    {
        var jObject = JObject.Parse(text);
        var time = jObject["time"]!.Value<long>();

        return time;
    }

    private static DateTime UnixTimestampToDateTime(long unixTime) =>
        DateTimeOffset.FromUnixTimeMilliseconds(unixTime).DateTime;
}
}