using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObject : MonoBehaviour, IPickable
{
    FixedJoint _joint;
    Rigidbody _objectRB;

    void Start()
    {
        _objectRB = GetComponent<Rigidbody>();
    }

    public void OnPicked(Transform attachPoint)
    {
        transform.position = attachPoint.position;
        transform.rotation = attachPoint.rotation;
        transform.SetParent(attachPoint);

        _objectRB.isKinematic = true;
        _objectRB.useGravity = false;
    }

    public void OnDropped()
    {
        Destroy(_joint);
        _objectRB.isKinematic = false;
        _objectRB.useGravity = true;
        transform.SetParent(null);
    }
}
