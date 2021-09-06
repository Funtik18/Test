using UnityEngine;

public class PipeCollider : ColliderObject
{
    [SerializeField] private BaseObstacle pipes;
    public override void Collide(IEntity entity)
    {
        pipes.Disable();
        entity.Die();
    }
}