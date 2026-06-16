using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<float, Transform> OnIlluminatingMonster;
    public static void FireOnIlluminatingMonster(float delta, Transform playerTransform)
    {
        OnIlluminatingMonster?.Invoke(delta, playerTransform);
    }
    public static event Action<Vector3, Transform, float> OnNoiseEmitted;
    public static void FireOnNoiseEmiteed(Vector3 position, Transform source, float volumeRange)
    {
        OnNoiseEmitted?.Invoke(position, source, volumeRange);
    }
    public static event Action OnPlayerCaught;
    public static void FireOnPlayerCaught()
    {
        OnPlayerCaught?.Invoke();
    }
    public static event Action OnGameOver;
    public static void FireOnGameOver()
    {
        OnGameOver?.Invoke();
    }
    public static event Action OnTerminalActivated;
    public static void FireOnTerminalActivated()
    {
        OnTerminalActivated?.Invoke();
    }
}
