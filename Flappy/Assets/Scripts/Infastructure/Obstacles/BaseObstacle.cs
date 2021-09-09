using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;

using Zenject;

public class BaseObstacle : MonoBehaviour, IObstacle
{
    public event UnityAction<IObstacle> onSpawned;
    public event UnityAction<IObstacle> onDespawned;


    [SerializeField] private float speed;
    [Space]
    [SerializeField] private List<Collider2D> colliders = new List<Collider2D>();

    private bool isPaused = false;

    private float endXPosition;

    private IMemoryPool pool;

    private void Update()
    {
        if(transform.position.x <= endXPosition)
        {
            Dispose();
        }

        if (!isPaused)
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
    }

    public void StartMove()
    {
        isPaused = false;
        Enable();
    }
    public void Pause()
    {
        isPaused = true;
    }
    public void Continue()
    {
        isPaused = false;
    }

    public void Enable()
    {
        EnableColliders(true);
    }
    public void Disable()
    {
        EnableColliders(false);
    }

    public void OnSpawned(Vector3 startPosition, Vector3 endPosition, IMemoryPool pool)
    {
        this.pool = pool;

        transform.position = startPosition;
        endXPosition = endPosition.x;

        onSpawned?.Invoke(this);
    }
    public void OnDespawned()
    {
        pool = null;

        onDespawned?.Invoke(this);
    }

    public void Dispose()
    {
        Pause();
        pool.Despawn(this);
    }

    private void EnableColliders(bool trigger)
    {
        for (int i = 0; i < colliders.Count; i++)
        {
            colliders[i].enabled = trigger;
        }
    }

    [ContextMenu("FillList")]
    private void FillList()
    {
        colliders = GetComponentsInChildren<Collider2D>().ToList();
    }

    public class ZenjectFactory : PlaceholderFactory<Vector3, Vector3, BaseObstacle> { }
}