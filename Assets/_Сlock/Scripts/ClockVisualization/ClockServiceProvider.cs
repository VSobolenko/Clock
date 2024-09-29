using ClockService;
using UnityEngine;

namespace Сlock
{
public class ClockServiceProvider : MonoBehaviour
{
    [SerializeField] private ClockSettings _settings;

    public IClockService ClockService { get; private set; }

    private void Awake()
    {
        ClockService = ClockServiceInstaller.InstallBindings(_settings);
    }

    private void OnDestroy()
    {
        ClockService.Dispose();
    }
}
}