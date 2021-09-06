using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private static Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();

    public static GameObject GetObject(GameObject gameObject, bool isEnabled = true)
    {
        if (objectPool.TryGetValue(gameObject.name, out Queue<GameObject> objectList))
        {
            if (objectList.Count == 0)
                return CreateNewObject(gameObject);
            else
            {
                GameObject obj = objectList.Dequeue();
                obj.SetActive(isEnabled);
                return obj;
            }
        }
        else
            return CreateNewObject(gameObject);
    }

    public static void ReturnGameObject(GameObject gameObject)
    {
        if (objectPool.TryGetValue(gameObject.name, out Queue<GameObject> objectList))
        {
            objectList.Enqueue(gameObject);
        }
        else
        {
            Queue<GameObject> newObjectQueue = new Queue<GameObject>();
            newObjectQueue.Enqueue(gameObject);
            objectPool.Add(gameObject.name, newObjectQueue);
        }
        gameObject.transform.SetParent(null);
        gameObject.SetActive(false);
    }

    public static void DestroyGameobject(GameObject gameObject)
    {
        GameObject.DestroyImmediate(gameObject);
    }


    public static void Clear()
    {
        objectPool.Clear();
    }


    private static GameObject CreateNewObject(GameObject gameObject)
    {
        GameObject newGO = GameObject.Instantiate(gameObject);
        newGO.name = gameObject.name;
        Physics.SyncTransforms();

        Physics.autoSimulation = false;
        Physics.Simulate(Time.fixedDeltaTime);
        Physics.autoSimulation = true;

        return newGO;
    }
}