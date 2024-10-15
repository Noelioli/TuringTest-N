using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotation : MonoBehaviour
{
    [SerializeField] float maxSpread;
    [SerializeField] float rotationTime;
    float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        transform.localRotation = Quaternion.Euler(0, maxSpread * Mathf.Sin(rotationTime * timer), 0);
    }
}
