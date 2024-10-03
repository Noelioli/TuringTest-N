using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PooledObject : MonoBehaviour
{
    [SerializeField] private UnityEvent OnReset;

    ObjectPool _associatedPool;

    float _timer;
    bool _setToDestroy = false;
    float _destroyTime = 0f;

    public void SetObjectPool(ObjectPool pool)
    {
        _associatedPool = pool;
        _timer = 0;
        _destroyTime = 0f;
        _setToDestroy = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_setToDestroy)
        {
            _timer += Time.deltaTime;

            if (_timer >= _destroyTime)
            {
                _setToDestroy = false;
                _timer = 0;
                Destroy();
            }
        }
    }

    public void ResetObject()
    {
        OnReset?.Invoke();
    }

    public void Destroy()
    {
        if (_associatedPool != null)
        {
            _associatedPool.RestoreObject(this);
        }
    }

    public void Destroy(float time)
    {
        _setToDestroy = true;
        _destroyTime = time;
    }
}
