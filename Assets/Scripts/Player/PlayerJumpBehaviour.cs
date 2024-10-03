using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovementBehaviour))]
public class PlayerJumpBehaviour : Interactor
{
    [Header("Jump")]
    [SerializeField] private float _jumpVelocity;

    private PlayerMovementBehaviour _movementBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        _input = PlayerInput.GetInstance();

        _movementBehaviour = GetComponent<PlayerMovementBehaviour>();
    }

    public override void Interact()
    {
        if (_input._jumpPressed && _movementBehaviour._isGrounded)
        {
            _movementBehaviour.SetYVelocity(_jumpVelocity);
        }
    }
}
