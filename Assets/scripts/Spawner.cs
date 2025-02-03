using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private GameObject _explosionEffectPrefab;
    private Exploder _explosion;
    public event Action<Cube> Change;

    private void Awake()
    {
        _explosion = GetComponent<Exploder>();
    }

    private void OnEnable()
    {
        _prefab.Destroyed += HandleCubeDestruction;
    }

    private void OnDisable()
    {
        _prefab.Destroyed -= HandleCubeDestruction;
    }

    private void HandleCubeDestruction(Cube destroyedCube)
    {
        if (destroyedCube == null)
        {
            return;
        }

        Vector3 position = destroyedCube.transform.position;
        Vector3 scale = destroyedCube.transform.localScale;
        destroyedCube.Destroyed -= HandleCubeDestruction;

        TrySpawnNewCubes(position, scale, destroyedCube);
    }

    private void TrySpawnNewCubes(Vector3 position, Vector3 scale, Cube destroyedCube)
    {
        float minValue = 0f;
        float maxValue = 101f;

        float randomValue = UnityEngine.Random.Range(minValue, maxValue);

        if (randomValue < destroyedCube.CurrentChance)
        {
            SpawnCubes(position, scale, destroyedCube);
            destroyedCube.ReduceChance();
        }
        else
        {
            List<Cube> cubes = SpawnCubes(position, scale, destroyedCube);
            Instantiate(_explosionEffectPrefab, position, Quaternion.identity);
            _explosion?.Explode(position, destroyedCube, cubes);
        }
    }

    private List<Cube> SpawnCubes(Vector3 position, Vector3 scale, Cube destroyedCube)
    {
        int minValue = 2;
        int maxValue = 6;

        int randomCount = UnityEngine.Random.Range(minValue, maxValue);
        List<Cube> newCubes = new List<Cube>();

        for (int i = 0; i < randomCount; i++)
        {
            Cube cube = InstantiateCube(position, scale, destroyedCube);
            newCubes.Add(cube);
        }

        return newCubes;
    }

    private Cube InstantiateCube(Vector3 position, Vector3 scale, Cube destroyedCube)
    {
        Cube cube = Instantiate(destroyedCube, position + Vector3.up, Quaternion.identity);
        cube.transform.localScale = scale / 2;

        cube.InheritChance(destroyedCube);
        cube.Destroyed += HandleCubeDestruction;
        Change?.Invoke(cube);

        return cube;
    }
}
