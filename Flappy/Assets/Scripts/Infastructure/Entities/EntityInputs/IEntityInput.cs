using UnityEngine.Events;

public interface IEntityInput
{
    event UnityAction onInput;

    void ReadInput();

    void UnControl();
    void Block();
    void Freeze();
    void UnBlockUnFreeze();
}