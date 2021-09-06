public interface IObstacle
{
    bool IsMovementProccess { get; }

    void StartMove();
    void Pause();
    void Continue();
    void StopMove();

    void Enable();
    void Disable();
}