using UnityEngine;

public interface IEnemyHandlerState
{
    /// <summary>
    /// Attached to listener for GameEventObject OnEnemyTurnStart.
    /// Handle behaviour to instantiate an enemy's objects here.
    /// </summary>
    /// <param name="monoBehaviour">EnemyHandler instance</param>
    abstract void OnEnemyTurnStart(MonoBehaviour monoBehaviour);

    /// <summary>
    /// Attached to listener for GameEventObject OnEnemyTurnEnd.
    /// Handle behaviour for cleaning up enemy objects here.
    /// </summary>
    /// <param name="monoBehaviour">EnemyHandler instance</param>
    abstract void OnEnemyTurnEnd(MonoBehaviour monoBehaviour);

    /// <summary>
    /// Attached to listener for GameEventObject OnBattleStart.
    /// Instantiates prefabs associated with the enemy, among other init tasks.
    /// </summary>
    /// <param name="monoBehaviour">EnemyHandler instance</param>
    /// <param name="turnHandler">TurnHandler instance</param>
    abstract void OnBattleStart(MonoBehaviour monoBehaviour, TurnHandler turnHandler);

    /// <summary>
    /// Attached to listener for GameEventObject OnPlayerWin.
    /// Handles enemy death. Destroying/hiding of objects, etc.
    /// </summary>
    /// <param name="monoBehaviour">EnemyHandler instance</param>
    abstract void OnPlayerWin(MonoBehaviour monoBehaviour);
    
    /// <summary>
    /// Attached to listener for GameEventObject OnPlayerRun.
    /// Refers to all nonstandard enemy win conditions. Eg spare, run, etc
    /// </summary>
    /// <param name="monoBehaviour">EnemyHandler instance</param>
    /// <param name="battleState">Battle State</param>
    abstract void OnPlayerRun(MonoBehaviour monoBehaviour, BattleState battleState);

    /// <summary>
    /// Attached to listener for GameEventObject OnEnemyTakeDamage.
    /// Normally handles the enemy receive damage animation.
    /// </summary>
    /// <param name="monoBehaviour">EnemyHandler instance</param>
    /// <param name="enemyHP">Enemy HP IntVariable</param>
    /// <param name="battleState">Battle State</param>
    abstract void OnTakeDamage(MonoBehaviour monoBehaviour, IntVariable enemyHP, BattleState battleState);

    /// <summary>
    /// Called by PlayerTurnHandler.
    /// Returns true if content is valid json, and values of the json are acceptable, as defined by the enemy.
    /// </summary>
    /// <param name="monoBehaviour">EnemyHandler instance</param>
    /// <param name="content">String of a json, representing a response from the LLM</param>
    /// <returns></returns>
    abstract bool CheckLLMResponse(MonoBehaviour monoBehaviour, string content);

    /// <summary>
    /// Called by PlayerTurnHandler.
    /// Handles a valid response from the LLM, such as updating current emotion, and handling
    /// speech responses from the enemy.
    /// </summary>
    /// <param name="monoBehaviour">EnemyHandler instance</param>
    /// <param name="content">String of a valid json response</param>
    abstract void HandleLLMResponse(MonoBehaviour monoBehaviour, string content);

    /// <summary>
    /// Called internally by HandleLLMResponse or otherwise.
    /// Handles enemy speech. Progresses by spacebar.
    /// </summary>
    /// <param name="monoBehaviour">EnemyHandler instance</param>
    /// <param name="content">String of enemy dialogue</param>
    /// <param name="enemyDialogueHandler">EnemyDialogueHandler specific to current enemy</param>
    void OnDisplayEnemyDialogue(MonoBehaviour monoBehaviour, string content, EnemyDialogueHandler enemyDialogueHandler)
    {
        enemyDialogueHandler.DisplayText(content);
    }

    /// <summary>
    /// Called internally.
    /// Handles enemy speech. Hides itself after a set duration.
    /// </summary>
    /// <param name="monoBehaviour">EnemyHandler instance</param>
    /// <param name="content">String of enemy dialogue</param>
    /// <param name="enemyDialogueHandler">EnemyDialogueHandler specific to current enemy</param>
    /// <param name="duration">Length of time to show the dialogue for</param>
    void OnDisplayEnemyDialogueExpire(MonoBehaviour monoBehaviour, string content, EnemyDialogueHandler enemyDialogueHandler, float duration)
    {
        enemyDialogueHandler.DisplayTextExpire(content, duration);
    }
}