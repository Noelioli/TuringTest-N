using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorKeyScript : MonoBehaviour
{
    public UnityEvent OnKeyPicked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnKeyPicked?.Invoke();
            Destroy(gameObject);
        }
    }
}
