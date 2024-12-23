using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnBehaviour : MonoBehaviour
{
    PlayerInput _input;

    [Header("Player Turn")]
    [SerializeField] private float _turnSpeed;

    private void Start()
    {
        _input = PlayerInput.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GetInstance().IsInputActive())
            RotatePlayer();
    }

    void RotatePlayer()
    {
        transform.Rotate(Vector3.up * _turnSpeed * Time.deltaTime * _input._mouseX);
    }
}
