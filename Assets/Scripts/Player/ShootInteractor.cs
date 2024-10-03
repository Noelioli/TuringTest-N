using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootInteractor : Interactor
{
    [SerializeField] private Input _inputType;

    [Header("Gun")]
    public MeshRenderer _gunRenderer;
    public Color _bulletGunColour;
    public Color _rocketGunColour;

    [Header("Shoot")]
    public ObjectPool _bulletPool;
    public ObjectPool _rocketPool;

    [SerializeField] private float _shootVelocity;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private PlayerMovementBehaviour _movementBehaviour;

    private float _finalShootVelocity;
    private IShootStrategy _currentShootStrategy;

    private void Start()
    {
        _input = PlayerInput.GetInstance();
    }

    public enum Input
    {
        Primary,
        Secondary
    }

    public override void Interact()
    {
        if (_currentShootStrategy == null)
        {
            _currentShootStrategy = new BulletShootStrategy(this);
        }

        if (_input._weapon1Pressed)
        {
            _currentShootStrategy = new BulletShootStrategy(this);
        }

        if (_input._weapon2Pressed)
        {
            _currentShootStrategy = new RocketShootStrategy(this);
        }

        if (_input._primaryShoot && _currentShootStrategy != null)
        {
            _currentShootStrategy.Shoot();
        }
        // Add secondary shoot here
    }

    public float GetShootVelocity()
    {
        _finalShootVelocity = _movementBehaviour.GetForwardSpeed() + _shootVelocity;
        return _finalShootVelocity;
    }

    public Transform GetShootPoint()
    {
        return _shootPoint;
    }
}