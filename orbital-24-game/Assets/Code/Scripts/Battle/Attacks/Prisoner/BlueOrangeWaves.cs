using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class BlueOrangeWaves : MonoBehaviour, IScriptedAttack
{
    [SerializeField] private Wave[] blueWaves;
    [SerializeField] private Wave[] orangeWaves;
    [SerializeField] private float minX = -4.05f;
    [SerializeField] private float maxX = 3.37f;
    [SerializeField] private float y = 0.22f;
    private Coroutine attackCoroutine;
    //private int blueWaveIndex = 0;
    //private int orangeWaveIndex = 0;

    void Start()
    {
        OnBattleStart(this.gameObject);
        OnEnemyAttackStart();
    }

    public void OnBattleStart(GameObject _)
    {
        //blueWaveIndex = 0;
        //orangeWaveIndex = 0;
        MoveAllToX(minX);
        SetActiveAllWaves(false);
    }

    public void OnEnemyAttackEnd()
    {
        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);
        StopCoroutineAllWaves();
        MoveAllToX(minX);
        SetActiveAllWaves(false);
    }

    public void OnEnemyAttackStart()
    {
        SetActiveAllWaves(true);
        attackCoroutine = StartCoroutine(AttackEnum());
    }

    private IEnumerator AttackEnum()
    {
        while (true)
        {
            blueWaves[0].MoveFromAXToBX(minX, maxX, 3);
            yield return new WaitForSeconds(1);
            blueWaves[1].MoveFromAXToBX(minX, maxX, 3);
            yield return new WaitForSeconds(1);
            orangeWaves[0].MoveFromAXToBX(minX, maxX, 2);
            yield return new WaitForSeconds(2.5f);
        }
    }

    private void MoveAllToX(float x)
    {
        foreach (Wave bw in blueWaves)
        {
            bw.transform.position = new Vector3(x, y, 0);
        }
        foreach (Wave ow in orangeWaves)
        {
            ow.transform.position = new Vector3(x, y, 0);
        }
    }

    private void SetActiveAllWaves(bool b)
    {
        foreach (Wave bw in blueWaves)
        {
            bw.gameObject.SetActive(b);
        }
        foreach (Wave ow in orangeWaves)
        {
            ow.gameObject.SetActive(b);
        }
    }

    private void StopCoroutineAllWaves()
    {
        foreach (Wave bw in blueWaves)
        {
            bw.StopCoroutineThisWave();
        }
        foreach (Wave ow in orangeWaves)
        {
            ow.StopCoroutineThisWave();
        }
    }
}
