using UnityEngine;

public class GroundCollider : ColliderObject
{
    [SerializeField] private Animator animator;

    public void EnableAnimator(bool trigger)
    {
        animator.enabled = trigger;
    }

    public override void Collide(IEntity entity)
    {
        entity.Die();
        entity.Input.Freeze();
    }
}