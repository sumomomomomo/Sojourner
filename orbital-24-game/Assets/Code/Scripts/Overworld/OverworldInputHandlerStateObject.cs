using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Overworld Input Handler State")]
public class OverworldInputHandlerStateObject : ScriptableObject
{
    [SerializeField] private BoolVariable isDialogueBoxOpen;
    [SerializeField] private BoolVariable isInventoryOpen;
    [SerializeField] private BoolVariable isOverworldMenuOpen;
    [SerializeField] private BoolVariable isCutscenePlaying;

    public bool CanPlayerInteract()
    {
        return !isCutscenePlaying.Value && !isDialogueBoxOpen.Value && !isOverworldMenuOpen.Value;
    }

    public bool CanPlayerMove()
    {
        return !isDialogueBoxOpen.Value && !isCutscenePlaying.Value && !isOverworldMenuOpen.Value;
    }

    public bool CanOverworldMenuOpen()
    {
        return !isCutscenePlaying.Value && !isDialogueBoxOpen.Value;
    }

    public bool CanOverworldMenuClose()
    {
        return !isInventoryOpen.Value;
    }
}
