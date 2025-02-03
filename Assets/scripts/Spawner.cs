using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    private Fuse _explosion;
    public event Action<Cube> Change;

    private void Awake()
    {
        _explosion = GetComponent<Fuse>();
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

        TrySpawnNewCubes(position, scale , destroyedCube);
    }

    private void TrySpawnNewCubes(Vector3 position, Vector3 scale , Cube destroyedCube)
    {
        float randomValue = UnityEngine.Random.Range(0f, 101f);

        if (randomValue < destroyedCube.CurrentChance)
        {
            SpawnCubes(position, scale, destroyedCube);
            destroyedCube.ReduceChance();
        }
        else
        {
            _explosion?.Explode(position);
        }
    }

    private void SpawnCubes(Vector3 position, Vector3 scale, Cube destroyedCube)
    {
        int randomCount = UnityEngine.Random.Range(2, 6);

        for (int i = 0; i < randomCount; i++)
        {
            InstantiateCube(position, scale, destroyedCube);
        }
    }

    private void InstantiateCube(Vector3 position, Vector3 scale , Cube destroyedCube)
    {
        Cube cube = Instantiate(destroyedCube, position + Vector3.up, Quaternion.identity);
        cube.transform.localScale = scale / 2;

        cube.InheritChance(destroyedCube);
        cube.Destroyed += HandleCubeDestruction;
        Change?.Invoke(cube);
    }
}
