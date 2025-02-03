using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private Spawner _spawner;

    private void Awake()
    {
        _spawner = GetComponent<Spawner>();
    }

    private void OnEnable()
    {
        _spawner.Change += ChangeColors;
    }

    private void OnDisable()
    {
        _spawner.Change -= ChangeColors;
    }

    public void ChangeColors(Cube cube)
    {
        MeshRenderer renderer = cube.GetComponent<MeshRenderer>();

        if (renderer != null)
        {
            renderer.material.color = Random.ColorHSV();
        }
    }
}
