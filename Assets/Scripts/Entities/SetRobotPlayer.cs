using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetRobotPlayer : MonoBehaviour
{
    [SerializeField] CommandInteractor commandInteractor;

    void Start()
    {
        commandInteractor._agent = gameObject.GetComponent<NavMeshAgent>();
        commandInteractor.hasAgent = true;
    }
}
