using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundDrawer : MonoBehaviour
{
    [SerializeField] private GameObject topWall;
    [SerializeField] private GameObject bottomWall;
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private FloatReference boundThickness;
    [SerializeField] private FloatReference boundWidth;
    [SerializeField] private FloatReference boundHeight;
    [SerializeField] private FloatReference boundOriginXTranslation;
    [SerializeField] private FloatReference boundOriginYTranslation;
    void Update()
    {
        this.transform.position = new Vector3(boundOriginXTranslation.Value, boundOriginYTranslation.Value, -1);

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

        // update thickness
        // change x scale of left,right walls, y scale of top,bottom walls

        // boundObject.sizeDelta = new Vector2(boundWidth.Value, boundHeight.Value);
        // boundObject.anchoredPosition = new Vector2(boundOriginXTranslation.Value, boundOriginYTranslation.Value);

        // movableArea.sizeDelta = new Vector2(boundWidth.Value - borderThickness.Value * 2, boundHeight.Value - borderThickness.Value * 2);
    }
}
