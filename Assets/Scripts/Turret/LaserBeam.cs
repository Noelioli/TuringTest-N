using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] Transform _shootPoint;
    [SerializeField] bool _isRotating;
    [SerializeField] Transform _endPoint; // For rotation, because I cannot rotate the beam correctly, so set manually
    [SerializeField] float _beamDistance = 5.0f; // Normal no movement
    [SerializeField] float _damage = 0.2f;
    LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void FixedUpdate()
    {
        _lineRenderer.SetPosition(0, _shootPoint.position);

        //This code is awful I know, I just wanted it to work
        RaycastHit hit;
        if (_isRotating)
        {
            if (Physics.Raycast(_shootPoint.position, _endPoint.position - _shootPoint.position, out hit, _beamDistance))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    _lineRenderer.SetPosition(1, _shootPoint.position + (_endPoint.position - _shootPoint.position).normalized * hit.distance);
                    hit.transform.GetComponent<Health>().DeductHealth(_damage);

                }
                else
                {
                    _lineRenderer.SetPosition(1, _shootPoint.position + (_endPoint.position - _shootPoint.position).normalized * hit.distance);
                }
            }
            else
            {
                _lineRenderer.SetPosition(1, _endPoint.position);

            }
        }
        else
        {
            if (Physics.Raycast(_shootPoint.position, Vector3.forward, out hit, _beamDistance))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    _lineRenderer.SetPosition(1, _shootPoint.position + Vector3.forward * hit.distance);
                    hit.transform.GetComponent<Health>().DeductHealth(_damage);

                }
                else
                {
                    _lineRenderer.SetPosition(1, _shootPoint.position + Vector3.forward * hit.distance);
                }
            }
            else
            {
                _lineRenderer.SetPosition(1, _shootPoint.position + Vector3.forward * _beamDistance);

            }
        }
    }
}