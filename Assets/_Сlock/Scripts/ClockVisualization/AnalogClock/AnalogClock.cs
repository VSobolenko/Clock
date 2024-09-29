using UnityEngine;

namespace Сlock.Analog
{
public class AnalogClock : MonoBehaviour
{
    [SerializeField] private ClockServiceProvider _clockService;
    [SerializeField] private ClockHand _hourHand;
    [SerializeField] private ClockHand _minuteHand;
    [SerializeField] private ClockHand _secondHand;

    private void Update()
    {
        var time = _clockService.ClockService.ActiveTime;
        var hoursSeconds = time.Hour * 3600;
        var minuteSeconds = time.Minute * 60;
        var seconds = time.Second;
        _hourHand.Rotate(hoursSeconds + minuteSeconds + seconds);
        _minuteHand.Rotate(minuteSeconds + seconds);
        _secondHand.Rotate(seconds + ClockHand.DelayPredicate);
    }
}
}