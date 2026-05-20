using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<float> OnIlluminatingMonster;
    public static event Action<Vector3> OnNoiseEmitted;
    public static event Action OnPlayerCaught;
    public static event Action OnGameOver;
}
