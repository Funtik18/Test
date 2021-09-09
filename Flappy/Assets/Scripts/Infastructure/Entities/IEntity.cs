using UnityEngine.Events;

public interface IEntity
{
    event UnityAction onDead;
    event UnityAction<int> onScoreChanged;

    IEntityInput Input { get; }
    bool IsDead { get; }

    void Go();
    void Die();

    void AddScore(int amount);
}