using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;

public class BaseSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] private Vector2 spawnPosition;
    [SerializeField] private Vector2 destructPosition;

    [SerializeField] private float heightMin, heightMax;
    [SerializeField] private float timeBtw = 2f;


    private bool isPaused = false;

    private Coroutine spawnCoroutine = null;
    public bool IsSpawnProccess => spawnCoroutine != null;

    private WaitForSeconds seconds;


    private List<IObstacle> obstacles = new List<IObstacle>();

    private BaseObstacle.ZenjectFactory factory;

    [Inject]
    public void Construct(BaseObstacle.ZenjectFactory factory)
    {
        this.factory = factory;
    }

    public void StartSpawn()
    {
        if (!IsSpawnProccess)
        {
            seconds = new WaitForSeconds(timeBtw);
            spawnCoroutine = StartCoroutine(Spawner());
        }
    }
    private IEnumerator Spawner()
    {
        while (true)
        {
            while (isPaused)
            {
                yield return null;

                if (isPaused == false)
                {
                    yield return seconds;
                }
            }

            Vector2 pos = spawnPosition + new Vector2(0, Random.Range(heightMin, heightMax));

            IObstacle obstacle = factory.Create(pos, destructPosition);
            obstacle.onDespawned += RemoveObstacle;
            obstacles.Add(obstacle);
            obstacle.StartMove();

            yield return seconds;
        }
    }
    public void Pause()
    {
        PauseSpawner(true);
    }
    public void Continue()
    {

        PauseSpawner(false);
    }
    public void StopSpawn()
    {
        if (IsSpawnProccess)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;

            PauseSpawner(true);

            obstacles.Clear();
        }
    }

    private void RemoveObstacle(IObstacle obstacle)
    {
        if (obstacles.Contains(obstacle))
        {
            obstacles.Remove(obstacle);

            obstacle.onDespawned -= RemoveObstacle;
        }
    }

    private void PauseSpawner(bool trigger)
    {
        isPaused = trigger;

        if (trigger)
        {
            for (int i = 0; i < obstacles.Count; i++)
            {
                obstacles[i].Pause();
            }
        }
        else
        {
            for (int i = 0; i < obstacles.Count; i++)
            {
                obstacles[i].Continue();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(destructPosition, 0.05f);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(spawnPosition, 0.05f);
    }
}