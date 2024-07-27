using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleCombo : MonoBehaviour, IScriptedAttack
{
    [SerializeField] private Animator[] fists;
    private Coroutine attackCoroutine;
    [SerializeField] private GameObject player;

    [SerializeField] private float xMin = -0.74f;
    [SerializeField] private float xMax = 0.76f;
    [SerializeField] private float yMin = -1.41f;
    [SerializeField] private float yMax = 0.59f;

    void Start()
    {
        SetFistsToIdle();
        OnEnemyAttackStart();
    }

    public void OnBattleStart(GameObject player)
    {
        this.player = player;
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

    public void OnEnemyAttackStart()
    {
        attackCoroutine = StartCoroutine(PunchStart());
    }

    public void OnEnemyAttackEnd()
    {
        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);
    }

    private Vector3 FistTransformClampJitter(Vector3 pos)
    {
        return new Vector3(
            Mathf.Clamp(pos.x + UnityEngine.Random.Range(-0.5f, 0.5f), xMin, xMax),
            Mathf.Clamp(pos.y + UnityEngine.Random.Range(-0.5f, 0.5f), yMin, yMax),
            pos.z
        );
    }

    private IEnumerator PunchStart()
    {
        float waitTimeOverall = 1.25f;
        yield return new WaitForSeconds(0.25f);
        while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(0.4f);
                fists[i].transform.position = FistTransformClampJitter(player.transform.position);
                fists[i].Play("Punch");
            }
            yield return new WaitForSeconds(waitTimeOverall);

            waitTimeOverall -= 0.05f;
            waitTimeOverall = Mathf.Max(0.6f, waitTimeOverall);
        }
    }
}
