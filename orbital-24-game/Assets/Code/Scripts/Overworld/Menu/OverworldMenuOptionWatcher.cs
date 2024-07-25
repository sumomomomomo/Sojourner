using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OverworldMenuOptionWatcher : MonoBehaviour
{
    [SerializeField] private OverworldMenuOption thisMenuOption;
    [SerializeField] private OverworldMenuOptionTracker overworldMenuOptionTracker;
    [SerializeField] private GameObject selectedSprite;
    void Update()
    {
        if (thisMenuOption == overworldMenuOptionTracker.CurrentOption)
        {
            selectedSprite.SetActive(true);
        }
        else
        {
            selectedSprite.SetActive(false);
        }
    }
}
