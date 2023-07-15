using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform barrel;
	public Rigidbody2D bullet;
	
    private float _cdShoot = 0.0f;
	private float _shootDelay = 0.65f;
    public float fireSpeed = 500f;

	// Update is called once per frame
	void Update () {
		Fire ();
	}

	void Fire ()
	{
		if (Input.GetButton("Fire1") && Time.time > _cdShoot) {
			var firedBullet = Instantiate (bullet, barrel.position, barrel.rotation);
            firedBullet.transform.localRotation = Quaternion.Euler(0,0, barrel.rotation.z);
			firedBullet.AddForce (barrel.up * fireSpeed);
            firedBullet.transform.localRotation = Quaternion.Euler(0,0, barrel.rotation.z);
            _cdShoot = _shootDelay + Time.time;
		}
	}

}
