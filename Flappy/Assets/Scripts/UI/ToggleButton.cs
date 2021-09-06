using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private Image icon;

    [SerializeField] private bool isInverse = false;

    public ColorBlock selected;

    private ColorBlock normal;

    private void Awake()
    {
        toggle.onValueChanged.AddListener(ValueChanged);
        normal = toggle.colors;

        ValueChanged(toggle.isOn);
    }

    private void ValueChanged(bool trigger)
    {
        toggle.colors = trigger ? (isInverse ? normal : selected) : (isInverse ? selected : normal);
    }

    [ContextMenu("Copy")]
    private void Copy()
    {
        if (toggle == null) return;

        selected = toggle.colors;
    }
}