using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorKeyScript : MonoBehaviour
{
    public UnityEvent OnKeyPicked;

    public void PickupKeycard()
    {
        OnKeyPicked?.Invoke();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickupKeycard();
        }
    }
}
