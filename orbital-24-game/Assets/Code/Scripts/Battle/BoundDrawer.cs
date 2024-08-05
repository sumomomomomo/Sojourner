using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class BoundDrawer : MonoBehaviour
{
    [SerializeField] private GameObject playerBoundsGroup;
    [SerializeField] private GameObject topWall;
    [SerializeField] private GameObject bottomWall;
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject blackBase;
    [SerializeField] private FloatReference boundThickness;
    [SerializeField] private FloatReference boundWidth;
    [SerializeField] private FloatReference boundHeight;
    [SerializeField] private FloatReference boundOriginXTranslation;
    [SerializeField] private FloatReference boundOriginYTranslation;

    [SerializeField] private PlayerBoundsTarget playerBoundsTargetEditor;
    void Update()
    {
        if (playerBoundsTargetEditor != null)
        {
            boundWidth.Value = playerBoundsTargetEditor.BoundWidth;
            boundHeight.Value = playerBoundsTargetEditor.BoundHeight;
            boundOriginXTranslation.Value = playerBoundsTargetEditor.BoundOriginX;
            boundOriginYTranslation.Value = playerBoundsTargetEditor.BoundOriginY;
        }
        playerBoundsGroup.transform.position = new Vector3(boundOriginXTranslation.Value, boundOriginYTranslation.Value, -1);

        // update width
        leftWall.transform.localPosition = new Vector3(-(boundWidth.Value / 2), 0, 0);
        rightWall.transform.localPosition = new Vector3(boundWidth.Value / 2, 0, 0);
        topWall.transform.localScale = new Vector3(boundWidth.Value + boundThickness.Value, boundThickness.Value, 1);
        bottomWall.transform.localScale = new Vector3(boundWidth.Value + boundThickness.Value, boundThickness.Value, 1);

        // update height
        topWall.transform.localPosition = new Vector3(0, boundHeight.Value / 2, 0);
        bottomWall.transform.localPosition = new Vector3(0, -(boundHeight.Value / 2), 0);
        leftWall.transform.localScale = new Vector3(boundThickness.Value, boundHeight.Value + boundThickness.Value, 1);
        rightWall.transform.localScale = new Vector3(boundThickness.Value, boundHeight.Value + boundThickness.Value, 1);

        // update base
        blackBase.transform.localScale = new Vector3(boundWidth.Value, boundHeight.Value, 0);
    }
}