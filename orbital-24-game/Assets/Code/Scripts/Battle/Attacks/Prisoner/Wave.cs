using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Wave : MonoBehaviour
{
    private Coroutine coroutine;
    public void MoveFromAXToBX(float aX, float bX, float duration)
    {
        StopCoroutineThisWave();
        coroutine = StartCoroutine(Move(aX, bX, duration));
    }

    private IEnumerator Move(float aX, float bX, float duration)
    {
        float y = this.transform.position.y;
        float z = this.transform.position.z;
        yield return CoroutineUtils.Lerp(duration, t => {
            this.transform.position = new Vector3(
                Mathf.Lerp(aX, bX, t),
                y,
                z
            );
        });
    }

    public void StopCoroutineThisWave()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
    }
}
