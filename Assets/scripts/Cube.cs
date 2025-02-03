using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public event Action<Cube> Destroyed;
    public float CurrentChance { get; private set; } = 100f;

    public MeshRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    public void InheritChance(Cube parentCube)
    {
        int divider = 2;
        CurrentChance = parentCube.CurrentChance / divider;
    }

    public void ReduceChance()
    {
        CurrentChance /= 2;
    }

    public void OnClick()
    {
        Destroyed?.Invoke(this);
        Destroy(gameObject);
    }
}