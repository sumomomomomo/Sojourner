using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundDrawer : MonoBehaviour
{
    [SerializeField] private RectTransform boundObject;
    [SerializeField] private RectTransform movableArea;

    [SerializeField] private FloatReference borderThickness;

    [SerializeField] private FloatReference boundWidth;
    [SerializeField] private FloatReference boundHeight;
    [SerializeField] private FloatReference boundOriginXTranslation;
    [SerializeField] private FloatReference boundOriginYTranslation;
    void Update()
    {
        boundObject.sizeDelta = new Vector2(boundWidth.Value, boundHeight.Value);
        boundObject.anchoredPosition = new Vector2(boundOriginXTranslation.Value, boundOriginYTranslation.Value);

        movableArea.sizeDelta = new Vector2(boundWidth.Value - borderThickness.Value * 2, boundHeight.Value - borderThickness.Value * 2);
    }
}
