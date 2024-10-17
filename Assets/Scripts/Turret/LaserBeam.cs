using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] float _beamDistance = 5.0f;
    [SerializeField] float _damage = 0.2f;
    [SerializeField] Transform _shootPoint;
    [SerializeField] Transform _endPoint; //Just to manage direction
    LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void FixedUpdate()
    {
        _lineRenderer.SetPosition(0, _shootPoint.position);

        RaycastHit hit;
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
            _lineRenderer.SetPosition(1, _shootPoint.position + (_endPoint.position - _shootPoint.position).normalized * _beamDistance);

        }
    }
}