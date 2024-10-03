using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovementBehaviour : MonoBehaviour
{
    PlayerInput _input;

    [Header("Player Turn")]
    [SerializeField] private float _turnSpeed;
    [SerializeField] private bool _invertMouse;

    private float _camXRotation;

    // Start is called before the first frame update
    void Start()
    {
        _input = PlayerInput.GetInstance();

        //hides mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        _camXRotation += Time.deltaTime * _input._mouseY * _turnSpeed * (_invertMouse ? 1 : -1);
        _camXRotation = Mathf.Clamp(_camXRotation, -85f, 85f);

        transform.localRotation = Quaternion.Euler(_camXRotation, 0, 0);
    }
}
