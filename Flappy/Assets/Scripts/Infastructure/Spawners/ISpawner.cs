public interface ISpawner
{
    bool IsSpawnProccess { get; }

    void StartSpawn();
    void Pause();
    void Continue();
    void StopSpawn();
}