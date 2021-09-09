using UnityEngine;

public class ScoreCollider : ColliderObject
{
    [SerializeField] private Collider2D coll;

    public override void Trigger(IEntity entity)
    {
        coll.enabled = false;
        entity.AddScore(1);
    }
}