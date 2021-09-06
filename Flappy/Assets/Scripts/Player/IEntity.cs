public interface IEntity
{
    IEntityInput Input { get; }
    bool IsDead { get; }

    void Go();
    void Die();

    void AddScore(int amount);
}