using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloser : MonoBehaviour
{
    [SerializeField] Animator _animatorDoor1;
    [SerializeField] Animator _animatorDoor2;

    private void Start()
    {
        _animatorDoor1.SetBool("Door", false);
        _animatorDoor2.SetBool("Door", true);
    }
}
