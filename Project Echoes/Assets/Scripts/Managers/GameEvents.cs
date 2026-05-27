using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<float> OnIlluminatingMonster;
    public static void FireOnIlluminatingMonster(float delta)
    {
        OnIlluminatingMonster?.Invoke(delta);
    }
    public static event Action<Vector3> OnNoiseEmitted;
    public static void FireOnNoiseEmiteed(Vector3 position)
    {
        OnNoiseEmitted?.Invoke(position);
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
}
