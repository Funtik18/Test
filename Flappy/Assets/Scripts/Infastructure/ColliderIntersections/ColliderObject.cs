using UnityEngine;

public abstract class ColliderObject : MonoBehaviour, IColliderIntersection
{
    public virtual void Trigger(IEntity entity) { }
    public virtual void Collide(IEntity entity) { }
}