using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Overworld Menu/Menu Option Tracker")]
public class OverworldMenuOptionTracker : ScriptableObject
{
    [SerializeField] private OverworldMenuOption defaultOption;
    [SerializeField] private OverworldMenuOption currentOption;
    public OverworldMenuOption CurrentOption => currentOption;

    public void InitDefaultOption()
    {
        currentOption = defaultOption;
    }
    public UnityEvent SelectCurentOption()
    {
        return currentOption.OnSelectOption;
    }

    public void ToNextOption()
    {
        currentOption = currentOption.GetNextOption();
    }

    public void ToPreviousOption()
    {
        currentOption = currentOption.GetPreviousOption();
    }
}
