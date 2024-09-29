using TMPro;
using UnityEngine;

namespace Сlock.Digital
{
public class DigitalClock : MonoBehaviour
{
    [SerializeField] private ClockServiceProvider _clockService;
    [SerializeField] private TextMeshProUGUI _text;

    private void Update()
    {
        _text.text = $"{_clockService.ClockService.ActiveTime:HH:mm:ss}";
    }
}
}