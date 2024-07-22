using System;
using System.Collections;
using System.Collections.Generic;
using OpenAI;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Battle/EnemyObject")]
public class EnemyObject : ScriptableObject
{
    [SerializeField] private string enemyName;
    public string EnemyName => enemyName;
    [SerializeField] private GameObject spritePrefab;
    public GameObject SpritePrefab => spritePrefab;
    [SerializeField] private GameObject healthBarPrefab;
    public GameObject HealthBarPrefab => healthBarPrefab;
    [SerializeField] private GameObject dialogueBoxPrefab;
    public GameObject DialogueBoxPrefab => dialogueBoxPrefab;
    [SerializeField] private int maxHP;
    public int MaxHP => maxHP;
    [SerializeField] private float currHP;
    public float CurrHP => currHP;
    [SerializeField] private int def;
    public int Def => def;
    [SerializeField] private int atk;
    public int Atk => atk;
    [SerializeField] private float agility;
    public float Agility => agility;
    [SerializeField] private int expReward;
    public int ExpReward => expReward;
    [SerializeField] private int moneyReward;
    public int MoneyReward => moneyReward;
    [SerializeField] private ChatMessageObject[] defaultChatMessageObjects;
    public ChatMessageObject[] DefaultChatMessageObjects => defaultChatMessageObjects;
    [SerializeField] private string[] availableEmotions;
    public string[] AvailableEmotions => availableEmotions;
    [SerializeField] private Sprite[] indexSensitiveSpritesForEachEmotion;
    public Sprite[] IndexSensitiveSpritesForEachEmotion => indexSensitiveSpritesForEachEmotion;
    [SerializeField] private EnemyLoadedTrackerObject enemyLoadedTrackerObject;
    [SerializeField] private bool hasEnemyLoadedTrackerObject = false;
    [SerializeField] private UnityEngine.Object _enemyHandlerState;
    public IEnemyHandlerState EnemyHandlerState => (IEnemyHandlerState) _enemyHandlerState;
    //[SerializeField] private bool isEnemyDead = false;
    [SerializeField] private BackloggedCutsceneSequenceObject onWinBackloggedCutsceneSequenceObject;
    [SerializeField] private CutsceneEventSequenceObject onWinCutsceneSequenceObject;
    [SerializeField] [TextArea] private string developerComments;

    #if UNITY_EDITOR
    public void Awake()
    {
        if (!hasEnemyLoadedTrackerObject)
        {
            string[] guids = AssetDatabase.FindAssets("t:EnemyLoadedTrackerObject");
            if (guids.Length < 1)
            {
                Debug.LogError("Cannot find EnemyLoadedTrackerObject");
            }
            enemyLoadedTrackerObject = (EnemyLoadedTrackerObject) AssetDatabase.LoadAssetAtPath(
                AssetDatabase.GUIDToAssetPath(guids[0]), typeof(EnemyLoadedTrackerObject));
            hasEnemyLoadedTrackerObject = true;
        }
    }
    #endif

    public void SetEnemyLoadedTrackerObjectToThis()
    {
        if (!hasEnemyLoadedTrackerObject)
        {
            Debug.LogError("No EnemyLoadedTrackerObject set!");
        }
        else
        {
            enemyLoadedTrackerObject.LoadEnemy(this);
        }
    }

    public void OnBattleWin()
    {
        if (onWinBackloggedCutsceneSequenceObject != null && onWinCutsceneSequenceObject != null)
        {
            onWinBackloggedCutsceneSequenceObject.LoadCutsceneEventSequence(onWinCutsceneSequenceObject);
        }
        //isEnemyDead = true;
    }

    public void SetCurrHP(float newHP)
    {
        currHP = newHP;
    }

    public Dictionary<string, Sprite> GetStringToSpriteDictionary()
    {
        Dictionary<string, Sprite> ans = new();
        Assert.IsTrue(availableEmotions.Length == indexSensitiveSpritesForEachEmotion.Length);
        for (int i = 0; i < availableEmotions.Length; i++)
        {
            ans.Add(availableEmotions[i], indexSensitiveSpritesForEachEmotion[i]); 
        }
        return ans;
    }
}
