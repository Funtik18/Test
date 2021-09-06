using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    private static ScenesManager instance;
    public static ScenesManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ScenesManager>();

                if (instance == null)
                {
                    instance = new GameObject("_ScenesManager").AddComponent<ScenesManager>();
                }

                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }

    public void ReloadCurrentScene()
    {
        ObjectPool.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}