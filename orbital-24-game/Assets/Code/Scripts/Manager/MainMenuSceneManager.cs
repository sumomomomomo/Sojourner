using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuSceneManager : MonoBehaviour
{
    [SerializeField] private IntReference playerHP;
    [SerializeField] private IntReference playerBaseMaxHP;
    [SerializeField] private FloatVariable playerCoordinatesX;
    [SerializeField] private FloatVariable playerCoordinatesY;
    [SerializeField] private float startingPlayerCoordinatesX;
    [SerializeField] private float startingPlayerCoordinatesY;
    [SerializeField] private BackloggedCutsceneSequenceObject backloggedCutsceneSequenceObject;
    [SerializeField] private CutsceneEventSequenceObject newGameCutscene;
    [SerializeField] private string newGameSceneName;
    public void NewGame()
    {
        // Init player health
        playerHP.Value = playerBaseMaxHP.Value;

        // Init player coordinates
        playerCoordinatesX.Value = startingPlayerCoordinatesX;
        playerCoordinatesY.Value = startingPlayerCoordinatesY;

        // Reset all cutscenes
        string[] guids = AssetDatabase.FindAssets("t:CutsceneEventSequenceObject");
        if (guids.Length < 1)
        {
            Debug.LogError("Cannot find any cutscenes");
        }
        foreach (string s in guids)
        {
            CutsceneEventSequenceObject c = (CutsceneEventSequenceObject) AssetDatabase.LoadAssetAtPath(
                AssetDatabase.GUIDToAssetPath(s), typeof(CutsceneEventSequenceObject));
            c.SetFinished(false);
        }

        // Set backlog to new game scene
        backloggedCutsceneSequenceObject.LoadCutsceneEventSequence(newGameCutscene);

        SceneManager.LoadSceneAsync(newGameSceneName);
    }
}
