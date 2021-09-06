using UnityEngine;

[CreateAssetMenu]
public class EntitySettingsSD : ScriptableObject
{
    public float force = 1.75f;
    public float rotationSmooth = 1.5f;
    [Space]
    public Quaternion forwardRotation = Quaternion.Euler(0, 0, 35);
    public Quaternion downRotation = Quaternion.Euler(0, 0, -90);
    public bool clockRotation = true;
}