using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _sprintMultiplier;
    [SerializeField] private float _turnSpeed; // camMove
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private bool _invertMouse; // camMove
    [SerializeField] private float _jumpVelocity;
    [SerializeField] private float _gravity = -9.81f;

    [Header("Ground Check")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _groundCheckDistance;

    [Header("Shooting")]
    [SerializeField] private Rigidbody _bulletPrefab;
    [SerializeField] private float _shootForce;
    [SerializeField] private Transform _shootPoint;

    [Header("Interactable")]
    [SerializeField] private LayerMask _buttonLayer;
    [SerializeField] private float _rayCastDistance;
    private RaycastHit _rayCastHit;
    private ISelectable _selectable;
    private Camera _cam;

    [Header("Pick and Drop")]
    [SerializeField] private LayerMask _pickupLayer;
    [SerializeField] private float _pickupDistance = 5f;
    [SerializeField] private Transform _attachTransform;
    bool _isPicked;
    private IPickable _pickable;

    private CharacterController _characterController;

    private float _horizontal, _vertical;
    private float _mouseX, _mouseY;
    private float _camXRotation; // camMove
    private float _moveMultiplier = 1f;
    private Vector3 _playerVelocity;
    private bool _isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;

        _characterController = GetComponent<CharacterController>();

        //hides mouse
        Cursor.lockState = CursorLockMode.Locked; // CamMove
        Cursor.visible = false; // CamMove
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        MovePlayer();
        RotatePlayer(); // part CamMove
        GroundCheck();
        JumpCheck();

        ShootBullet();
        Interact();
        PickAndDrop();
    }

    void GetInput()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");

        _moveMultiplier = Input.GetButton("Fire3") ? _sprintMultiplier : 1;
    }

    void MovePlayer()
    {
        _characterController.Move((transform.forward * _vertical + transform.right * _horizontal) * _moveSpeed * Time.deltaTime * _moveMultiplier);

        //Ground check
        if (_isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2f;
        }

        _playerVelocity.y += _gravity * Time.deltaTime;

        _characterController.Move(_playerVelocity * Time.deltaTime);
    }

    void RotatePlayer()
    {
        transform.Rotate(Vector3.up * _turnSpeed * Time.deltaTime * _mouseX);

        _camXRotation += Time.deltaTime * _mouseY * _turnSpeed * (_invertMouse ? 1 : -1);
        _camXRotation = Mathf.Clamp(_camXRotation, -85f, 85f);

        _cameraTransform.localRotation = Quaternion.Euler(_camXRotation, 0, 0);
    }

    void GroundCheck()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckDistance, _groundMask);
    }

    void JumpCheck()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _playerVelocity.y = _jumpVelocity;
        }
    }

    void ShootBullet()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody bullet = Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);
            bullet.velocity = _cameraTransform.forward * _shootForce;
            Destroy(bullet, 5.0f);
        }
    }

    void Interact()
    {
        Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out _rayCastHit, _rayCastDistance, _buttonLayer))
        {
            //Temp hold last ray cast.
            _selectable = _rayCastHit.transform.GetComponent<ISelectable>();
            if (_selectable != null)
            {
                _selectable.OnHoverEnter();

                if (Input.GetKeyDown(KeyCode.F))
                {
                    _selectable.OnSelect();
                }
            }
        }
        if (_rayCastHit.transform == null && _selectable != null)
        {
            _selectable.OnHoverExit();
            _selectable = null;
        }
    }

    void PickAndDrop()
    {
        Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out _rayCastHit, _pickupDistance, _pickupLayer))
        {
            _pickable = _rayCastHit.transform.GetComponent<IPickable>();
            if (Input.GetKeyDown(KeyCode.E) && !_isPicked && _pickable != null)
            {
                _pickable.OnPicked(_attachTransform);
                _isPicked = true;
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && _isPicked && _pickable != null)
        {
            _pickable.OnDropped();
            _isPicked = false;
            _pickable = null;
        }
    }
}
