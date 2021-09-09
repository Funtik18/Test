using UnityEngine;
using UnityEngine.Events;

using Zenject;

public abstract class Player : MonoBehaviour, IEntity
{
    public event UnityAction onDead;
    public event UnityAction<int> onScoreChanged;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    public bool IsDead { get; private set; }
    public IEntityInput Input { get; private set; }

    private IAudioService audioService;

    [Inject]
    public void Construct(IAudioService audioService, EntitySettingsSD settings)
    {
        this.audioService = audioService;

        var mainTuple = (transform, rb, animator);
        Input = new PlayerInput(mainTuple, settings);
        Input.onInput += PlayWingSound;
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
        if (IsDead) return;

        Input.UnControl();

        IsDead = true;

        PlayDieSound();

        onDead?.Invoke();
    }

    public void AddScore(int amount)
    {
        PlayPointSound();
        onScoreChanged?.Invoke(amount);
    }


    private void PlayDieSound()
    {
        audioService.PlaySound("Die");
    }
    private void PlayWingSound()
    {
        audioService.PlaySound("Wing");
    }
    private void PlayPointSound()
    {
        audioService.PlaySound("Point");
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