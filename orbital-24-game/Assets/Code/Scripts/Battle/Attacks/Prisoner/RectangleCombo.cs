using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Rendering;

public class RectangleCombo : MonoBehaviour, IScriptedAttack
{
    [SerializeField] private Animator[] fists;
    private Coroutine attackCoroutine;

    [SerializeField] private int rowCount;
    [SerializeField] private int colCount;
    [SerializeField] private int flashCount;
    private int[][][] fistOrder;
    void Start()
    {
        fistOrder = new int[flashCount][][];
        for (int i = 0; i < flashCount; i++)
        {
            var a = new int[rowCount][];
            for (int j = 0; j < rowCount; j++)
            {
                a[j] = new int[colCount];
            }
            fistOrder[i] = a;
        }
        SetFistsToIdle();
        OnEnemyAttackStart();
    }

    public void OnBattleStart(GameObject player)
    {
        SetFistsToIdle();
    }

    public void SetFistsToIdle()
    {
        foreach (Animator a in fists)
        {
            a.Play("PunchIdle");
            a.speed = 1;
        }
    }

    public void InitNewSequence()
    {
        for (int i = 0; i < flashCount; i++)
        {
            for (int j = 0; j < rowCount; j++)
            {
                for (int k = 0; k < colCount; k++)
                {
                    fistOrder[i][j][k] = Random.Range(0,2);
                }
                fistOrder[i][j][Random.Range(0,colCount)] = 1;
            }
        }
    }

    public void OnEnemyAttackStart()
    {
        attackCoroutine = StartCoroutine(PunchStart());
    }

    public void OnEnemyAttackEnd()
    {
        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);
    }

    private IEnumerator PunchStart()
    {
        while (true)
        {
            InitNewSequence();
            yield return new WaitForSeconds(0.25f);
            // give warnings
            for (int i = 0; i < flashCount; i++)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    for (int k = 0; k < colCount; k++)
                    {
                        if (fistOrder[i][j][k] == 1)
                            fists[j * colCount + k].Play("Punch2_WindUp");
                        else
                            fists[j * colCount + k].Play("PunchIdle");
                    }
                }

                yield return new WaitForSeconds(0.5f);
            }

            yield return new WaitForSeconds(1f);

            // punch
            for (int i = 0; i < flashCount; i++)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    for (int k = 0; k < colCount; k++)
                    {
                        if (fistOrder[i][j][k] == 1)
                            fists[j * colCount + k].Play("Punch2_Punch");
                        else
                            fists[j * colCount + k].Play("PunchIdle");
                    }
                }

                yield return new WaitForSeconds(0.5f);
            }

            yield return new WaitForSeconds(5f);
        }
    }
}
