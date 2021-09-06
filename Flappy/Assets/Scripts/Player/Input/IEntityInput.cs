public interface IEntityInput
{
    bool IsCanControl { get; set; }
    bool IsCanRotate { get; set; }
    bool IsKinematic { get; set; }

    void ReadInput();

    void UnControl();
    void Block();
    void Freeze();
    void UnBlockUnFreeze();
}