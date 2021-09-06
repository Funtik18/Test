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

    private BaseObstacle obstaclePrefab;

    private Coroutine spawnCoroutine = null;
    public bool IsSpawnProccess => spawnCoroutine != null;

    private List<BaseObstacle> obstacles = new List<BaseObstacle>();

    private WaitForSeconds seconds;

    private bool isPaused = false;

    [Inject]
    public void Construct(BaseObstacle obstacle)
    {
        obstaclePrefab = obstacle;
    }

    public void StartSpawn()
    {
        if (!IsSpawnProccess)
        {
            seconds = new WaitForSeconds(timeBtw);
            spawnCoroutine = StartCoroutine(Spawner());
        }
    }

    public void Pause()
    {
        isPaused = true;

        for (int i = 0; i < obstacles.Count; i++)
        {
            obstacles[i].Pause();
        }
    }
    public void Continue()
    {
        isPaused = false;

        for (int i = 0; i < obstacles.Count; i++)
        {
            obstacles[i].Continue();
        }
    }
    private IEnumerator Spawner()
    {
        while (true)
        {
            while (isPaused)//error
            {
                yield return null;

                if(isPaused == false)
                {
                    yield return seconds;
                }
            }

            Vector2 pos = spawnPosition + new Vector2(0, Random.Range(heightMin, heightMax));

            GameObject obj = ObjectPool.GetObject(obstaclePrefab.gameObject);
            obj.transform.position = pos;
            obj.transform.rotation = Quaternion.identity;

            BaseObstacle obstacle = obj.GetComponent<BaseObstacle>();
            obstacles.Add(obstacle);
            obstacle.StartMove();

            CheckObstacles();

            yield return seconds;
        }
    }
    public void StopSpawn()
    {
        if (IsSpawnProccess)
        {
            StopCoroutine(spawnCoroutine);

            for (int i = 0; i < obstacles.Count; i++)
            {
                obstacles[i].StopMove();
            }
            //need clear
        }
    }

    private void CheckObstacles()
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            if (obstacles[i].transform.position.x <= destructPosition.x)
            {
                obstacles[i].StopMove();
                ObjectPool.ReturnGameObject(obstacles[i].gameObject);

                if (obstacles.Contains(obstacles[i]))
                {
                    obstacles.Remove(obstacles[i]);
                }
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