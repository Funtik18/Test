using UnityEngine.SceneManagement;

public class ScenesManager : IScenesService
{
    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}