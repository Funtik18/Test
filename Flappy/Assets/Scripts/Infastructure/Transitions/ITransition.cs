using UnityEngine.Events;

public interface ITransition 
{
    event UnityAction onIn;
    event UnityAction onOut;

    bool IsTransitionProccess { get; }

    void TransitionIn();
    void TransitionOut();
}
