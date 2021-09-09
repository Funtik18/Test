public class SkyCollider : ColliderObject
{
    public override void Collide(IEntity entity)
    {
        entity.Die();
    }
}
