using UnityEngine;
using UnityEngine.Events;

using Zenject;

public abstract class Player : MonoBehaviour, IEntity
{
    public UnityAction OnDead;
    public UnityAction<int> OnScoreChanged;

    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Animator animator;
    [Space]
    [SerializeField] private bool isDebug = false;

    public bool IsDead { get; private set; }
    public IEntityInput Input { get; private set; }


    [Inject]
    public void Construct(EntitySettingsSD settings)
    {
        Input = new PlayerInput(transform, rb, animator, settings);
        Input.Block();
    }

    protected virtual void Update()
    {
        Input.ReadInput();
    }

    public void Go()
    {
        Input.UnBlockUnFreeze();
    }

    public void Die()
    {
        if (isDebug || IsDead) return;

        Input.UnControl();

        IsDead = true;

        AudioManager.Instance.PlaySound("Die");

        OnDead?.Invoke();
    }

    public void AddScore(int amount)
    {
        OnScoreChanged?.Invoke(amount);
    }

   
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        collider.GetComponent<IColliderIntersection>()?.Trigger(this);
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        collision.collider.GetComponent<IColliderIntersection>()?.Collide(this);
    }
}