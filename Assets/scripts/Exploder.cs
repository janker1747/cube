using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    private float _radius = 500f;
    private float _force = 100f;

    public void Explode(Vector3 explosionPosition, Cube cube, List<Cube> cubes)
    {
        float scale = cube.transform.localScale.x;
        float explosionRadius = _radius * scale;  
        float explosionForce = _force * scale;

        foreach (Cube affectedCube in cubes)
        {
            Rigidbody rigidbody = affectedCube.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
            }
        }
    }
}
