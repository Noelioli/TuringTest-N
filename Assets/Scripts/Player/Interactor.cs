using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactor : MonoBehaviour
{
    protected PlayerInput _input;

    private void Update()
    {
        Interact();
    }

    public abstract void Interact();
}
