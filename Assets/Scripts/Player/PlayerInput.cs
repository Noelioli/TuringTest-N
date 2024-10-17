using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Before all else
[DefaultExecutionOrder(-100)]

public class PlayerInput : MonoBehaviour
{
    public float _horizontal {  get; private set; }
    public float _vertical { get; private set; }
    public float _mouseX { get; private set; }
    public float _mouseY { get; private set; }

    public bool _sprintHeld { get; private set; }
    public bool _jumpPressed { get; private set; }
    public bool _interactPressed { get; private set; }
    public bool _pickupPressed { get; private set; }
    public bool _primaryShoot { get; private set; }
    public bool _secondaryShoot { get; private set; }

    public bool _weapon1Pressed { get; private set; }
    public bool _weapon2Pressed { get; private set; }

    public bool _commandPressed { get; private set; }

    private bool _clear;

    /// <summary>
    /// Singleton Pattern
    /// </summary>
    private static PlayerInput _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(_instance);
            return;
        }

        _instance = this;
    }

    public static PlayerInput GetInstance() { return _instance; }
    /// <summary>
    /// End of Pattern
    /// </summary>

    // Update is called once per frame
    void Update()
    {
        ClearInput();
        ProcessInputs();
    }

    void ProcessInputs()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");

        _sprintHeld = _sprintHeld || Input.GetButton("Fire3");
        _jumpPressed = _jumpPressed || Input.GetButtonDown("Jump");
        _interactPressed = _interactPressed || Input.GetKeyDown(KeyCode.E);
        _pickupPressed = _pickupPressed || Input.GetKeyDown(KeyCode.F);

        _primaryShoot = _primaryShoot || Input.GetButtonDown("Fire1");
        _secondaryShoot = _secondaryShoot || Input.GetButtonDown("Fire2");

        _weapon1Pressed = _weapon1Pressed || Input.GetKeyDown(KeyCode.Alpha1);
        _weapon2Pressed = _weapon2Pressed || Input.GetKeyDown(KeyCode.Alpha2);

        _commandPressed = _commandPressed || Input.GetKeyDown(KeyCode.G);
    }

    private void FixedUpdate()
    {
        _clear = true;
    }

    void ClearInput()
    {
        if (!_clear)
            return;

        _horizontal = 0;
        _vertical = 0;
        _mouseX = 0;
        _mouseY = 0;

        _sprintHeld = false;
        _jumpPressed = false;
        _interactPressed = false;
        _pickupPressed = false;
            
        _primaryShoot = false;
        _secondaryShoot = false;

        _weapon1Pressed = false;
        _weapon2Pressed = false;

        _commandPressed = false;
    }
}
