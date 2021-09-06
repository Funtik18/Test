using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class BaseObstacle : MonoBehaviour, IObstacle
{
    [SerializeField] private float speed;
    [Space]
    [SerializeField] private List<Collider2D> colliders = new List<Collider2D>();

    private Coroutine movementCoroutine = null;
    public bool IsMovementProccess => movementCoroutine != null;

    private bool isPaused = false;

    public void StartMove()
    {
        if (!IsMovementProccess)
        {
            isPaused = false;
            Enable();
            movementCoroutine = StartCoroutine(Movement());
        }
    }
    public void Pause()
    {
        isPaused = true;
    }
    public void Continue()
    {
        isPaused = false;
    }
    private IEnumerator Movement()
    {
        while (true)
        {
            while (isPaused)
            {
                yield return null;
            }

            transform.Translate(-speed * Time.deltaTime, 0, 0);

            yield return null;
        }
    }
    public void StopMove()
    {
        if (IsMovementProccess)
        {
            StopCoroutine(movementCoroutine);
            movementCoroutine = null;
        }
    }

    public void Enable()
    {
        EnableColliders(true);
    }
    public void Disable()
    {
        EnableColliders(false);
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
}