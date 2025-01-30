using System;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public event Action<Cube> Destroyed;

    public void OnClick()
    {
        Vector3 position = transform.position;
        Destroyed?.Invoke(this);
        Destroy(gameObject);
    }
}