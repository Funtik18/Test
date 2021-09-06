using UnityEngine;

public class PlayerInput : IEntityInput
{
    public bool IsCanControl { get; set; }
    public bool IsCanRotate { get; set; }

    public bool IsKinematic { get => rb.isKinematic; set => rb.isKinematic = value; }

    private Transform transform;
    private Rigidbody2D rb;
    private Animator animator;

    private float force;
    private Quaternion start;
    private Quaternion end;
    private float rotation;

    public PlayerInput(Transform player, Rigidbody2D playerRB, Animator anim, EntitySettingsSD settingsSD)
    {
        transform = player;
        rb = playerRB;
        animator = anim;

        force = settingsSD.force;
        start = settingsSD.clockRotation ? settingsSD.forwardRotation : settingsSD.downRotation;
        end = settingsSD.clockRotation ? settingsSD.downRotation : settingsSD.forwardRotation;
        rotation = settingsSD.rotationSmooth;
    }

    public void ReadInput()
    {
        if (IsCanControl)
        {
            if (Input.GetMouseButtonDown(0))
            {
                transform.rotation = start;
                rb.velocity = Vector2.up * force;

                AudioManager.Instance.PlaySound("Wing");
            }
        }

        if (IsCanRotate)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, end, rotation * Time.deltaTime);
        }
    }

    public void UnControl()
    {
        IsCanControl = false;
        animator.enabled = false;
    }

    public void Block()
    {
        IsKinematic = true;
        IsCanControl = false;
        IsCanRotate = false;
    }
    public void Freeze()
    {
        IsKinematic = true;
        IsCanControl = false;
        IsCanRotate = false;

        rb.velocity = Vector2.zero;

        animator.enabled = false;
    }
    public void UnBlockUnFreeze()
    {
        IsKinematic = false;
        IsCanControl = true;
        IsCanRotate = true;

        animator.enabled = true;
    }
}