using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardDoorBehaviour : MonoBehaviour
{
    [SerializeField] private string _doorColour;
    [SerializeField] private Animator _gearAnimator;
    [SerializeField] private Animator _doorAnimator;
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (gameManager.GetKeycard(_doorColour) && other.gameObject.CompareTag("Player"))
        {
            _gearAnimator.SetBool("GearSpin", true);
            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(2.5f);
        _doorAnimator.SetBool("OpenDoor", true);
    }
}
