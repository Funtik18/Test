using System;
using UnityEngine;
using UnityEngine.Events;

using Zenject;

public interface IObstacle : IPoolable<Vector3, Vector3, IMemoryPool>, IDisposable
{
    event UnityAction<IObstacle> onSpawned;
    event UnityAction<IObstacle> onDespawned;

    void StartMove();
    void Pause();
    void Continue();

    void Enable();
    void Disable();
}