using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnSceneLoadMover : MonoBehaviour
{
    [SerializeField] private FloatVariableNonSerialized x;
    [SerializeField] private FloatVariableNonSerialized y;
    [SerializeField] private GameObject playerObject;
    void Start()
    {
        //Debug.Log(":" + x.Value + "," + y.Value);
        playerObject.transform.position = new Vector2
        (
            PlayerPrefs.GetFloat("PlayerXCoordinate"),
            PlayerPrefs.GetFloat("PlayerYCoordinate")
        );
    }
}
