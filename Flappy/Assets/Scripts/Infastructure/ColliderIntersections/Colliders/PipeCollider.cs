using UnityEngine;

public class PipeCollider : ColliderObject
{
    [SerializeField] private BaseObstacle obstacle;

    public override void Collide(IEntity entity)
    {
        obstacle.Disable();
        entity.Die();
    }
}