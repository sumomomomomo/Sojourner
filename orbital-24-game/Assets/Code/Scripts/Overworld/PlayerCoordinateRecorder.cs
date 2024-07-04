using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoordinateRecorder : MonoBehaviour
{
    [SerializeField] private FloatVariable x;
    [SerializeField] private FloatVariable y;
    [SerializeField] private GameObject playerObject;
    void FixedUpdate()
    {
        x.Value = playerObject.transform.position.x;
        y.Value = playerObject.transform.position.y;
    }
}
