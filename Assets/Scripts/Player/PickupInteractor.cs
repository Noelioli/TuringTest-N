using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteractor : Interactor
{
    [SerializeField] private Camera _cam;
    [SerializeField] private LayerMask _pickupLayer;
    [SerializeField] private float _pickupDistance;
    [SerializeField] private Transform _attachPosition;

    private bool _isPicked = false;
    private RaycastHit _raycastHit;
    private IPickable _pickable;

    private void Start()
    {
        _input = PlayerInput.GetInstance();
    }

    public override void Interact()
    {
        Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        if (Physics.Raycast(ray, out _raycastHit, _pickupDistance, _pickupLayer))
        {
            _pickable = _raycastHit.transform.GetComponent<IPickable>();
            if (_input._pickupPressed && !_isPicked)
            {
                if (_pickable == null)
                    return;

                _pickable.OnPicked(_attachPosition);
                _isPicked=true;
                return;
            }
        }

        if (_input._pickupPressed && _isPicked && _pickable != null)
        {
            _pickable.OnDropped();
            _isPicked=false;
        }
    }
}
