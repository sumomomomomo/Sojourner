using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Overworld Menu/Menu Option")]
public class OverworldMenuOption : ScriptableObject
{
    [SerializeField] private OverworldMenuOption prevOption;
    [SerializeField] private OverworldMenuOption nextOption;
    [SerializeField] private UnityEvent onSelectOption;
    public UnityEvent OnSelectOption => onSelectOption;

    public OverworldMenuOption GetNextOption()
    {
        return nextOption == null ? this : nextOption;
    }

    public OverworldMenuOption GetPreviousOption()
    {
        return prevOption == null ? this : prevOption;
    }
}
