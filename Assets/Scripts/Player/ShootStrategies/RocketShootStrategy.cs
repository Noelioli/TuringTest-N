using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShootStrategy : IShootStrategy
{
    ShootInteractor _interactor;
    Transform _shootPoint;

    public RocketShootStrategy(ShootInteractor interactor)
    {
        Debug.Log("Switched to Bullet Mode");
        _interactor = interactor;
        _shootPoint = _interactor.GetShootPoint();

        // change gun colour
        _interactor._gunRenderer.material.color = _interactor._rocketGunColour;
    }

    public void Shoot()
    {
        PooledObject pooledRocket = _interactor._rocketPool.GetPooledObject();
        pooledRocket.gameObject.SetActive(true);

        Rigidbody rocket = pooledRocket.GetComponent<Rigidbody>();
        rocket.transform.position = _shootPoint.position;
        rocket.transform.rotation = _shootPoint.rotation;
        //Rigidbody bullet = Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);

        rocket.velocity = _shootPoint.forward * _interactor.GetShootVelocity();
        _interactor._rocketPool.DestroyPooledObject(pooledRocket, 5.0f);
        //Destroy(bullet.gameObject, 5.0f);
    }
}
