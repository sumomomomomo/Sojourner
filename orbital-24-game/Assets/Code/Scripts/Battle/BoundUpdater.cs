using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoundUpdater : MonoBehaviour
{
    [SerializeField] private BoundTargetInstructionsObject boundTargetInstructionsObject;
    [SerializeField] private FloatReference boundWidth;
    [SerializeField] private FloatReference boundHeight;
    [SerializeField] private FloatReference boundOriginXTranslation;
    [SerializeField] private FloatReference boundOriginYTranslation;
    [SerializeField] private FloatReference boundUpdateAnimationTime;
    [SerializeField] private BoolReference isFreezeTurn;
    public void UpdateBounds()
    {
        Debug.Log("Bounds updated");
        if (boundTargetInstructionsObject.PlayerBoundsTarget == null)
        {
            Debug.Log("Update bounds called but no target!");
            return;
        }
        StartCoroutine(UpdateBoundsAnimation());
    }

    private IEnumerator UpdateBoundsAnimation()
    {
        float startBw = boundWidth.Value;
        float startBh = boundHeight.Value;
        float startBOXT = boundOriginXTranslation.Value;
        float startBOYT = boundOriginYTranslation.Value;
 
        float endBw = boundTargetInstructionsObject.PlayerBoundsTarget.BoundWidth;
        float endBh = boundTargetInstructionsObject.PlayerBoundsTarget.BoundHeight;
        float endBOXT = boundTargetInstructionsObject.PlayerBoundsTarget.BoundOriginX;
        float endBOYT = boundTargetInstructionsObject.PlayerBoundsTarget.BoundOriginY;

        float buat = boundUpdateAnimationTime.Value;

        isFreezeTurn.Value = true;

        yield return CoroutineUtils.Lerp(buat, t=> {
            boundWidth.Value = Mathf.Lerp(startBw, endBw, t);
            boundHeight.Value = Mathf.Lerp(startBh, endBh, t);
            boundOriginXTranslation.Value = Mathf.Lerp(startBOXT, endBOXT, t);
            boundOriginYTranslation.Value = Mathf.Lerp(startBOYT, endBOYT, t);
        });

        isFreezeTurn.Value = false;
    }
}
