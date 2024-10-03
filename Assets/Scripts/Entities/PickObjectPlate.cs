using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObjectPlate : MonoBehaviour
{
    [SerializeField] private Animator _doorAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickable"))
        {
            _doorAnimator.SetBool("Door", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pickable"))
        {
            _doorAnimator.SetBool("Door", false);
        }
    }
}
