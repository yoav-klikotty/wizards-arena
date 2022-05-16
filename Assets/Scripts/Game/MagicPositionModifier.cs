using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPositionModifier : MonoBehaviour
{
    [SerializeField] Vector3 _position;
    [SerializeField] Vector3 _rotation;

    void Start()
    {
        gameObject.transform.localPosition = _position;
        gameObject.transform.localEulerAngles = _rotation;
    }
}
