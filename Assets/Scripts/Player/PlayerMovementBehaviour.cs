using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementBehaviour : MonoBehaviour
{
    PlayerInput _input;

    [Header("PlayerMovement")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _sprintMultiplier;

    [Header("Ground Check")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _groundCheckDistance;

    private CharacterController _characterController;

    private Vector3 _playerVelocity;
    public bool _isGrounded { get; private set; }
    private float _moveMultiplier = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _input = PlayerInput.GetInstance();
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        if (GameManager.GetInstance().IsInputActive())
            MovePlayer();
    }

    void MovePlayer()
    {
        _moveMultiplier = _input._sprintHeld ? _sprintMultiplier : 1f;

        _characterController.Move(_moveMultiplier * _moveSpeed * Time.deltaTime * (transform.forward * _input._vertical + transform.right * _input._horizontal));

        //Ground check
        if (_isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2f;
        }

        _playerVelocity.y += _gravity * Time.deltaTime;

        _characterController.Move(_playerVelocity * Time.deltaTime);
    }

    void GroundCheck()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckDistance, _groundMask);
    }

    public void SetYVelocity(float value)
    {
        _playerVelocity.y = value;
    }

    public float GetForwardSpeed()
    {
        return _input._vertical * _moveSpeed * _moveMultiplier;
    }
}
