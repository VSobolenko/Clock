using System;

namespace ClockService.ClockTextParser
{
public interface IDateTimeParser
{
    DateTime Parse(string time);
}
}