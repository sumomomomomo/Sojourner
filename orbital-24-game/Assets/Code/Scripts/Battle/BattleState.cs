using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle State")]
public class BattleState : ScriptableObject
{
    [SerializeField] private FloatVariable playerDamageAnimationLength;
    public float PlayerDamageAnimationLength => playerDamageAnimationLength.Value;

    [SerializeField] private BoolVariable isPlayerTurn;
    [SerializeField] private BoolVariable isFreezeTurn;
    [SerializeField] private BoolVariable isBattleLose;
    [SerializeField] private BoolVariable isBattleWin;
    [SerializeField] private BoolVariable isPlayerDefending;
    [SerializeField] private BoolVariable isPlayerInvulnerable;
    [SerializeField] private BoolVariable isChangeTurnExecuting;
    [SerializeField] private BoolVariable isEnemyDamageAnimationPlaying;

    public void ResetAllFlags()
    {
        isFreezeTurn.Value = false;
        isBattleLose.Value = false;
        isBattleWin.Value = false;
        isPlayerDefending.Value = false;
        isPlayerInvulnerable.Value = false;
        isChangeTurnExecuting.Value = false;
        isEnemyDamageAnimationPlaying.Value = false;
    }

    public void SetToPlayerTurn()
    {
        isPlayerTurn.Value = true;
    }
    public bool CanStartPlayerTurn()
    {
        return !isFreezeTurn.Value && !isBattleLose.Value && !isBattleWin.Value;
    }
    public bool CanStartEnemyTurn()
    {
        return !isFreezeTurn.Value && !isBattleLose.Value && !isBattleWin.Value && !isEnemyDamageAnimationPlaying.Value;
    }

    public bool IsTurnHandlerActive()
    {
        return !isFreezeTurn.Value && !isBattleLose.Value && !isBattleWin.Value && !isChangeTurnExecuting.Value;
    }

    public bool IsPlayerTurn()
    {
        return isPlayerTurn.Value;
    }

    public bool IsPlayerHidden()
    {
        return isFreezeTurn.Value || isBattleLose.Value || isBattleWin.Value;
    }

    public bool IsPlayerStrategySelectable()
    {
        return isPlayerTurn.Value && !isFreezeTurn.Value;
    }

    public bool IsPlayerDefending()
    {
        return isPlayerDefending.Value;
    }

    public bool IsPlayerInvulnerable()
    {
        return isPlayerInvulnerable.Value;
    }

    public bool IsPlayerUnmovable()
    {
        return isFreezeTurn.Value || isPlayerTurn.Value;
    }

    public bool IsEnemyDamageAnimationPlaying()
    {
        return isEnemyDamageAnimationPlaying.Value;
    }

    public void FlipIsPlayerTurn()
    {
        isPlayerTurn.Value = !isPlayerTurn.Value;
    }

    public void SetBattleLose(bool b)
    {
        isBattleLose.Value = b;
    }

    public void SetBattleWin(bool b)
    {
        isBattleWin.Value = b;
    }

    public void SetPlayerInvulnerable(bool b)
    {
        isPlayerInvulnerable.Value = b;
    }

    public void SetPlayerDefending(bool b)
    {
        isPlayerDefending.Value = b;
    }

    public void SetFreezeTurn(bool b)
    {
        isFreezeTurn.Value = b;
    }

    public void SetEnemyDamageAnimationPlaying(bool b)
    {
        isEnemyDamageAnimationPlaying.Value = b;
    }

    public void OnStartChangeTurn()
    {
        isChangeTurnExecuting.Value = true;
    }

    public void OnEndChangeTurn()
    {
        isChangeTurnExecuting.Value = false;
    }

}
