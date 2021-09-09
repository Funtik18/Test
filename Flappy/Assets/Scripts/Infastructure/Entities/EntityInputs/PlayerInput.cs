using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : IEntityInput
{
    public event UnityAction onInput;

    private Transform transform;
    private Rigidbody2D rb;
    private Animator animator;

    private float force;
    private Quaternion start;
    private Quaternion end;
    private float rotation;

    private bool isCanControl;
    private bool isCanRotate;

    private bool IsKinematic { get => rb.isKinematic; set => rb.isKinematic = value; }

    public PlayerInput((Transform player, Rigidbody2D rb, Animator animator) mainTuple, EntitySettingsSD settingsSD)
    {
        transform = mainTuple.player;
        rb = mainTuple.rb;
        animator = mainTuple.animator;

        force = settingsSD.force;
        start = settingsSD.clockRotation ? settingsSD.forwardRotation : settingsSD.downRotation;
        end = settingsSD.clockRotation ? settingsSD.downRotation : settingsSD.forwardRotation;
        rotation = settingsSD.rotationSmooth;

        isCanControl = true;
        isCanRotate = true;
    }

    public void ReadInput()
    {
        if (isCanControl)
        {
            if (Input.GetMouseButtonDown(0))
            {
                transform.rotation = start;
                rb.velocity = Vector2.up * force;

                onInput?.Invoke();
            }
        }

        if (isCanRotate)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, end, rotation * Time.deltaTime);
        }
    }

    public void UnControl()
    {
        isCanControl = false;
        animator.enabled = false;
    }

    public void Block()
    {
        IsKinematic = true;
        isCanControl = false;
        isCanRotate = false;
    }
    public void Freeze()
    {
        IsKinematic = true;
        isCanControl = false;
        isCanRotate = false;

        rb.velocity = Vector2.zero;

        animator.enabled = false;
    }
    public void UnBlockUnFreeze()
    {
        IsKinematic = false;
        isCanControl = true;
        isCanRotate = true;

        animator.enabled = true;
    }
}