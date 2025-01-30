using System;
using System.Collections.Generic;
using UnityEngine;

public class RaycastScript : MonoBehaviour
{
    private Camera _camera;

    private int _mouseButton = 0;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(_mouseButton))
        {
            DrawsLine();
        }
    }

    private void DrawsLine()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.TryGetComponent(out Cube cube))
            {
                cube.OnClick();
            }
        }
    }
}
