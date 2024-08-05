using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoordinateRecorder : MonoBehaviour
{
    // [SerializeField] private FloatVariableNonSerialized x;
    // [SerializeField] private FloatVariableNonSerialized y;
    [SerializeField] private GameObject playerObject;
    void Update()
    {
        // x.Value = playerObject.transform.position.x;
        // y.Value = playerObject.transform.position.y;
        PlayerPrefs.SetFloat("PlayerXCoordinate", playerObject.transform.position.x);
        PlayerPrefs.SetFloat("PlayerYCoordinate", playerObject.transform.position.y);
    }
}
