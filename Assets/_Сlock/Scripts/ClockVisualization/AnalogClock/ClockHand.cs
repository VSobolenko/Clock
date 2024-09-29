using DG.Tweening;
using UnityEngine;

namespace Сlock.Analog
{
public class ClockHand : MonoBehaviour
{
    [SerializeField] private Discreteness _discreteness;
    private int _cacheValueFloat = -1;
    
    public const int DelayPredicate = 1;

    public void Rotate(int value) => RotateInternal(value);

    private void RotateInternal(int value)
    {
        var targetRotation = GetTargetAngleDegrees(value);
        if (_cacheValueFloat != value)
            transform.DORotate(targetRotation, DelayPredicate).SetEase(Ease.Linear);
        _cacheValueFloat = value;
    }

    private Vector3 GetTargetAngleDegrees(int value)
    {
        var maxValue = (int) _discreteness;
        var angle = value * 360f / maxValue;
        var rotation = transform.rotation.eulerAngles;

        return new Vector3(rotation.x, rotation.y, -angle);
    }
}
}