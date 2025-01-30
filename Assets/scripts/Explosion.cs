using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffectPrefab;
    private float _radius = 100f;
    private float _force = 500f;

    public void Explode(Vector3 explosionPosition)
    {
        Instantiate(explosionEffectPrefab, explosionPosition, Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody rigidbody = colliders[i].attachedRigidbody;

            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(_force, explosionPosition, _radius);
            }
        }
    }
}
