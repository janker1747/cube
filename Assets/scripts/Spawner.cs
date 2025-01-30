using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    private static float _currentChance = 100f;
    private Explosion _explosion;
    public event Action<Cube> Change;


    private void Awake()
    {
        _explosion = GetComponent<Explosion>();
    }

    private void OnEnable()
    {
        _prefab.Destroyed += OnCubeDestroyed;
    }

    private void OnDisable()
    {
        _prefab.Destroyed -= OnCubeDestroyed;
    }

    private void OnCubeDestroyed(Cube destroyedCube)
    {
        if (destroyedCube == null)
        {
            return;
        }

        Vector3 position = destroyedCube.transform.position;
        destroyedCube.Destroyed -= OnCubeDestroyed;
        SpawnChance(position);
    }

    private void SpawnChance(Vector3 position)
    {
        float minValue = 0;
        float maxValue = 101;
        float randomValue = UnityEngine.Random.Range(minValue, maxValue);

        if (randomValue < _currentChance)
        {
            Debug.Log("Разделение произошло! Шанс не сработать: " + randomValue);
            SpawnObject(position);
            _currentChance /= 2;
        }
        else
        {
            _explosion?.Explode(position);
        }
    }

    private void SpawnObject(Vector3 position)
    {
        float minValue = 2f;
        float maxValue = 6f;
        int random = UnityEngine.Random.Range((int)minValue, (int)maxValue);

        for (int i = 0; i < random; i++)
        {
            CreateObject(position);
        }
    }

    private void CreateObject(Vector3 position)
    {
        Cube cube = Instantiate(_prefab, position + Vector3.up, Quaternion.identity);
        cube.transform.localScale = _prefab.transform.localScale / 2;

        Rigidbody rigidbody = cube.gameObject.AddComponent<Rigidbody>();

        if (rigidbody != null)
        {
            rigidbody.useGravity = true;
        }

        if (rigidbody != null)
        {
            Collider collider = rigidbody.gameObject.AddComponent<BoxCollider>();
        }

        cube.Destroyed += OnCubeDestroyed;
        Change?.Invoke(cube);
    }


}