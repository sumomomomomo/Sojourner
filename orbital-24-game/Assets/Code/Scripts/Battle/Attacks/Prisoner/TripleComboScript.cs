using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleComboScript : MonoBehaviour
{
    [SerializeField] private Animator[] fists;
    private Coroutine attackCoroutine;
    [SerializeField] private GameObject player;

    void Start()
    {
        SetFistsToIdle();
        OnEnemyAttack();
    }

    private void OnBattleStart(GameObject player)
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

    public void OnEnemyAttack()
    {
        attackCoroutine = StartCoroutine(PunchStart());
    }

    private IEnumerator PunchStart()
    {
        float waitTimeOverall = 0.25f;
        while (true)
        {
            yield return new WaitForSeconds(waitTimeOverall);
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(0.4f);
                fists[i].transform.position = player.transform.position;
                fists[i].Play("Punch");
            }
            yield return new WaitForSeconds(1.3f);

            waitTimeOverall -= 0.05f;
            waitTimeOverall = Mathf.Min(0.05f, waitTimeOverall);
        }
    }

    public void OnEnemyAttackEnd()
    {
        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);
    }
}
