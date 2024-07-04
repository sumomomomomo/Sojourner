using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnSceneLoadMover : MonoBehaviour
{
    [SerializeField] private FloatReference x;
    [SerializeField] private FloatReference y;
    [SerializeField] private GameObject playerObject;
    void Start()
    {
        //Debug.Log(":" + x.Value + "," + y.Value);
        playerObject.transform.position = new Vector2(x.Value, y.Value);
    }
}
