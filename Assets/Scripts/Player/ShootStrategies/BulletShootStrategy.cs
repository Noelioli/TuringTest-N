using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShootStrategy : IShootStrategy
{
    ShootInteractor _interactor;
    Transform _shootPoint;

    public BulletShootStrategy(ShootInteractor interactor)
    {
        Debug.Log("Switched to Bullet Mode");
        _interactor = interactor;
        _shootPoint = _interactor.GetShootPoint();

        // change gun colour
        _interactor._gunRenderer.material.color = _interactor._bulletGunColour;
    }

    public void Shoot()
    {
        PooledObject pooledBullet = _interactor._bulletPool.GetPooledObject();
        pooledBullet.gameObject.SetActive(true);

        Rigidbody bullet = pooledBullet.GetComponent<Rigidbody>();
        bullet.transform.position = _shootPoint.position;
        bullet.transform.rotation = _shootPoint.rotation;
        //Rigidbody bullet = Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);

        bullet.velocity = _shootPoint.forward * _interactor.GetShootVelocity();
        _interactor._bulletPool.DestroyPooledObject(pooledBullet, 5.0f);
        //Destroy(bullet.gameObject, 5.0f);
    }
}
